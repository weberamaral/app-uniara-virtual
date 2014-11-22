using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniaraService.Core.Html.Exceptions
{
    public class InvalidDocumentException : Exception
    {
        public InvalidDocumentException(string message) : base(message) { }
        public InvalidDocumentException(string message, Exception innerException) : base(message, innerException) { }
    }
}