﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Reactive.Linq;

namespace Bonsai.IO
{
    public class SerialStringWrite : Sink<object>
    {
        [Editor("Bonsai.IO.Design.SerialPortConfigurationEditor, Bonsai.IO.Design", typeof(UITypeEditor))]
        public string PortName { get; set; }

        public override IObservable<object> Process(IObservable<object> source)
        {
            return Observable.Using(
                () =>
                {
                    var writeLine = ObservableSerialPort.WriteLine(PortName);
                    var iterator = writeLine.GetEnumerator();
                    iterator.MoveNext();
                    return iterator;
                },
                iterator => source.Do(input => iterator.Current(input.ToString())));
        }
    }
}
