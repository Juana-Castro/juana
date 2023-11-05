namespace PGE.CIT.Seguimiento
{
    public enum TipoSeguimiento : int { Debug = 0, Informacion = 1, Precaucion = 2, Error = 3, ErrorFatal = 4 }

    public static class LoggingHelper
    {
        //private static readonly ILog log = LogManager.GetLogger(typeof(LoggingHelper));

        //public static void LogMensaje(TipoSeguimiento tipo, string mensaje)
        //{
        //    LogMensaje(tipo, mensaje, null);
        //}

        //public static void LogMensaje(TipoSeguimiento tipo, string mensaje, Exception excepcion)
        //{
        //    log4net.Config.XmlConfigurator.Configure();
        //    string mensajeLog = string.Empty;

        //    switch (tipo)
        //    {
        //        case TipoSeguimiento.Debug:
        //            if (excepcion == null)
        //            {
        //                log.Debug(mensaje);
        //            }
        //            else
        //            {
        //                log.Debug(mensaje, excepcion);
        //            }
        //            break;
        //        case TipoSeguimiento.Informacion:
        //            if (excepcion == null)
        //            {
        //                log.Info(mensaje);
        //            }
        //            else
        //            {
        //                log.Info(mensaje, excepcion);
        //            }
        //            break;
        //        case TipoSeguimiento.Precaucion:
        //            if (excepcion == null)
        //            {
        //                log.Warn(mensaje);
        //            }
        //            else
        //            {
        //                log.Warn(mensaje, excepcion);
        //            }
        //            break;
        //        case TipoSeguimiento.Error:
        //            if (excepcion == null)
        //            {
        //                log.Error(mensaje);
        //            }
        //            else
        //            {
        //                log.Error(mensaje, excepcion);
        //            }
        //            break;
        //        case TipoSeguimiento.ErrorFatal:
        //            if (excepcion == null)
        //            {
        //                log.Fatal(mensaje);
        //            }
        //            else
        //            {
        //                log.Fatal(mensaje, excepcion);
        //            }
        //            break;
        //    }
        //}
    }
}
