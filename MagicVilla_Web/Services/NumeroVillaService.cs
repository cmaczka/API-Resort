using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IService;
using MAtgicVilla_Utilidad;

namespace MagicVilla_Web.Services
{
    public class NumeroVillaService : BaseService, INumeroVillaService
    {
        public readonly IHttpClientFactory _httpClientFactory;
        private string _numeroVillaUrl;
        public NumeroVillaService(IHttpClientFactory httpClientFactory, IConfiguration configuration):base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _numeroVillaUrl = configuration.GetValue<string>("ServiceUrls:API_URL");
        }

        public Task<T> CreateAsync<T>(NumeroVillaCreateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = DS.APITipo.POST,
                Data = dto,
                Url = _numeroVillaUrl + "api/NumeroVilla"
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = DS.APITipo.DELETE,
                Url = _numeroVillaUrl + "api/NumeroVilla/" + id
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = DS.APITipo.GET,
                Url = _numeroVillaUrl + "api/NumeroVilla"
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = DS.APITipo.GET,
                Url = _numeroVillaUrl + "api/NumeroVilla/" + id
            });
        }

        public Task<T> UpdateAsync<T>(NumeroVillaUpdateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                APIType = DS.APITipo. PUT,
                Data = dto,
                Url = _numeroVillaUrl + "api/NumeroVilla/" + dto.VillaId
            });
        }
    }
}
