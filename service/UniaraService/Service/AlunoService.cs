using System;
using System.Collections.Generic;
using UniaraService.App;
using UniaraService.Core.Http;
using UniaraService.Exceptions;
using UniaraService.Model;

namespace UniaraService.Service
{
    public class AlunoService : IAlunoService
    {
        public Perfil GetPerfilAluno(string username, string password)
        {
            Uniara ctx = new Uniara();
            Perfil perfil = null;
            bool isLogged = false;

            try
            {
                isLogged = ctx.GetAuthentication(username, password);
                if(isLogged)
                {
                    perfil = ctx.GetPartialAluno().Perfil;
                }
            }
            catch(Exception ex)
            {
                throw new ServiceException(ex.Message);
            }

            if(!isLogged)
            {
                throw new NotAuthorizedExcetion(AppConstants.INVALID_CREDENTIAL); 
            }

            return perfil;

        }

        public List<Disciplina> GetNotas(string username, string password)
        {
            Uniara ctx = new Uniara();
            List<Disciplina> disciplinas = null;
            bool isLogged = false;

            try
            {
                isLogged = ctx.GetAuthentication(username, password);
                if (isLogged)
                {
                    disciplinas = ctx.GetNotasAluno();
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }

            if(!isLogged)
            {
                throw new NotAuthorizedExcetion(AppConstants.INVALID_CREDENTIAL);
            }

            return disciplinas;
        }

        public List<Aula> GetAulas(string username, string password)
        {
            Uniara ctx = new Uniara();
            List<Aula> aulas = null;
            bool isLogged = false;

            try
            {
                isLogged = ctx.GetAuthentication(username, password);
                if (isLogged)
                {
                    aulas = ctx.GetAulas();
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }

            if(!isLogged)
            {
                throw new NotAuthorizedExcetion(AppConstants.INVALID_CREDENTIAL);
            }

            return aulas;
        }

        public List<Disciplina> GetHistorico(string username, string password)
        {
            Uniara ctx = new Uniara();
            List<Disciplina> discplinas = null;
            bool isLogged = false;

            try
            {
                isLogged = ctx.GetAuthentication(username, password);
                if (isLogged)
                {
                    discplinas = ctx.GetHistoricoAluno();
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }

            if(!isLogged)
            {
                throw new NotAuthorizedExcetion(AppConstants.INVALID_CREDENTIAL);
            }

            return discplinas;
        }

        public List<Falta> GetFaltas(string username, string password)
        {
            Uniara ctx = new Uniara();
            List<Falta> faltas = null;
            bool isLogged = false;

            try
            {
                isLogged = ctx.GetAuthentication(username, password);
                if (isLogged)
                {
                    faltas = ctx.GetFaltas();
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }

            if(!isLogged)
            {
                throw new NotAuthorizedExcetion(AppConstants.INVALID_CREDENTIAL);
            }

            return faltas;
        }

        public Aluno GetFullAluno(string username, string password)
        {
            Uniara ctx = new Uniara();
            Aluno aluno = null;
            bool isLogged = false;

            try
            {
                isLogged = ctx.GetAuthentication(username, password);
                if (isLogged)
                {
                    aluno = ctx.GetAluno();
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }

            if(!isLogged)
            {
                throw new NotAuthorizedExcetion(AppConstants.INVALID_CREDENTIAL);
            }

            return aluno;
        }
    }
}