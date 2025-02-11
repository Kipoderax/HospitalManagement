﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace HospitalManagement
{
    /// <summary>
    /// A converter that takes in an RGB astring such as FF00FF and converts it to a WPF brush
    /// </summary>
    public class StringRGBToBrushConverter : BaseValueConverter<StringRGBToBrushConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (SolidColorBrush) new BrushConverter().ConvertFrom( $"#{value}" );
        }
    }
}
