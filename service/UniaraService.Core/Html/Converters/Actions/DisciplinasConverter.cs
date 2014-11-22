
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
    internal class DisciplinasConverter : AbstractConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDocumentException"
        /// <exception cref="InvalidDocumentParseException"
        public List<Disciplina> ConvertTo(string value)
        {
            List<Disciplina> disciplinas = new List<Disciplina>();
            HtmlDocument htmlDoc = new HtmlDocument();
            HtmlDocument tooltip = new HtmlDocument();
            string mouseOverAtt;

            try
            {
                if (DocumentValidator.ValidarHtmlDoc(value))
                {
                    // Armazena apenas o conteudo necessário para filtragem HTML
                    string dadosEmHtml = value.Substring(DocumentTrim.RecortarHtml(value)[0], (DocumentTrim.RecortarHtml(value)[1] - DocumentTrim.RecortarHtml(value)[0]));

                    // Cria o node HTML
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
                            // NodeCollection para as tags dentro de <TR><TD>...
                            // Seleção necessária para chegar ao atributo onmouseover
                            HtmlNodeCollection a = td[0].ChildNodes;    // Apenas no primeiro, por existir o att ToolTip

                            for (int j = 0; j < a.Count; j++)
                            {
                                // Seleção para o atributo
                                HtmlAttributeCollection att = a[j].Attributes;

                                // Pega os att da tag A
                                for (int k = 0; k <= a.Count; k++)
                                {
                                    if (att[k].Name.ToLower() == "onmouseover")
                                    {
                                        // Recebe o conteudo do att como uma string
                                        string onmouseover = att[k].Value;
                                        int onmouseoverLenght = onmouseover.Length;

                                        // Novo contexto HTML filtrado
                                        mouseOverAtt = onmouseover.Substring(9, (onmouseoverLenght - 9 - 7));
                                        tooltip.LoadHtml(mouseOverAtt);
                                        HtmlNodeCollection tooltipNodes = tooltip.DocumentNode.SelectNodes("//tr");
                                        HtmlNodeCollection tooltipChildNodes = tooltipNodes[3].ChildNodes;

                                        Disciplina disciplina = new Disciplina()
                                        {
                                            // nome do aluno
                                            Nome = td[0].InnerText,
                                            // inicializa a frequencia
                                            Frequencia = new Falta()
                                            {
                                                CargaHoraria = td[1].InnerText,
                                                Frequencia = td[5].InnerText,
                                                Faltas = tooltipChildNodes[8].InnerText
                                            },
                                            // motivo
                                            Motivo = td[8].InnerText,
                                            // inicia as notas
                                            Notas = new Nota()
                                            {
                                                Nota1 = tooltipChildNodes[0].InnerText,
                                                Nota2 = tooltipChildNodes[1].InnerText,
                                                Nota3 = tooltipChildNodes[2].InnerText,
                                                Nota4 = tooltipChildNodes[3].InnerText,
                                                Sub = tooltipChildNodes[4].InnerText,
                                                Media = td[6].InnerText,
                                                Exame = tooltipChildNodes[6].InnerText
                                            },
                                            Periodo = td[3].InnerText,
                                            Situacao = td[7].InnerText,
                                            Turma = td[4].InnerText,
                                            Ano = td[2].InnerText

                                        };

                                        disciplinas.Add(disciplina);
                                    }
                                }
                            }
                        }
                    }
                    return disciplinas;
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