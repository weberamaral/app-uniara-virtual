using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniaraVirtual.Code.Model
{
    public class Nota
    {
        /// <summary>
        /// Representa o nome do aluno
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// REpresenta a nota do primeiro bimestre
        /// </summary>
        public string Nota1 { get; set; }

        /// <summary>
        /// Representa a nota do segundo bimestre
        /// </summary>
        public string Nota2 { get; set; }

        /// <summary>
        /// representa a nota do terceiro bimestre
        /// </summary>
        public string Nota3 { get; set; }

        /// <summary>
        /// representa a nota do quarto bimestre
        /// </summary>
        public string Nota4 { get; set; }

        /// <summary>
        /// representa a nota da quinta prova
        /// </summary>
        public string Sub { get; set; }

        /// <summary>
        /// representa a nota de exame
        /// </summary>
        public string Exame { get; set; }

        /// <summary>
        /// representa a media da disciplina
        /// </summary>
        public string Media { get; set; }
    }
}
