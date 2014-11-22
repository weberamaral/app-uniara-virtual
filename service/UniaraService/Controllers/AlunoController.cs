using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Services.Description;
using UniaraService.App;
using UniaraService.Exceptions;
using UniaraService.Models;
using UniaraService.Service;

namespace UniaraService.Controllers
{
    public class AlunoController : ApiController
    {
        private readonly IAlunoService service;

        public AlunoController(IAlunoService service)
        {
            this.service = service;
        }

        //=========
        // Actions
        //==========

        [HttpGet]
        public HttpResponseMessage All()
        {
            string username = string.Empty;
            string password = string.Empty;

            try
            {
                this.ExtractCredentials(ref username, ref password);

                return Request.CreateResponse<DataResponse>(HttpStatusCode.OK, new DataResponse()
                    {
                        MessageResult = AppConstants.REQUEST_OK,
                        StatusCodeResult = 200,
                        RequestDate = DateTime.UtcNow.ToString(),
                        DataResult = service.GetFullAluno(username, password)
                    });
            }
            catch(NotAuthorizedExcetion e)
            {
                return Request.CreateResponse<DataResponse>(HttpStatusCode.OK, new DataResponse()
                    {
                        DataResult = null,
                        RequestDate = DateTime.UtcNow.ToString(),
                        MessageResult = e.Message,
                        StatusCodeResult = 403
                    });
            }
            catch(ServiceException e)
            {
                return Request.CreateResponse<DataResponse>(HttpStatusCode.OK, new DataResponse()
                {
                    DataResult = null,
                    RequestDate = DateTime.UtcNow.ToString(),
                    MessageResult = e.Message,
                    StatusCodeResult = 500
                });
            }
        }

        [HttpGet]
        public HttpResponseMessage Notas()
        {
            string username = string.Empty;
            string password = string.Empty;

            try
            {
                this.ExtractCredentials(ref username, ref password);

                return Request.CreateResponse<DataResponse>(HttpStatusCode.OK, new DataResponse()
                {
                    RequestDate = DateTime.UtcNow.ToString(),
                    DataResult = service.GetNotas(username, password),
                    MessageResult = AppConstants.REQUEST_OK,
                    StatusCodeResult = 200
                });
            }
            catch (NotAuthorizedExcetion e)
            {
                return Request.CreateResponse<DataResponse>(HttpStatusCode.OK, new DataResponse()
                {
                    DataResult = null,
                    RequestDate = DateTime.UtcNow.ToString(),
                    MessageResult = e.Message,
                    StatusCodeResult = 403
                });
            }
            catch (ServiceException e)
            {
                return Request.CreateResponse<DataResponse>(HttpStatusCode.OK, new DataResponse()
                {
                    DataResult = null,
                    RequestDate = DateTime.UtcNow.ToString(),
                    MessageResult = e.Message,
                    StatusCodeResult = 500
                });
            }
        }

        [HttpGet]
        public HttpResponseMessage Faltas()
        {
            string username = string.Empty;
            string password = string.Empty;

            try
            {
                this.ExtractCredentials(ref username, ref password);

                return Request.CreateResponse<DataResponse>(HttpStatusCode.OK, new DataResponse()
                {
                    RequestDate = DateTime.UtcNow.ToString(),
                    DataResult = service.GetFaltas(username, password),
                    MessageResult = AppConstants.REQUEST_OK,
                    StatusCodeResult = 200
                });
            }
            catch (NotAuthorizedExcetion e)
            {
                return Request.CreateResponse<DataResponse>(HttpStatusCode.OK, new DataResponse()
                {
                    DataResult = null,
                    RequestDate = DateTime.UtcNow.ToString(),
                    MessageResult = e.Message,
                    StatusCodeResult = 403
                });
            }
            catch (ServiceException e)
            {
                return Request.CreateResponse<DataResponse>(HttpStatusCode.OK, new DataResponse()
                {
                    DataResult = null,
                    RequestDate = DateTime.UtcNow.ToString(),
                    MessageResult = e.Message,
                    StatusCodeResult = 500
                });
            }
        }

        [HttpGet]
        public HttpResponseMessage Aulas()
        {
            string username = string.Empty;
            string password = string.Empty;

            try
            {
                this.ExtractCredentials(ref username, ref password);

                return Request.CreateResponse<DataResponse>(HttpStatusCode.OK, new DataResponse()
                {
                    RequestDate = DateTime.UtcNow.ToString(),
                    DataResult = service.GetAulas(username, password),
                    MessageResult = AppConstants.REQUEST_OK,
                    StatusCodeResult = 200
                });
            }
            catch (NotAuthorizedExcetion e)
            {
                return Request.CreateResponse<DataResponse>(HttpStatusCode.OK, new DataResponse()
                {
                    DataResult = null,
                    RequestDate = DateTime.UtcNow.ToString(),
                    MessageResult = e.Message,
                    StatusCodeResult = 403
                });
            }
            catch (ServiceException e)
            {
                return Request.CreateResponse<DataResponse>(HttpStatusCode.OK, new DataResponse()
                {
                    DataResult = null,
                    RequestDate = DateTime.UtcNow.ToString(),
                    MessageResult = e.Message,
                    StatusCodeResult = 500
                });
            }
        }

        [HttpGet]
        public HttpResponseMessage Historico()
        {
            string username = string.Empty;
            string password = string.Empty;

            try
            {
                this.ExtractCredentials(ref username, ref password);

                return Request.CreateResponse<DataResponse>(HttpStatusCode.OK, new DataResponse()
                {
                    RequestDate = DateTime.UtcNow.ToString(),
                    DataResult = service.GetHistorico(username, password),
                    MessageResult = AppConstants.REQUEST_OK,
                    StatusCodeResult = 200
                });
            }
            catch (NotAuthorizedExcetion e)
            {
                return Request.CreateResponse<DataResponse>(HttpStatusCode.OK, new DataResponse()
                {
                    DataResult = null,
                    RequestDate = DateTime.UtcNow.ToString(),
                    MessageResult = e.Message,
                    StatusCodeResult = 403
                });
            }
            catch (ServiceException e)
            {
                return Request.CreateResponse<DataResponse>(HttpStatusCode.OK, new DataResponse()
                {
                    DataResult = null,
                    RequestDate = DateTime.UtcNow.ToString(),
                    MessageResult = e.Message,
                    StatusCodeResult = 500
                });
            }
        }

        [HttpGet]
        public HttpResponseMessage Perfil()
        {
            string username = string.Empty;
            string password = string.Empty;

            try
            {
                this.ExtractCredentials(ref username, ref password);

                return Request.CreateResponse<DataResponse>(HttpStatusCode.OK, new DataResponse()
                {
                    RequestDate = DateTime.UtcNow.ToString(),
                    DataResult = service.GetPerfilAluno(username, password),
                    MessageResult = AppConstants.REQUEST_OK,
                    StatusCodeResult = 200
                });
            }
            catch (NotAuthorizedExcetion e)
            {
                return Request.CreateResponse<DataResponse>(HttpStatusCode.OK, new DataResponse()
                {
                    DataResult = null,
                    RequestDate = DateTime.UtcNow.ToString(),
                    MessageResult = e.Message,
                    StatusCodeResult = 403
                });
            }
            catch (ServiceException e)
            {
                return Request.CreateResponse<DataResponse>(HttpStatusCode.OK, new DataResponse()
                {
                    DataResult = null,
                    RequestDate = DateTime.UtcNow.ToString(),
                    MessageResult = e.Message,
                    StatusCodeResult = 500
                });
            }
        }

        //=============
        // Auxiliares
        //=============

        private void ExtractCredentials(ref string username, ref string password)
        {
            var auth = this.Request.Headers.Authorization;
            if(auth == null || string.IsNullOrEmpty(auth.Parameter))
            {
                throw new NotAuthorizedExcetion(AppConstants.NOT_AUTHRIZED);
            }

            string decoded = Encoding.Default.GetString(Convert.FromBase64String(auth.Parameter));
            int separator = decoded.IndexOf(":");

            username = decoded.Substring(0, separator);
            password = decoded.Substring(separator + 1);
        }
    }
}
