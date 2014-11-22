using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniaraService.Core.Html.Exceptions
{
    public class NotAuthenticationException : Exception
    {
        public NotAuthenticationException(string message) : base(message) { }
        public NotAuthenticationException(string message, Exception innerException) : base(message, innerException) { }
    }
}