using System.Collections.Generic;
using UniaraVirtual.Code.Model;

namespace UniaraVirtual.Code
{
    public class DisciplinaResponse
    {
        public string MessageResult { get; set; }
        public string RequestDate { get; set; }
        public int StatusCodeResult { get; set; }
        public List<Disciplina> DataResult { get; set; }
    }
}
