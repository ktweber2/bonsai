﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Bonsai.Design;
using OpenCV.Net;

namespace Bonsai.Vision.Design
{
    public class CvScalarConverter : TypeConverter
    {
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            var propertyDescriptor = context.PropertyDescriptor;

            var properties = new PropertyDescriptor[4];
            properties[0] = new DynamicPropertyDescriptor<double>("Val0", c => ((CvScalar)c).Val0, (c, v) => { var s = (CvScalar)c; s.Val0 = (double)v; propertyDescriptor.SetValue(context.Instance, s); });
            properties[1] = new DynamicPropertyDescriptor<double>("Val1", c => ((CvScalar)c).Val1, (c, v) => { var s = (CvScalar)c; s.Val1 = (double)v; propertyDescriptor.SetValue(context.Instance, s); });
            properties[2] = new DynamicPropertyDescriptor<double>("Val2", c => ((CvScalar)c).Val2, (c, v) => { var s = (CvScalar)c; s.Val2 = (double)v; propertyDescriptor.SetValue(context.Instance, s); });
            properties[3] = new DynamicPropertyDescriptor<double>("Val3", c => ((CvScalar)c).Val3, (c, v) => { var s = (CvScalar)c; s.Val3 = (double)v; propertyDescriptor.SetValue(context.Instance, s); });

            return new PropertyDescriptorCollection(properties);
        }
    }
}
