using System;
using System.Windows.Data;

namespace UniaraVirtual.Assets.Converters
{
    public class TextCaseConverter : IValueConverter
    {
        private string ToNotaText(object value)
        {
            if (value == null)
            {
                return "N.A.";
            }

            decimal nota = System.Convert.ToDecimal(value);
            if (nota == -1)
            {
                return "N.C.";
            }
            else
            {
                return nota.ToString();
            }
        }

        private string ToAbbr(object value)
        {
            string name = (string)value;

            if (!string.IsNullOrEmpty(name))
            {
                var palavra = name.Split(' ');
                for (int index = 0; index < palavra.Length; index++)
                {
                    var p = palavra[index];
                    if (p.Length > 3)
                    {
                        palavra[index] = p[0].ToString();
                    }
                    else
                    {
                        palavra[index] = "";
                    }
                }
                name = string.Join("", palavra);
            }

            return name;
        }

        private string ToCamelCase(object value)
        {
            string name = (string)value;

            if (!string.IsNullOrEmpty(name))
            {
                var palavra = name.Split(' ');
                for (int index = 0; index < palavra.Length; index++)
                {
                    var p = palavra[index].ToLower();
                    if (p.Length > 3)
                    {
                        palavra[index] = p[0].ToString().ToUpper() + p.Substring(1).ToLower();
                    }
                    else
                    {
                        if (p == "i" || p == "ii" || p == "iii" || p == "iv" || p == "v")
                            palavra[index] = p.ToUpper();
                        else
                            palavra[index] = p;
                    }
                }
                name = string.Join(" ", palavra);
            }
            return name;
        }

        private string ToWeekDayAbbr(object value)
        {
            string name = (string)value;

            if (!string.IsNullOrEmpty(name))
            {
                var palavra = name.Substring(0, 3);
                name = palavra;
            }

            return name;
        }

        private string ToStringFaltas(object value)
        {
            if (value == null)
            {
                return "Sem Faltas";
            }
            else
            {
                int o = System.Convert.ToInt32(value);
                string message = string.Format("{0} {1}", o, o > 1 ? "Faltas" : "Falta");
                return message;
            }
        }

        private string ToStringFrequencia(object value)
        {
            string message = System.Convert.ToString(value);
            return message + "%";
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter is string)
            {
                string param = parameter as string;

                if (param == "ToUpper")
                    return value.ToString().ToUpper();
                if (param == "ToLower")
                    return value.ToString().ToLower();
                if (param == "CamelCase")
                    return ToCamelCase(value);
                if (param == "ToAbbr")
                    return ToAbbr(value);
                if (param == "ToWeekDay")
                    return ToWeekDayAbbr(value).ToUpper();
                if (param == "ToFalta")
                    return ToStringFaltas(value).ToUpper();
                if (param == "ToFrequencia")
                    return ToStringFrequencia(value).ToUpper();
                if (param == "ToNotaText")
                    return ToNotaText(value);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
