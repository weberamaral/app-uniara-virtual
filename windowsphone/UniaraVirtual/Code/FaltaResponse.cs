using System.Collections.Generic;
using UniaraVirtual.Code.Model;

namespace UniaraVirtual.Code
{
    public class FaltaResponse
    {
        public string MessageResult { get; set; }
        public string RequestDate { get; set; }
        public int StatusCodeResult { get; set; }
        public List<Falta> DataResult { get; set; }
    }
}
