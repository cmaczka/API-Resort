using System.Linq.Expressions;

namespace Resort.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T : class
    {
        Task Crear(T entidad);
        Task<T> Obtener(Expression<Func<T, bool>>? filtro = null, bool tracked = true);
        Task<IEnumerable<T>> ObtenerTodos(Expression<Func<T,bool>>? filtro = null);
        Task Eliminar(T entidad);
        Task Grabar();
    }
}
