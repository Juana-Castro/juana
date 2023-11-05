using PGE.CIT.Mensajes.Enumeraciones;

namespace PGE.CIT.Mensajes.Base
{
    public abstract class Respuesta
    {
        public TipoRespuesta TipoRespuesta { get; private set; }
        public string Mensaje { get; private set; }

        public bool Exitosa
        {
            get { return this.TipoRespuesta == TipoRespuesta.Exito; }
        }

        public Respuesta(TipoRespuesta tipoRespuesta, string mensaje = null)
        {
            this.TipoRespuesta = tipoRespuesta;
            this.Mensaje = mensaje;
        }

        public override string ToString()
        {
            return this.Mensaje;
        }
    }
}
