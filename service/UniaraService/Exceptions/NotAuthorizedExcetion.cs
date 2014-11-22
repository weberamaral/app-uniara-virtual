using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniaraService.Exceptions
{
    public class NotAuthorizedExcetion : Exception
    {
        public NotAuthorizedExcetion(string message) : base(message) { }
        public NotAuthorizedExcetion(string message, Exception innerException) : base(message, innerException) { }
    }
}