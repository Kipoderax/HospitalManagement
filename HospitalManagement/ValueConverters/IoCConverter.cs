﻿using HospitalManagement.Core;
using System;
using System.Diagnostics;
using System.Globalization;

namespace HospitalManagement
{
    /// <summary>
    /// Converts a string name to a service pulles from the IoC container
    /// </summary>
    public class IoCConverter : BaseValueConverter<IoCConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Find the appropriate page
            switch ((string) parameter)
            {
                case nameof(ApplicationViewModel):
                    return IoC.Get<ApplicationViewModel>();

                default:
                    Debugger.Break();
                    return null;
            }
        }
    }
}
