using System.Collections.Generic;
using System.Linq;

namespace PGE.CIT
{
    public class ListaPaginada<T> : List<T>
    {
        public Paginado Paginado { get; set; }

        public ListaPaginada(List<T> lista, int totalRegistros, int pagina, int registrosPorPagina)
        {
            this.Paginado = new Paginado(totalRegistros, pagina, registrosPorPagina);
            this.AddRange(lista);
        }

        public static ListaPaginada<T> Paginar(IQueryable<T> origen, int pagina, int registrosPorPagina)
        {
            int totalRegistros = origen.Count();
            List<T> lista = new List<T>();
            if (registrosPorPagina > 0)
            {
                lista = origen.Skip((pagina - 1) * registrosPorPagina).Take(registrosPorPagina).ToList();
            }
            else
            {
                lista = origen.ToList();
            }
            return new ListaPaginada<T>(lista, totalRegistros, pagina, registrosPorPagina);
        }
    }
}
