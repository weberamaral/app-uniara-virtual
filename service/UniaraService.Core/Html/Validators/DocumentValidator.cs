using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniaraService.Core.Html.Validators
{
    public class DocumentValidator
    {
        /// <summary>
        /// Verifica se o documento obtido do servidor contem as tags necessários da busca
        /// </summary>
        /// <param name="contexto">Dados do servidor</param>
        /// <returns>verdadeiro ou falso</returns>
        public static bool ValidarHtmlDoc(string contexto)
        {
            bool isValido = true;

            if (!contexto.Contains("<div id='conteudo'></div>"))
            {
                isValido = false;
            }
            else if (contexto.Contains("<div id='conteudo'></div>") && contexto.Contains("Sem Registro"))
            {
                isValido = false;
            }

            return isValido;
        }

        /// <summary>
        /// Verifica se o documento obtido do servidor contem as tags necessárias para busca
        /// </summary>
        /// <param name="contexto">Dados do servidor</param>
        /// <returns>Qual a opção de validação/busca</returns>
        public static int ValidarHtmlPerfil(string contexto)
        {
            if (contexto.Contains("<b>Bom Dia,"))
                return 1;
            if (contexto.Contains("<b>Boa Tarde,"))
                return 2;
            if (contexto.Contains("<b>Boa Noite,"))
                return 3;

            return 0;
        }

        /// <summary>
        /// Verifica se o documento obtido do servidor contem as tags necessários da busca
        /// </summary>
        /// <param name="contexto">Dados do servidor</param>
        /// <returns>verdadeiro ou falso</returns>
        public static bool ValidaHtmlHorarioAulas(string contexto)
        {
            bool isValid = true;

            if (!contexto.Contains("<div id='conteudo'></div>"))
                return isValid = false;
            else if (contexto.Contains("<div id='conteudo'></div>") && contexto.Contains("Sem Registro"))
                return isValid = false;
            else
                return isValid;
        }
    }
}
