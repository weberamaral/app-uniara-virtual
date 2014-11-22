using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace UniaraService.Model
{
    public class Aula
    {
        public string Nome { get; set; }
        public string DiaSemana { get; set; }
        public string HoraInicial { get; set; }
        public string HoraFinal { get; set; }
        public string Sala { get; set; }
    }
}