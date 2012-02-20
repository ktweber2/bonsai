﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace Bonsai.Arduino
{
    public sealed class Arduino : IDisposable
    {
        #region Constants

        public const int Low = 0;
        public const int High = 1;

        const int AnalogPins = 6;
        const int DigitalPorts = 2;
        const int ConnectionDelay = 2000;
        const int DefaultBaudRate = 115200;

        const int DIGITAL_MESSAGE = 0x90; // send data for a digital port
        const int ANALOG_MESSAGE  = 0xE0; // send data for an analog pin (or PWM)
        const int REPORT_ANALOG   = 0xC0; // enable analog input by pin #
        const int REPORT_DIGITAL  = 0xD0; // enable digital input by port
        const int SET_PIN_MODE    = 0xF4; // set a pin to INPUT/OUTPUT/PWM/etc
        const int REPORT_VERSION  = 0xF9; // report firmware version
        const int SYSTEM_RESET    = 0xFF; // reset from MIDI
        const int START_SYSEX     = 0xF0; // start a MIDI SysEx message
        const int END_SYSEX       = 0xF7; // end a MIDI SysEx message

        #endregion

        bool disposed;
        int dataToReceive;
        int multiByteCommand;
        int multiByteChannel;
        readonly SerialPort serialPort;
        readonly byte[] commandBuffer;
        readonly byte[] responseBuffer;
        readonly byte[] readBuffer;
        readonly byte[] analogInput;
        readonly byte[] digitalInput;
        readonly byte[] digitalOutput;

        public Arduino(string portName)
        {
            serialPort = new SerialPort(portName);
            serialPort.BaudRate = DefaultBaudRate;
            serialPort.Parity = Parity.None;
            serialPort.StopBits = StopBits.One;

            commandBuffer = new byte[3];
            responseBuffer = new byte[2];
            readBuffer = new byte[serialPort.ReadBufferSize];
            analogInput = new byte[AnalogPins];
            digitalInput = new byte[DigitalPorts];
            digitalOutput = new byte[DigitalPorts];
            serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
        }

        public event EventHandler<AnalogInputReceivedEventArgs> AnalogInputReceived;

        public event EventHandler<DigitalInputReceivedEventArgs> DigitalInputReceived;

        public int MajorVersion { get; private set; }

        public int MinorVersion { get; private set; }

        public bool IsOpen
        {
            get { return serialPort.IsOpen; }
        }

        void OnAnalogInputReceived(AnalogInputReceivedEventArgs e)
        {
            var handler = AnalogInputReceived;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        void OnDigitalInputReceived(DigitalInputReceivedEventArgs e)
        {
            var handler = DigitalInputReceived;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var bytesToRead = serialPort.BytesToRead;
            if (serialPort.IsOpen && bytesToRead > 0)
            {
                bytesToRead = serialPort.Read(readBuffer, 0, bytesToRead);
                for (int i = 0; i < bytesToRead; i++)
                {
                    ProcessInput(readBuffer[i]);
                }
            }
        }

        public void Open()
        {
            serialPort.Open();
            Thread.Sleep(ConnectionDelay);

            for (int i = 0; i < AnalogPins; i++)
            {
                ReportAnalog(i, true);
            }

            for (int i = 0; i < DigitalPorts; i++)
            {
                ReportDigital(i, true);
            }
        }

        public void PinMode(int pin, PinMode mode)
        {
            commandBuffer[0] = SET_PIN_MODE;
            commandBuffer[1] = (byte)pin;
            commandBuffer[2] = (byte)mode;
            serialPort.Write(commandBuffer, 0, 3);
        }

        public int DigitalRead(int pin)
        {
            var portNumber = (pin >> 3) & 0x0F;
            return ((digitalInput[portNumber] >> (pin & 0x07)) & 0x01);
        }

        public void DigitalWrite(int pin, int value)
        {
            var portNumber = (pin >> 3) & 0x0F;
            if (value == 0) digitalOutput[portNumber] &= (byte)~(1 << (pin & 0x07));
            else digitalOutput[portNumber] |= (byte)(1 << (pin & 0x07));

            commandBuffer[0] = (byte)(DIGITAL_MESSAGE | portNumber);
            commandBuffer[1] = (byte)(digitalOutput[portNumber] & 0x7F);
            commandBuffer[2] = (byte)(digitalOutput[portNumber] >> 7);
            serialPort.Write(commandBuffer, 0, 3);
        }

        public int AnalogRead(int pin)
        {
            return analogInput[pin];
        }

        public void AnalogWrite(int pin, int value)
        {
            commandBuffer[0] = (byte)(ANALOG_MESSAGE | (pin & 0x0F));
            commandBuffer[1] = (byte)(value & 0x7F);
            commandBuffer[2] = (byte)(value >> 7);
            serialPort.Write(commandBuffer, 0, 3);
        }

        void ReportAnalog(int pin, bool state)
        {
            commandBuffer[0] = (byte)(REPORT_ANALOG | pin);
            commandBuffer[1] = (byte)(state ? 1 : 0);
            serialPort.Write(commandBuffer, 0, 2);
        }

        void ReportDigital(int port, bool state)
        {
            commandBuffer[0] = (byte)(REPORT_DIGITAL | port);
            commandBuffer[1] = (byte)(state ? 1 : 0);
            serialPort.Write(commandBuffer, 0, 2);
        }

        void SetDigitalInput(int port, int data)
        {
            digitalInput[port] = (byte)data;
            OnDigitalInputReceived(new DigitalInputReceivedEventArgs(port, data));
        }

        void SetAnalogInput(int pin, int value)
        {
            analogInput[pin] = (byte)value;
            OnAnalogInputReceived(new AnalogInputReceivedEventArgs(pin, value));
        }

        void SetVersion(int majorVersion, int minorVersion)
        {
            MajorVersion = majorVersion;
            MinorVersion = minorVersion;
        }

        void ProcessInput(byte inputData)
        {
            if (dataToReceive > 0 && inputData < 128)
            {
                dataToReceive--;
                responseBuffer[dataToReceive] = inputData;

                if (multiByteCommand != 0 && dataToReceive == 0)
                {
                    switch (multiByteCommand)
                    {
                        case DIGITAL_MESSAGE: SetDigitalInput(multiByteChannel, (responseBuffer[0] << 7) + responseBuffer[1]); break;
                        case ANALOG_MESSAGE: SetAnalogInput(multiByteChannel, (responseBuffer[0] << 7) + responseBuffer[1]); break;
                        case REPORT_VERSION: SetVersion(responseBuffer[1], responseBuffer[0]); break;
                    }
                }
            }
            else
            {
                int command;
                if (inputData < 0xF0)
                {
                    command = inputData & 0xF0;
                    multiByteChannel = inputData & 0x0F;
                }
                else command = inputData;

                switch (command)
                {
                    case DIGITAL_MESSAGE:
                    case ANALOG_MESSAGE:
                    case REPORT_VERSION:
                        dataToReceive = 2;
                        multiByteCommand = command;
                        break;
                }
            }
        }

        public void Close()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Arduino()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    serialPort.Close();
                    disposed = true;
                }
            }
        }

        void IDisposable.Dispose()
        {
            Close();
        }
    }

    public enum PinMode : byte
    {
        Input = 0,
        Output = 1,
        Analog = 2,
        Pwm = 3,
        Servo = 4,
        Shift = 5,
        I2C = 6
    }
}
