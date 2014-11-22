using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniaraVirtual.Code.Model
{
    public class Disciplina
    {
        /// <summary>
        /// representa a frequencia do aluno para a disciplina
        /// </summary>
        public Falta Frequencia { get; set; }

        /// <summary>
        /// notas bimestrais da disciplina
        /// </summary>
        public Nota Notas { get; set; }

        /// <summary>
        /// ano que a disciplina foi cursada
        /// </summary>
        public string Ano { get; set; }

        /// <summary>
        /// nome da disciplina
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// observação
        /// </summary>
        public string Motivo { get; set; }

        /// <summary>
        /// situação atual da disciplina
        /// </summary>
        public string Situacao { get; set; }

        /// <summary>
        /// peridiodo cursado da disciplina
        /// </summary>
        public string Periodo { get; set; }

        /// <summary>
        /// turma
        /// </summary>
        public string Turma { get; set; }
    }
}
