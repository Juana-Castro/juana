using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PGE.CIT.Auditoria
{
    public static class AuditoriaHelper<T>
    {
        public static void Establecer(ref T objeto, object usuario, string tipo, string observacion)
        {
            Dictionary<string, object> camposAuditoria = new Dictionary<string, object>();
            if (tipo == "C")
            {
                camposAuditoria.Add("AudUsuCreador", usuario);
                camposAuditoria.Add("AudInstCreacion", DateTime.Now);
            }
            camposAuditoria.Add("AudUsuOperacion", usuario);
            camposAuditoria.Add("AudTipoOperacion", tipo);
            camposAuditoria.Add("AudInstOperacion", DateTime.Now);
            camposAuditoria.Add("AudObservacion", observacion);

            foreach (KeyValuePair<string, object> campo in camposAuditoria)
            {
                if (objeto.GetType().GetProperty(campo.Key) != null)
                {
                    objeto.GetType().GetProperty(campo.Key).SetValue(objeto, campo.Value, new object[] { });
                }
            }
        }

        public static void Guardar(T objetoBD, T objetoNuevo)
        {
            JObject jsonObjetoBD = objetoBD != null ? JObject.FromObject(objetoBD) : new JObject();
            JObject jsonObjetoNuevo = objetoNuevo != null ? JObject.FromObject(objetoNuevo) : new JObject();
            JObject json = JObject.FromObject(new { AntesOperacion = jsonObjetoBD, DespuesOperacion = jsonObjetoNuevo });

            Debug.WriteLine(json.ToString());
        }
    }
}
