using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniaraService.Core.Html.Utils
{
    public class DocumentTrim
    {
        /// <summary>
        /// Busca e retorna as posições das tags no contexto retornado pelo servidor
        /// </summary>
        /// <param name="contexto">Dados do servidor, depois de validados</param>
        /// <returns>Array das posições</returns>
        public static int[] RecortarHtml(string contexto)
        {
            int inicio = Convert.ToInt32(contexto.IndexOf("<div id='conteudo'></div>") + 26);
            int fim = Convert.ToInt32(contexto.IndexOf("<div id=\"limite2\"></div>"));

            return new int[2] { inicio, fim };
        }

        /// <summary>
        /// busca e retorna as posições das tags no contexto retornado pelo servidor
        /// </summary>
        /// <param name="contexto">Dados do servidor, depois de validado</param>
        /// <param name="op">Opção de busca</param>
        /// <returns>Array de posições</returns>
        public static int[] RecortarHtmlPerfil(string contexto, int op)
        {
            int inicio = 0;
            int fim = 0;

            if (op == 1)
            {
                inicio = Convert.ToInt32(contexto.IndexOf("<b>Bom Dia,") + 12);
                fim = 20;
            }
            if (op == 2)
            {
                inicio = Convert.ToInt32(contexto.IndexOf("<b>Boa Tarde,") + 14);
                fim = 20;
            }
            if (op == 3)
            {
                inicio = Convert.ToInt32(contexto.IndexOf("<b>Boa Noite,") + 14);
                fim = 20;
            }
            if (op == 4)
            {
                inicio = Convert.ToInt32(contexto.IndexOf("menu3[4]='<a href=\"") + 19);
                fim = Convert.ToInt32(contexto.IndexOf("\" class=dropmenul target=_blank>Meu Curso</a>'"));
            }
            if (op == 5)
            {
                inicio = Convert.ToInt32(contexto.IndexOf("<title>Uniara | ") + 16);
                fim = Convert.ToInt32(contexto.IndexOf(" | Página Principal</title>"));
            }

            return new int[2] { inicio, fim };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contexto"></param>
        /// <returns></returns>
        public static int[] RecortarHtmlHorarioAulas(string contexto)
        {
            int inicio = Convert.ToInt32(contexto.IndexOf("<div id='conteudo'></div>") + 26);
            int fim = Convert.ToInt32(contexto.IndexOf("<div id=\"limite2\"></div>"));

            return new int[2] { inicio, fim };
        }
    }
}
