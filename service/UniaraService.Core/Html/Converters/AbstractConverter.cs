using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace UniaraService.Core.Html.Converters
{
    internal abstract class AbstractConverter
    {
        protected const string STRING_NULL_VALUE = "--";
        protected const string STRING_NOT_VALUE = "N.C.";
        protected readonly CultureInfo LOCALE = new CultureInfo("pt-BR");

        private static string _last_value = string.Empty;

        protected Nullable<int> ToInt(string value)
        {
            Nullable<int> converted;

            if (value != null && !value.Equals(STRING_NULL_VALUE))
            {
                value = ToDigit(value);

                try
                {
                    converted = Convert.ToInt32(value);
                }
                catch (FormatException)
                {
                    converted = null;
                }
                catch (OverflowException)
                {
                    converted = null;
                }
            }
            else
            {
                converted = null;
            }

            return converted;
        }

        protected Nullable<decimal> ToDecimal(String value)
        {
            Nullable<decimal> converted = null;

            if (value != null && value.Equals(STRING_NOT_VALUE))
            {
                converted = -1;
            }
            else if (value != null && !value.Equals(STRING_NULL_VALUE))
            {
                try
                {
                    converted = Convert.ToDecimal(value, LOCALE);
                }
                catch (FormatException)
                {
                    converted = null;
                }
                catch (OverflowException)
                {
                    converted = null;
                }
            }
            else
            {
                converted = null;
            }

            return converted;
        }

        protected Nullable<DateTime> ToDateTime(string value)
        {
            Nullable<DateTime> converted;

            if (value != null && !value.Equals(STRING_NULL_VALUE))
            {
                // formato esperado DD/MM/AAAA
                try
                {
                    converted = Convert.ToDateTime(value, LOCALE);
                }
                catch (FormatException)
                {
                    converted = null;
                }
                catch (OverflowException)
                {
                    converted = null;
                }
            }
            else
            {
                converted = null;
            }

            return converted;
        }

        protected string ToDayOfWeek(string value)
        {
            string converted;
            _last_value = value.Equals("&nbsp;") ? _last_value : value;
            value = value.Equals("&nbsp;") ? _last_value : value;

            if (value != null && !value.Equals(STRING_NULL_VALUE))
            {
                try
                {
                    if (value.ToLower().Equals("segunda")) converted = "Segunda";
                    else if (value.ToLower().Equals("terça")) converted = "Terça";
                    else if (value.ToLower().Equals("quarta")) converted = "Quarta";
                    else if (value.ToLower().Equals("quinta")) converted = "Quinta";
                    else converted = "Sexta";

                }
                catch (FormatException)
                {
                    converted = null;
                }
                catch (OverflowException)
                {
                    converted = null;
                }
            }
            else
            {
                converted = null;
            }

            return converted;
        }

        // frequqncia, carga horaria
        private string ToDigit(string value)
        {
            if (value != null)
            {
                StringBuilder converted = new StringBuilder();
                char[] chars = value.ToCharArray();

                for (int i = 0; i < chars.Length; i++)
                {
                    if (Char.IsDigit(chars[i]) || chars[i].Equals(","))
                    {
                        converted.Append(chars[i]);
                    }
                }

                return converted.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}