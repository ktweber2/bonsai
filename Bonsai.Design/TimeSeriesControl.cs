﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Bonsai.Design
{
    public partial class TimeSeriesControl : UserControl
    {
        public TimeSeriesControl()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        public DataPointCollection Points
        {
            get { return chart.Series[0].Points; }
        }
    }
}
