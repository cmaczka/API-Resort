using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;

namespace MagicVilla_Web.Services.IService
{
    public interface INumeroVillaService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(NumeroVillaCreateDto dto);
        Task<T> UpdateAsync<T>(NumeroVillaUpdateDto dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
