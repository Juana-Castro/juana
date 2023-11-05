using System;

namespace PGE.CIT.Excepciones
{
    public class AplicacionExcepcion : Exception
    {
        public AplicacionExcepcion() { }

        public AplicacionExcepcion(string mensaje) : base(mensaje) { }

        public AplicacionExcepcion(string mensaje, Exception excepcion) : base(mensaje, excepcion) { }
    }
}
