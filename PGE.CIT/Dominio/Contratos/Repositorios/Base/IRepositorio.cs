using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PGE.CIT.Dominio.Contratos.Repositorios
{
    public interface IRepositorio<T> where T : class, IEntidad
    {

        IQueryable<T> ObtenenerPor(Expression<Func<T, bool>> matchitem, bool seguimiento = true);
        Task<ListaPaginada<T>> ObtenerTodoAsync(int inicio = 0, int cantidad = 0);
        Task<ListaPaginada<T>> ObtenerPorAsync(Expression<Func<T, bool>> expresion, int inicio = 0, int cantidad = 0, bool seguimiento = true);
        Task<T> ObtenerObjetoPorAsync(Expression<Func<T, bool>> expresion, bool seguimiento = true);
        Task<T> ObtenerPorIdAsync(int id);
        Task<int> GuardarAsync(T objeto);
        Task<bool> ModificarAsync(T objeto);
        Task<bool> EliminarAsync(int id);
		Task<bool> EliminarAsync(T objeto);

	}
}
