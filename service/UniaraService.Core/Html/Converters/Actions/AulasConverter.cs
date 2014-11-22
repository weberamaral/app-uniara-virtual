
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
    class AulasConverter : AbstractConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDocumentException"
        /// <exception cref="InvalidDocumentParseException"
        public List<Aula> ConvertTo(string value)
        {
            List<Aula> horarios = new List<Aula>();
            HtmlDocument htmlDoc = new HtmlDocument();
            string tempSemana = string.Empty;

            try
            {
                if (DocumentValidator.ValidaHtmlHorarioAulas(value))
                {
                    string dadosEmHtml = value.Substring(DocumentTrim.RecortarHtmlHorarioAulas(value)[0], (DocumentTrim.RecortarHtmlHorarioAulas(value)[1] - DocumentTrim.RecortarHtmlHorarioAulas(value)[0]));
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

                            Aula aula = new Aula()
                            {
                                DiaSemana = base.ToDayOfWeek(td[0].InnerText),
                                HoraInicial = td[1].InnerText,
                                HoraFinal = td[2].InnerText,
                                Nome = td[3].InnerText,
                                Sala = td[4].InnerText
                            };

                            horarios.Add(aula);
                        }
                    }
                }
                else
                {
                    throw new InvalidDocumentException("Dados de entrada inválidos");
                }
                return horarios;
            }
            catch (Exception e)
            {
                throw new InvalidDocumentParseException("Não foi possivel converter os dados de entrada", e.InnerException);
            }
        }
    }
}