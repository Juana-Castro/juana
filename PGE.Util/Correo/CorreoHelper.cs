using PGE.CIT.Mensajes;
using PGE.CIT.Mensajes.Enumeraciones;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace PGE.Util.Correo
{
	public enum SmtpAutenticacion
    {
        Anonima = 0,
        Basica = 1,
        Windows = 2
    }

    public enum Formato
    {
        Informacion,
        Exito,
        Precaucion,
        Error
    }

    public class CorreoHelper
    {
        public static RespuestaOperacion Enviar(string servidor, SmtpAutenticacion autenticacion, bool usarSSL, string usuario, string contrasenia, string nomRemitente, string correoRemitente, List<string> destinatarios, string asunto, string mensaje, bool formatoHtml, string nomAdjunto = "", byte[] adjunto = null)
        {
            return Enviar(servidor, autenticacion, usarSSL, usuario, contrasenia, nomRemitente, correoRemitente, destinatarios, new List<string>(), new List<string>(), asunto, mensaje, formatoHtml, nomAdjunto, adjunto);
        }

        public static RespuestaOperacion Enviar(string servidor, SmtpAutenticacion autenticacion, bool usarSSL, string usuario, string contrasenia, string nomRemitente, string correoRemitente, List<string> destinatarios, List<string> destinatariosCopia, string asunto, string mensaje, bool formatoHtml, string nomAdjunto = "", byte[] adjunto = null)
        {
            return Enviar(servidor, autenticacion, usarSSL, usuario, contrasenia, nomRemitente, correoRemitente, destinatarios, destinatariosCopia, new List<string>(), asunto, mensaje, formatoHtml, nomAdjunto, adjunto);
        }

        public static RespuestaOperacion Enviar(string servidor, SmtpAutenticacion autenticacion, bool usarSSL, string usuario, string contrasenia, string nomRemitente, string correoRemitente, List<string> destinatarios, List<string> destinatariosCopia, List<string> destinatariosCopiaOculta, string asunto, string mensaje, Boolean formatoHtml, string nomAdjunto = "", byte[] adjunto = null)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            string[] servidorPuerto = servidor.Split(":");
            smtp.Host = servidorPuerto[0];
            smtp.Port = servidorPuerto.Length > 1 ? Convert.ToInt32(servidorPuerto[1]) : 25;

            if (destinatarios != null)
            {
                foreach (string destinatario in destinatarios)
                {
                    if (!string.IsNullOrEmpty(destinatario.Trim()))
                    {
                        message.To.Add(destinatario);
                    }
                }
            }
            else
            {
                throw new Exception("El parametro \"destinatarios\" no puede ser nulo");
            }

            if (destinatariosCopia != null)
            {
                foreach (string destinatario in destinatariosCopia)
                {
                    if (!string.IsNullOrEmpty(destinatario.Trim()))
                    {
                        message.CC.Add(destinatario);
                    }
                }
            }

            if (destinatariosCopiaOculta != null)
            {
                foreach (string destinatario in destinatariosCopiaOculta)
                {
                    if (!string.IsNullOrEmpty(destinatario.Trim()))
                    {
                        message.Bcc.Add(destinatario);
                    }
                }
            }

            message.From = new MailAddress(correoRemitente, nomRemitente, System.Text.Encoding.UTF8);
            message.Subject = asunto;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.Body = mensaje;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Priority = MailPriority.High;
            message.IsBodyHtml = formatoHtml;

            if (!string.IsNullOrEmpty(nomAdjunto) && adjunto != null)
            {
                Attachment attachment = new Attachment(new MemoryStream(adjunto), nomAdjunto);
                message.Attachments.Add(attachment);
            }

            try
            {
                switch (autenticacion)
                {
                    case SmtpAutenticacion.Anonima:
                        break;
                    case SmtpAutenticacion.Basica:
                        if (!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(contrasenia))
                        {
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = new NetworkCredential(usuario, contrasenia);
                        }
                        break;
                    case SmtpAutenticacion.Windows:
                        smtp.UseDefaultCredentials = true;
                        break;
                }
                // if (!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(contrasenia))
                // {
                //     smtp.UseDefaultCredentials = false;
                //     smtp.Credentials = new NetworkCredential(usuario, contrasenia);
                // }
                // else
                // {
                //     smtp.UseDefaultCredentials = true;
                // }
                smtp.EnableSsl = usarSSL;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

                return new RespuestaOperacion(TipoRespuesta.Exito, "Mensaje enviado correctamente");
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                return new RespuestaOperacion(TipoRespuesta.Error, ex.Message);
            }
        }

        public static string Formato(Formato formato, string aplicacion, string uniOrganizacional, string titulo, List<string> cuerpo, string encabezado = "", string pie = "", bool aclaracion = true)
        {
            string correo = "";

            switch (formato)
            {
                case Correo.Formato.Informacion:
                    correo = Formatos.Informacion;
                    break;
                case Correo.Formato.Exito:
                    correo = Formatos.Exito;
                    break;
                case Correo.Formato.Precaucion:
                    correo = Formatos.Precaucion;
                    break;
                case Correo.Formato.Error:
                    correo = Formatos.Error;
                    break;
            }

            correo = correo.Replace("#TITULO#", HttpUtility.HtmlEncode(titulo));
            correo = correo.Replace("#ACLARACION#", aclaracion ? "style:\"display: block;\"" : "style:\"display: none;\"");

            correo = correo.Replace("#ENCABEZADO_VISIBLE#", !string.IsNullOrEmpty(encabezado) ? "style:\"display: block;\"" : "style:\"display: none;\"");
            correo = correo.Replace("#ENCABEZADO#", HttpUtility.HtmlEncode(encabezado));

            StringBuilder cuerpoHtml = new StringBuilder();
            foreach (string parrafo in cuerpo)
            {
                cuerpoHtml.AppendFormat("<p>{0}</p>", parrafo);
            }
            correo = correo.Replace("#CUERPO#", cuerpoHtml.ToString());

            correo = correo.Replace("#PIE_VISIBLE#", !string.IsNullOrEmpty(pie) ? "style:\"display: block;\"" : "style:\"display: none;\"");
            correo = correo.Replace("#PIE#", HttpUtility.HtmlEncode(pie));

            correo = correo.Replace("#APLICACION#", HttpUtility.HtmlEncode(aplicacion));
            correo = correo.Replace("#UNIDAD_ORGANIZACIONAL#", HttpUtility.HtmlEncode(uniOrganizacional));

            return correo;
        }
    }
}
