using System;
using UniaraService.Core.Html.Converters;
using UniaraService.Core.Html.Exceptions;
using UniaraService.Core.Html.Utils;
using UniaraService.Core.Html.Validators;
using UniaraService.Model;

namespace UniaraService.Html.Core.Converters.Actions
{
    internal class PerfilConverter : AbstractConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDocumentException"
        /// <exception cref="InvalidDocumentParseException"
        public Perfil ConvertTo(string value)
        {
            // Trazendo o nome da página index
            int op = DocumentValidator.ValidarHtmlPerfil(value);
            if (op == 0)
            {
                throw new InvalidDocumentException("Dados de entrada inválidos");
            }
            else
            {
                string nome = string.Empty;
                string curso = string.Empty;

                try
                {
                    nome = value.Substring(DocumentTrim.RecortarHtmlPerfil(value, op)[0], DocumentTrim.RecortarHtmlPerfil(value, op)[1]).Split(' ')[0];
                    // Trazendo o curso da página index
                    //string url = value.Substring(Helpers.RecortarHtmlPerfil(value, 4)[0], (Helpers.RecortarHtmlPerfil(value, 4)[1] - Helpers.RecortarHtmlPerfil(value, 4)[0]));
                    //string newContexto = HttpConn.GetContext(url);
                    //curso = newContexto.Substring(Helpers.RecortarHtmlPerfil(newContexto, 5)[0], (Helpers.RecortarHtmlPerfil(newContexto, 5)[1] - Helpers.RecortarHtmlPerfil(newContexto, 5)[0]));

                }
                catch (ArgumentOutOfRangeException e)
                {
                    throw new InvalidDocumentParseException("Não foi possivel converter os dados de entrada", e.InnerException);
                }
                catch (Exception e)
                {
                    throw new InvalidDocumentParseException("Não foi possivel carregar o perfil do aluno", e.InnerException);
                }

                return new Perfil() { Nome = nome, Curso = curso };
            }
        }
    }
}