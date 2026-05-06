using static MAtgicVilla_Utilidad.DS;

namespace MagicVilla_Web.Models
{
    public class APIRequest
    {
        public APITipo APIType { get; set; } = APITipo.GET;
        public string Url { get; set; }
        public object? Data { get; set; }
    }

}
