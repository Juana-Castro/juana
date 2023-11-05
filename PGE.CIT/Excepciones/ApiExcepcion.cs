using System;

namespace PGE.CIT.Excepciones
{
    public class ApiExcepcion : Exception
    {
        public ApiExcepcion() { }

        public ApiExcepcion(string mensaje) : base(mensaje) { }

        public ApiExcepcion(string mensaje, Exception excepcion) : base(mensaje, excepcion) { }
    }
}
