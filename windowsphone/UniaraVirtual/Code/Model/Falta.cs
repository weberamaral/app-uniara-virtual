using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniaraVirtual.Code.Model
{
    public class Falta
    {
        /// <summary>
        /// representa o nomes do aluno
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// presenta a data de atualização da frequencia desta disciplina
        /// </summary>
        public string Atualizacao { get; set; }

        /// <summary>
        /// representa a carga horária da disciplina
        /// </summary>
        public string CargaHoraria { get; set; }

        /// <summary>
        /// representa o percentual de frequencia da disciplina
        /// </summary>
        public string Frequencia { get; set; }

        /// <summary>
        /// representa a quantidade total de faltas da disciplina
        /// </summary>
        public string Faltas { get; set; }
    }
}
