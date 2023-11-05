using System;

namespace PGE.CIT
{
    public class Paginado
    {
        public int TotalRegistros { get; private set; }
        public int RegistrosPorPagina { get; private set; }
        public int Pagina { get; private set; }
        public int TotalPaginas
        {
            get
            {
                if (this.RegistrosPorPagina > 0)
                {
                    return Convert.ToInt32(Math.Ceiling((decimal)this.TotalRegistros / (decimal)this.RegistrosPorPagina));
                }
                return 0;
            }
        }

        public Paginado(int totalRegistros, int pagina = 0, int registrosPorPagina = 20)
        {
            this.TotalRegistros = totalRegistros;
            this.Pagina = pagina;
            this.RegistrosPorPagina = registrosPorPagina;
        }
    }
}
