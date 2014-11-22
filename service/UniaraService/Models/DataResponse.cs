using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniaraService.Models
{
    public class DataResponse
    {
        public string MessageResult { get; set; }
        public int StatusCodeResult { get; set; }
        public string RequestDate { get; set; }
        public object DataResult { get; set; }
    }
}