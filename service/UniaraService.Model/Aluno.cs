using System;
using System.Collections.Generic;

namespace UniaraService.Model
{
    public class Aluno
    {
        public Perfil Perfil
        {
            get;
            set;
        }
        public List<Disciplina> Disciplinas
        {
            get;
            set;
        }
        public List<Aula> Aulas
        {
            get;
            set;
        }
    }
}