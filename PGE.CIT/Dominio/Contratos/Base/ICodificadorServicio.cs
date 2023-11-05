using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PGE.CIT.Dominio.Contratos
{
    public interface ICodificadorServicio<T>
    {
        Task<IList<T>> BuscarAsync(Expression<Func<T, bool>> expresion);
        Task<IList<T>> ObtenerTodoAsync();
    }
}
