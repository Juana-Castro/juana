using System;
using System.Collections;

namespace PGE.CIT.Mensajes
{
    [Obsolete("Formato de respuesta obsoleto, debe utilizar la clase RREE.CIT.Mensajes.RespuestaObjeto o RREE.CIT.Mensajes.RespuestaLista ", true)]
    public class RespuestaConsulta
    {
        public TipoRespuestaOperacion TipoRespuesta { get; private set; }
        public string Codigo { get; private set; }
        public string Mensaje { get; private set; }

        public Paginado Paginado { get; private set; }

        public object Objeto { get; private set; }
        public IList Lista { get; private set; }

        //Respuesta con un objeto
        public RespuestaConsulta(TipoRespuestaOperacion tipoRespuesta, string mensaje = null, object objeto = null)
        {
            this.TipoRespuesta = tipoRespuesta;
            this.Mensaje = mensaje;
            this.Paginado = null;
            this.Objeto = objeto;
            this.Lista = null;
        }

        public RespuestaConsulta(TipoRespuestaOperacion tipoRespuesta, string codigo = null, string mensaje = null, object objeto = null)
        {
            this.TipoRespuesta = tipoRespuesta;
            this.Codigo = codigo;
            this.Mensaje = mensaje;
            this.Paginado = null;
            this.Objeto = objeto;
            this.Lista = null;
        }

        //Respuesta con una lista de objetos
        public RespuestaConsulta(TipoRespuestaOperacion tipoRespuesta, string mensaje = null, IList lista = null)
        {
            this.TipoRespuesta = tipoRespuesta;
            this.Mensaje = mensaje;
            this.Paginado = null;
            this.Objeto = null;
            this.Lista = lista;
        }

        public RespuestaConsulta(TipoRespuestaOperacion tipoRespuesta, string codigo = null, string mensaje = null, IList lista = null)
        {
            this.TipoRespuesta = tipoRespuesta;
            this.Codigo = codigo;
            this.Mensaje = mensaje;
            this.Paginado = null;
            this.Objeto = null;
            this.Lista = lista;
        }

        public RespuestaConsulta(TipoRespuestaOperacion tipoRespuesta, string mensaje = null, int pagina = 1, int registrosPorPagina = 20, int totalRegistros = 0, IList lista = null)
        {
            this.TipoRespuesta = tipoRespuesta;
            this.Mensaje = mensaje;
            this.Paginado = new Paginado(totalRegistros, pagina, registrosPorPagina);
            this.Objeto = null;
            this.Lista = lista;
        }

        public RespuestaConsulta(TipoRespuestaOperacion tipoRespuesta, string codigo = null, string mensaje = null, int pagina = 1, int registrosPorPagina = 20, int totalRegistros = 0, IList lista = null)
        {
            this.TipoRespuesta = tipoRespuesta;
            this.Codigo = codigo;
            this.Mensaje = mensaje;
            this.Paginado = new Paginado(totalRegistros, pagina, registrosPorPagina);
            this.Objeto = null;
            this.Lista = lista;
        }

        public override string ToString()
        {
            return this.Mensaje;
        }
    }
}
