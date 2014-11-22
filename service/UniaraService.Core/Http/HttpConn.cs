using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using UniaraService.App;

namespace UniaraService.Core.Http
{
    internal class HttpConn
    {
        #region Atributos/Propriedas

        private const string Charset = "iso-8859-1";

        private byte[] _requestByte;

        private CookieCollection _cookies;
        /// <summary>
        /// Recebe os Cookies retorna do servidor
        /// </summary>
        public CookieCollection Cookies
        {
            get { return _cookies; }
        }

        private bool _isLogged;
        /// <summary>
        /// Recebe se o aluno está logado
        /// </summary>
        public bool IsLogged
        {
            get { return _isLogged; }
        }

        #endregion

        #region CTOR

        public HttpConn()
        {
            _cookies = new CookieCollection();
        }

        #endregion

        public bool GetAutorizacao(string username, string password)
        {
            if (_isLogged)
            {
                throw new Exception("Você já esta logado");
            }

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            string postParameters = string.Format("username={0}&senha={1}", username, password);
            _isLogged = false;

            try
            {
                HttpWebRequest request = CreateRequest(AppConstants.LOGIN_URL, postParameters);
                HttpWebResponse response = GetResponse(request);
                _cookies = response.Cookies;
                _isLogged = ValidateCookies();
                response.Close();

                return _isLogged;
            }
            catch (WebException ex)
            {
                throw new WebException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GetData(string url)
        {
            string result = string.Empty;

            if (!_isLogged)
            {
                throw new Exception("Não autorizado");
            }
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("Parametro Url vazio");
            }

            try
            {
                HttpWebRequest request = CreateRequest(url);
                request.CookieContainer.Add(_cookies);

                HttpWebResponse respose = (HttpWebResponse)request.GetResponse();
                using (Stream dataStream = respose.GetResponseStream())
                {
                    using (StreamReader readerStream = new StreamReader(dataStream, Encoding.GetEncoding(Charset)))
                    {
                        result = readerStream.ReadToEnd();
                    }
                }

                respose.Close();
                return result;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (WebException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region auxiliares

        private HttpWebRequest CreateRequest(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("Url inválida ou vazia");
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = new CookieContainer();
            request.Timeout = 15 * 1000;

            return request;
        }

        private HttpWebRequest CreateRequest(string url, string requestBody)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("Url inválida ou vazia");
            }
            if (string.IsNullOrEmpty(requestBody))
            {
                throw new ArgumentNullException("Parametros de postagem inválido ou vazio");
            }

            _requestByte = Encoding.GetEncoding(Charset).GetBytes(requestBody);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = new CookieContainer();
            request.Timeout = 15 * 1000;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = _requestByte.Length;

            return request;
        }

        private HttpWebResponse GetResponse(HttpWebRequest request)
        {
            try
            {
                if (request.Method == "POST")
                {
                    Stream writeStream = request.GetRequestStream();
                    writeStream.Write(_requestByte, 0, _requestByte.Length);
                    writeStream.Close();
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                
                // Adiciono os cookies retornado do servidor
                if (request.Method == "POST")
                    _cookies = response.Cookies;

                response.Close();
                return response;
            }
            catch (WebException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool ValidateCookies()
        {
            foreach (Cookie cook in _cookies)
                if (cook.Name.ToLower() == "uvxs233e3" && cook.Value.ToLower() == "s")
                    return true;

            return false;
        }

        #endregion

        public static string GetContext(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = new CookieContainer();
            request.Timeout = 15 * 1000;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                using (StreamReader readerStream = new StreamReader(dataStream, Encoding.GetEncoding(Charset)))
                {
                    return readerStream.ReadToEnd();
                }
            }
        }
    }
}