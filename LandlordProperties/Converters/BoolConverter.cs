using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LandlordProperties.Converters
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class BoolConverter : IValueConverter
    {
        #region All Other Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!targetType.IsAssignableFrom(typeof(bool)))
                throw new ArgumentException("targetType must be bool");
            if (!(value is bool))
                throw new NotSupportedException("value must be a bool");
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }

        #endregion
    }
}
