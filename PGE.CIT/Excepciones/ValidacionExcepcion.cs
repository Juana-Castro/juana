using System;

namespace PGE.CIT.Excepciones
{
    public class ValidacionExcepcion : Exception
    {
        public ValidacionExcepcion() { }

        public ValidacionExcepcion(string mensaje) : base(mensaje) { }

        public ValidacionExcepcion(string mensaje, Exception excepcion) : base(mensaje, excepcion) { }
    }
}
