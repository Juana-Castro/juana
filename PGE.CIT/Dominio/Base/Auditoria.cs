using System;

namespace PGE.CIT.Dominio.Base
{
    abstract public class Auditoria
    {
        public string AudUsuCreador { get; set; }
        public DateTime AudInstCreacion { get; set; }
        public string AudUsuOperacion { get; set; }
        public DateTime AudInstOperacion { get; set; }
        public string AudTipoOperacion { get; set; }
        public string AudObservacion { get; set; }
    }
}
