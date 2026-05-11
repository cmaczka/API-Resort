using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IService;
using MAtgicVilla_Utilidad;

namespace MagicVilla_Web.Services
{
    public class VillaService : BaseService, IVillaService
    {
        public readonly IHttpClientFactory _httpClientFactory;
        private string _villaUrl;
        public VillaService(IHttpClientFactory httpClientFactory, IConfiguration configuration):base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _villaUrl = configuration.GetValue<string>("ServiceUrls:API_URL");
        }

        public Task<T> CreateAsync<T>(VillaCreateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = DS.APITipo.POST,
                Data = dto,
                Url = _villaUrl + "api/Villa"
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = DS.APITipo.DELETE,
                Url = _villaUrl + "api/Villa/" + id
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = DS.APITipo.GET,
                Url = _villaUrl + "api/Villa"
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = DS.APITipo.GET,
                Url = _villaUrl + "api/Villa/" + id
            });
        }

        public Task<T> UpdateAsync<T>(VillaUpdateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = DS.APITipo. PUT,
                Data = dto,
                Url = _villaUrl + "api/Villa/" + dto.Id
            });
        }
    }
}
