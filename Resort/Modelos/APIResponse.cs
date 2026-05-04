using System.Net;

namespace Resort.Modelos
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
