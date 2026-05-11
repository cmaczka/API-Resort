using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IService;
using MAtgicVilla_Utilidad;
using Newtonsoft.Json;
using System.Text;

namespace MagicVilla_Web.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse ResponseModel { get; set; }
        public IHttpClientFactory HttpClient { get; set; }

        public BaseService(IHttpClientFactory httpClient)
        {
            HttpClient = httpClient;
            ResponseModel = new APIResponse();
        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client = HttpClient.CreateClient("MagicAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
                }
                switch (apiRequest.APIType)
                {
                    case DS.APITipo.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case DS.APITipo.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case DS.APITipo.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
                HttpResponseMessage apiResponse = await client.SendAsync(message);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var apiResponseDTO = JsonConvert.DeserializeObject<APIResponse>(apiContent);

                // 1) Intentar leer wrapper
                var wrapper = JsonConvert.DeserializeObject<APIResponse>(apiContent);

                // 2) Si el caller pidió APIResponse, devolvés el wrapper completo
                if (typeof(T) == typeof(APIResponse))
                    return (T)(object)wrapper;

                // 3) Si wrapper OK y exitoso: devolvés Resultado como T

                if (wrapper != null && wrapper.IsExitoso)
                    return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(wrapper.Resultado));


                //4) Si no fue exitoso: devolvés el wrapper como T(por si T es APIResponse o compatible)
                return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(wrapper));


                //if (apiResponseDTO != null && apiResponseDTO.IsExitoso)
                //{
                //    var apiResult = JsonConvert.SerializeObject(apiResponseDTO.Resultado);
                //    var apiResultDTO = JsonConvert.DeserializeObject<T>(apiResult);
                //    return apiResultDTO;
                //}
                //else
                //{
                //    var apiResult = JsonConvert.SerializeObject(apiResponseDTO);
                //    var apiResultDTO = JsonConvert.DeserializeObject<T>(apiResult);
                //    return apiResultDTO;
                //}
            }
            catch (Exception ex)
            {
                var dto=new APIResponse
                {
                    IsExitoso=false,
                    Errores = new List<string> { "Error en la comunicación con la API " + Convert.ToString(ex.Message)}
                };
                var apiResult = JsonConvert.SerializeObject(dto);
                var apiResultDTO = JsonConvert.DeserializeObject<T>(apiResult);
                return apiResultDTO;
            }
        }
    }
}
