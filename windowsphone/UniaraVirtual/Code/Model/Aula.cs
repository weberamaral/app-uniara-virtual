using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniaraVirtual.Code.Model
{
    public class Aula
    {
        /// <summary>
        /// nome da disciplina
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// dia da semanada em que a disciplia é ministrada
        /// </summary>
        public string DiaSemana { get; set; }

        /// <summary>
        /// hora de inciio
        /// </summary>
        public string HoraInicial { get; set; }

        /// <summary>
        /// hora de termino
        /// </summary>
        public string HoraFinal { get; set; }

        /// <summary>
        /// sala 
        /// </summary>
        public string Sala { get; set; }
    }
}
