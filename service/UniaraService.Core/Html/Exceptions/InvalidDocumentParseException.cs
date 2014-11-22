using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniaraService.Core.Html.Exceptions
{
    public class InvalidDocumentParseException : Exception
    {
        public InvalidDocumentParseException(string message) : base(message) { }
        public InvalidDocumentParseException(string message, Exception innerException) : base(message, innerException) { }
    }
}