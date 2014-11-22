
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using UniaraService.Core.Html.Converters;
using UniaraService.Core.Html.Exceptions;
using UniaraService.Core.Html.Utils;
using UniaraService.Core.Html.Validators;
using UniaraService.Model;

namespace UniaraService.Html.Core.Converters.Actions
{
    internal class FaltasConverter : AbstractConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDocumentException"
        /// <exception cref="InvalidDocumentParseException"
        public List<Falta> ConvertTo(string value)
        {
            List<Falta> faltas = new List<Falta>();
            HtmlDocument htmlDoc = new HtmlDocument();

            try
            {
                if (DocumentValidator.ValidarHtmlDoc(value))
                {
                    string dadosEmHtml = value.Substring(DocumentTrim.RecortarHtml(value)[0], (DocumentTrim.RecortarHtml(value)[1] - DocumentTrim.RecortarHtml(value)[0]));
                    htmlDoc.LoadHtml(dadosEmHtml);
                    HtmlNodeCollection tr = htmlDoc.DocumentNode.SelectNodes("/table[1]/tr");

                    for (int i = 0; i < tr.Count; i++)
                    {
                        // Seleciona o conteudo de cada disciplina TAG
                        // Remove as tags indesejadas do cód da TD selecionada
                        HtmlNodeCollection td = tr[i].ChildNodes;
                        for (int j = 0; j < td.Count; j++)
                        {
                            if (td[j].NodeType != HtmlNodeType.Element)
                                td.RemoveAt(j);
                        }

                        if (i > 0)
                        {

                            // Preenche cada falta
                            Falta falta = new Falta()
                            {
                                Nome = td[1].InnerText,
                                CargaHoraria = td[2].InnerText,
                                Faltas = td[4].InnerText,
                                Frequencia = td[3].InnerText,
                                Atualizacao = td[5].InnerText
                            };

                            faltas.Add(falta);
                        }
                    }

                    return faltas;
                }
                else
                {
                    throw new InvalidDocumentException("Dados de entrada inválidos");
                }
            }
            catch (Exception e)
            {
                throw new InvalidDocumentParseException("Não foi possivel converter os dados de entrada", e.InnerException);
            }
        }
    }
}