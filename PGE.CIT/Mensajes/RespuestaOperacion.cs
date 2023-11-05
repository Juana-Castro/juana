using PGE.CIT.Mensajes.Enumeraciones;

namespace PGE.CIT.Mensajes
{
    public class RespuestaOperacion : Base.Respuesta
    {
        private object _Objeto;
        public object Objeto
        {
            get { return _Objeto; }
            set { _Objeto = value; }
        }

        public RespuestaOperacion(TipoRespuesta tipoRespuesta, string mensaje) : base(tipoRespuesta, mensaje)
        {
        }

        public RespuestaOperacion(TipoRespuesta tipoRespuesta, string mensaje, object objeto) : base(tipoRespuesta, mensaje)
        {
            _Objeto = objeto;
        }
    }
}
