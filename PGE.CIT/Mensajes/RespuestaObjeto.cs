using PGE.CIT.Mensajes.Enumeraciones;

namespace PGE.CIT.Mensajes
{
    public class RespuestaObjeto : Base.Respuesta
    {
        public object Objeto { get; private set; }

        public RespuestaObjeto(TipoRespuesta tipoRespuesta, string mensaje, object objeto) : base(tipoRespuesta, mensaje)
        {
            this.Objeto = objeto;
        }

        public override string ToString()
        {
            return this.Mensaje;
        }
    }
}
