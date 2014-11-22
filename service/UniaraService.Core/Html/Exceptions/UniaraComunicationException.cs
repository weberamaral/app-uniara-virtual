using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniaraService.Core.Html.Exceptions
{
    public class UniaraComunicationException : Exception
    {
        public UniaraComunicationException(string message) : base(message) { }
        public UniaraComunicationException(string message, Exception innerException) : base(message, innerException) { }
    }
}