using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UniaraService.App;
using UniaraService.Core.Html.Exceptions;
using UniaraService.Html.Core.Converters.Actions;
using UniaraService.Model;

namespace UniaraService.Core.Http
{
    public class Uniara
    {
        #region Atributos

        private HttpConn httpConn;
        private bool isLogado;
        private CookieCollection cookies;

        #endregion

        #region Propriedades

        public bool IsLogado
        {
            get { return isLogado; }
        }

        public CookieCollection Cookies
        {
            get { return cookies; }
        }

        #endregion

        #region CTOR

        public Uniara()
        {
            httpConn = new HttpConn();
            cookies = new CookieCollection();
        }

        #endregion

        /// <summary>
        /// Recebe a autorização do site uniara.virtual.com.br para as credenciais informadas
        /// </summary>
        /// <param name="username">Matricula do aluno</param>
        /// <param name="password">Senha do aluno</param>
        /// <returns>A autorização (true ou false)</returns>
        public bool GetAuthentication(string username, string password)
        {
            try
            {
                if (!isLogado && httpConn.GetAutorizacao(username, password))
                {
                    cookies = httpConn.Cookies;
                    isLogado = httpConn.IsLogged;
                }

                return isLogado;
            }
            catch (ArgumentNullException e)
            {
                throw new NotAuthenticationException("Verifique sua credencial", e.InnerException);
            }
            catch (Exception e)
            {
                throw new UniaraComunicationException("Ocorreu um problema no servidor", e.InnerException);
            }
        }

        /// <summary>
        /// Recebe um aluno mas apenas com o campo Perfil carregado
        /// </summary>
        /// <returns>Aluno parcialmente carregado</returns>
        public Aluno GetPartialAluno()
        {
            Aluno aluno = new Aluno();

            try
            {
                if (!isLogado)
                {
                    throw new NotAuthenticationException("Não autorizado");
                }

                PerfilConverter converter = new PerfilConverter();
                aluno.Perfil = converter.ConvertTo(httpConn.GetData(AppConstants.INDEX_URL));

                return aluno;
            }
            catch (Exception e)
            {
                throw new UniaraComunicationException("Serviço indisponivel no momento", e.InnerException);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Aluno GetAluno()
        {
            Aluno aluno = new Aluno();

            try
            {
                if (!isLogado)
                {
                    throw new NotAuthenticationException("Não autorizado");
                }

                aluno.Aulas = GetAulas();
                aluno.Disciplinas = GetHistoricoAluno();
                aluno.Perfil = GetPartialAluno().Perfil;

                return aluno;
            }
            catch (Exception e)
            {
                throw new UniaraComunicationException("Serviço indisponivel no momento", e.InnerException);
            }
        }

        /// <summary>
        /// Recebe todas as disciplinas do curso do aluno
        /// </summary>
        /// <returns>Lista de disciplinas</returns>
        public List<Disciplina> GetHistoricoAluno()
        {
            List<Disciplina> disciplinas;

            try
            {
                if (!isLogado)
                {
                    throw new NotAuthenticationException("Não autorizado");
                }

                DisciplinasConverter disciplinasConverter = new DisciplinasConverter();
                FaltasConverter faltasConverter = new FaltasConverter();

                disciplinas = disciplinasConverter.ConvertTo(httpConn.GetData(AppConstants.HISTORICO_URL));
                List<Falta> faltas = faltasConverter.ConvertTo(httpConn.GetData(AppConstants.FALTAS_URL));

                foreach (Disciplina disciplina in disciplinas)
                {
                    if (disciplina.Situacao.ToLower().Equals("cursando"))
                    {
                        disciplina.Frequencia.Frequencia = (from falta in faltas
                                                            where falta.Nome == disciplina.Nome
                                                            select falta.Frequencia).SingleOrDefault();
                        disciplina.Frequencia.Atualizacao = (from falta in faltas
                                                             where falta.Nome == disciplina.Nome
                                                             select falta.Atualizacao).SingleOrDefault();
                    }
                }

                return disciplinas;
            }
            catch (Exception e)
            {
                throw new UniaraComunicationException("Serviço indisponivel no momento", e.InnerException);
            }
        }

        /// <summary>
        /// Lista as disciplinas cursada atualmente
        /// </summary>
        /// <returns>Lista de diisciplinas atuais</returns>
        public List<Disciplina> GetNotasAluno()
        {

            List<Disciplina> disciplinas;

            try
            {
                if (!isLogado)
                {
                    throw new NotAuthenticationException("Não autorizado");
                }

                disciplinas = (from notas in GetHistoricoAluno()
                               where notas.Situacao.ToLower().Equals("cursando")
                               select notas).ToList();

                return disciplinas;
            }
            catch (Exception e)
            {
                throw new UniaraComunicationException("Serviço indisponivel no momento", e.InnerException);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Aula> GetAulas()
        {
            List<Aula> aulas;

            try
            {
                if (!isLogado)
                {
                    throw new NotAuthenticationException("Não autorizado");
                }

                AulasConverter converter = new AulasConverter();
                aulas = converter.ConvertTo(httpConn.GetData(AppConstants.AULAS_URL));
                return aulas;
            }
            catch (Exception e)
            {
                throw new UniaraComunicationException("Serviço indisponivel no momento", e.InnerException);
            }
        }

        /// <summary>
        /// Recebe todas as faltas nas disciplinas cursada atualmente pelo aluno
        /// </summary>
        /// <returns>Lista de disciplinas com as faltas</returns>
        public List<Falta> GetFaltas()
        {

            List<Falta> faltas;

            try
            {
                if (!isLogado)
                {
                    throw new NotAuthenticationException("Não autorizado");
                }

                FaltasConverter converter = new FaltasConverter();
                faltas = converter.ConvertTo(httpConn.GetData(AppConstants.FALTAS_URL));

                return faltas;
            }
            catch (Exception e)
            {
                throw new UniaraComunicationException("Serviço indisponivel no momento", e.InnerException);
            }
        }
    }
}