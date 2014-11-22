using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniaraService.Model;

namespace UniaraService.Service
{
    public interface IAlunoService
    {
        Perfil GetPerfilAluno(string username, string password);
        List<Disciplina> GetNotas(string username, string password);
        List<Aula> GetAulas(string username, string password);
        List<Disciplina> GetHistorico(string username, string password);
        List<Falta> GetFaltas(string username, string password);
        Aluno GetFullAluno(string username, string password);
    }
}
