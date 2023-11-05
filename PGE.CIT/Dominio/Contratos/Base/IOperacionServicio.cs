using PGE.CIT.Mensajes;
using System.Threading.Tasks;

namespace PGE.CIT.Dominio.Contratos
{
    public interface IOperacionServicio<T>
    {
        Task<RespuestaOperacion> GuardarAsync(T objeto);
        Task<RespuestaOperacion> ModificarAsync(T objeto);
        Task<RespuestaOperacion> EliminarAsync(int id);
    }
}
