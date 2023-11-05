using System;

namespace PGE.CIT.Excepciones
{
    public class PersistenciaExcepcion : Exception
    {
        public PersistenciaExcepcion() { }

        public PersistenciaExcepcion(string mensaje) : base(mensaje) { }

        public PersistenciaExcepcion(string mensaje, Exception excepcion) : base(mensaje, excepcion) { }
    }
}
