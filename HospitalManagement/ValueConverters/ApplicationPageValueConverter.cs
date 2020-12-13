using HospitalManagement.Core;
using System;
using System.Diagnostics;
using System.Globalization;

namespace HospitalManagement
{
    /// <summary>
    /// Converts the <see cref="ApplicationPage"/> to an acutal view/page
    /// </summary>
    public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Find the appropriate page
            switch ((ApplicationPage)value)
            {
                case ApplicationPage.Login:
                    return new LoginPage();

                case ApplicationPage.Work:
                    return new WorkPage();

                default:
                    Debugger.Break();
                    return null;
            }
        }
    }
}
