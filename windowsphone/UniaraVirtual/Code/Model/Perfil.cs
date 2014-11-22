using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniaraVirtual.Code.Model
{
    public class Perfil
    {
        /// <summary>
        /// Representa o nome do aluno
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// REpresenta o curso do aluno
        /// </summary>
        /// remarks : Alguns cursos estão com problemas
        public string Curso { get; set; }
    }
}
