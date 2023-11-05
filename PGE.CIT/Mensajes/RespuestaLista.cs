using Newtonsoft.Json;
using PGE.CIT.Mensajes.Enumeraciones;
using System.Collections;

namespace PGE.CIT.Mensajes
{
    public class RespuestaLista : Base.Respuesta
    {
        public Paginado Paginado { get; private set; }

        public IList Lista { get; private set; }

        public RespuestaLista(TipoRespuesta tipoRespuesta, string mensaje, IList lista = null) : base(tipoRespuesta, mensaje)
        {
            this.Paginado = new Paginado(lista.Count, 1, lista.Count);
            this.Lista = lista;
        }

        public RespuestaLista(TipoRespuesta tipoRespuesta, string mensaje, Paginado paginado, IList lista = null) : base(tipoRespuesta, mensaje)
        {
            this.Paginado = paginado;
            this.Lista = lista;
        }

        [JsonConstructor]
        public RespuestaLista(TipoRespuesta tipoRespuesta, string mensaje, int pagina = 1, int registrosPorPagina = 20, int totalRegistros = 0, IList lista = null) : base(tipoRespuesta, mensaje)
        {
            this.Paginado = new Paginado(totalRegistros, pagina, registrosPorPagina);
            this.Lista = lista;
        }

        public override string ToString()
        {
            return this.Mensaje;
        }
    }
}
