using MagicVilla_Web.Models;

namespace MagicVilla_Web.Services.IService
{
    public interface IBaseService
    {
        public APIResponse ResponseModel { get; set; }
        public Task<T> SendAsync<T>(APIRequest apiRequest);    
    }
}
