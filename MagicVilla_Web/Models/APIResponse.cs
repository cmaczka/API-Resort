using System.Net;

namespace MagicVilla_Web.Models
{
    public class APIResponse
    {
        public APIResponse() { }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsExitoso { get; set; } = true;
        public List<string> Errores { get; set; }
        public object Resultado { get; set; }

    }
}
