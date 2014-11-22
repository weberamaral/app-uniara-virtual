using System;

namespace UniaraService.Model
{
    public class Disciplina
    {
        public Falta Frequencia { get; set; }
        public Nota Notas { get; set; }
        public string Ano { get; set; }
        public string Nome { get; set; }
        public string Motivo { get; set; }
        public string Situacao { get; set; }
        public string Periodo { get; set; }
        public string Turma { get; set; }
    }
}