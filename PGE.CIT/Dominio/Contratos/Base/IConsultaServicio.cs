using System.Threading.Tasks;

namespace PGE.CIT.Dominio.Contratos
{
    public interface IConsultaServicio<T, TFiltro>
    {
        Task<ListaPaginada<T>> BuscarAsync(TFiltro filtro);
        Task<T> ObtenerPorIdAsync(int id);
    }
}
