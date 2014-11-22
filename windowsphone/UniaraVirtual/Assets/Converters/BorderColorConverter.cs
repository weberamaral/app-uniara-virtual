using System;
using System.Windows.Data;
using System.Windows.Media;

namespace UniaraVirtual.Assets.Converters
{
    public class BorderColorConverter : IValueConverter
    {
        private Brush FrequenciaColor(object value)
        {
            string f = value as string;
            int frequencia = System.Convert.ToInt32(f.Replace("%", ""));
            if (frequencia < 75)
            {
                return (Brush)App.Current.Resources["CustomBrushRed"];
            }
            else if (frequencia >= 75 && frequencia < 80)
            {
                return (Brush)App.Current.Resources["CustomBrushYellow"];
            }
            else
            {
                return (Brush)App.Current.Resources["CustomBrushGreen"];
            }
        }

        private Brush NotaColor(object value)
        {
            string n = value as string;

            if (n == "--")
            {
                return (Brush)App.Current.Resources["CustomBrushGray"];
            }
            if (n == "N.C.")
            {
                return (Brush)App.Current.Resources["CustomBrushGray"];
            }

            decimal nota = System.Convert.ToDecimal(n);
            
            if (nota < 6)
            {
                return (Brush)App.Current.Resources["CustomBrushRed"];
            }
            else
            {
                return (Brush)App.Current.Resources["CustomBrushGreen"];
            }
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter is string)
            {
                string param = parameter as string;

                if (param == "ToFrequencia")
                    return FrequenciaColor(value);
                if (param == "ToNota")
                    return NotaColor(value);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
