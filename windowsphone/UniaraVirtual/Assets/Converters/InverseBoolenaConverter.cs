using System;
using System.Windows.Data;

namespace UniaraVirtual.Assets.Converters
{
    public class InverseBoolenaConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool enable = (bool)value;
            return enable ? false : true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool enable = (bool)value;
            return enable ? true : false;
        }
    }
}
