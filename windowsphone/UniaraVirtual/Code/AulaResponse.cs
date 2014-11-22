using System.Collections.Generic;
using UniaraVirtual.Code.Model;

namespace UniaraVirtual.Code
{
    public class AulaResponse
    {
        public string MessageResult { get; set; }
        public string RequestDate { get; set; }
        public int StatusCodeResult { get; set; }
        public List<Aula> DataResult { get; set; }
    }
}
