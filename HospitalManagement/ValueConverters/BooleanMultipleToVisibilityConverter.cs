using System;
using System.Globalization;
using System.Windows;

namespace HospitalManagement
{
    /// <summary>
    /// A converter that takes in a multiple booleans and returns a <see cref="Visibility"/>
    /// </summary>
    public class BooleanMultipleToVisibilityConverter : BaseValueConverter<BooleanMultipleToVisibilityConverter>
    {
        public override object Convert( object[] values, Type targetType, object parameter, CultureInfo culture )
        {
            // TODO: Search better solution for universal using this converter
            
            // values[0] - is employee adm
            // values[1] - is other profile

            if( values[1] == null || values[0] == null ) return Visibility.Hidden;

            switch ( (bool) values[1] )
            {
                // Hidden if administrator overview other profile
                case true:
                    return Visibility.Hidden;
                
                // Visible if administrator overview self profile
                case false when (bool) values[0]:
                    return Visibility.Visible;
            }

            // Hidden if user isn't administrator and overview self profile
            if( !(bool) values[1] && !(bool) values[0] ) return Visibility.Hidden;
            
            
            return Visibility.Hidden;
        }

    }
}