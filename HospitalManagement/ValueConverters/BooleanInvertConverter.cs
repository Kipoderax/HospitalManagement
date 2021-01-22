using System;
using System.Globalization;

namespace HospitalManagement
{
    /// <summary>
    /// A converter that takes in a boolean and inverts it
    /// </summary>
    public class BooleanInvertConverter : BaseValueConverter<BooleanInvertConverter>
    {
        public override object Convert ( object value, Type targetType,
                                         object parameter, CultureInfo culture ) => !(bool) value;
    }
}
