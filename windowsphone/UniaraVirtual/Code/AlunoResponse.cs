using System.Collections.Generic;
using UniaraVirtual.Code.Model;

namespace UniaraVirtual.Code
{
    public class AlunoResponse
    {
        public string MessageResult { get; set; }
        public string RequestDate { get; set; }
        public int StatusCodeResult { get; set; }
        public Aluno DataResult { get; set; }
    }
}
