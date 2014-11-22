using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniaraVirtual.Code.Model
{
    public class Aluno
    {
        /// <summary>
        /// perfil do aluno
        /// </summary>
        public Perfil Perfil { get; set; }

        /// <summary>
        /// lista de disciplinas cursadas pelo aluno
        /// </summary>
        public List<Disciplina> Disciplinas { get; set; }

        /// <summary>
        /// lista de aulas cursadas atualmente pelo aluno
        /// </summary>
        public List<Aula> Aulas { get; set; }
    }
}
