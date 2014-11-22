
using UniaraVirtual.Code.Model;
namespace UniaraVirtual.Code
{
    public class PerfilResponse
    {
        public string MessageResult { get; set; }
        public string RequestDate { get; set; }
        public int StatusCodeResult { get; set; }
        public Perfil DataResult { get; set; }
    }
}
