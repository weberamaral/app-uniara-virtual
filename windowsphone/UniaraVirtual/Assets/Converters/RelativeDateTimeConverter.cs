using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;

namespace UniaraVirtual.Assets.Converters
{
    public class RelativeDateTimeConverter : IValueConverter
    {
        private const int _minutos = 60;
        private const int _horas = _minutos * 60;
        private const int _dias = _horas * 24;
        private const int _ano = _dias * 365;

        private readonly System.Collections.Generic.Dictionary<long, Func<TimeSpan, string>> thresholds = new Dictionary<long, Func<TimeSpan, string>>
        {
            {2, t => "há alguns segundos"},
            {_minutos, t => string.Format("{0} segundos atrás", (int)t.TotalSeconds)},
            {_minutos * 2, t => "um minuto atrás"},
            {_horas, t => string.Format("{0} minutos atrás", (int)t.TotalMinutes)},
            {_horas * 2, t => "uma hora atrás"},
            {_dias, t => "hoje"},
            {_dias * 2, t => "ontem"},
            {_dias * 30, t => string.Format("{0} dias atrás", (int)t.TotalDays)},
            {_dias * 60, t => "mês passado"},
            {_ano, t => string.Format("{0} meses atrás", (int)t.TotalDays / 30)},
            {_ano * 2, t => "ano passado"},
            {int.MaxValue, t => string.Format("{0} anos atrás", (int)t.TotalDays / 365)}
        };

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string dataOriginal = value as string;
            string[] data = dataOriginal.Split('/');

            DateTime dateTime = new DateTime(
                System.Convert.ToInt32(data[2]),
                System.Convert.ToInt32(data[1]),
                System.Convert.ToInt32(data[0]));


            var diference = DateTime.UtcNow - dateTime.ToUniversalTime();

            return thresholds.First(t => diference.TotalSeconds < t.Key).Value(diference);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
