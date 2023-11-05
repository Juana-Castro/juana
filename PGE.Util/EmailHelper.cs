using System;
using System.Net;
using System.Net.Mail;

namespace PGE.Util
{
    public class EmailHelper
    {
        public void SendEmail(String SmptServer, String User, String Password, String Destino, String Origen, string Referencia, string Mensaje)
        {
            SmtpClient client = new SmtpClient(SmptServer);
            client.UseDefaultCredentials = false;

            client.Credentials = new NetworkCredential(User, Password);

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(Origen);
            mailMessage.To.Add(Destino);
            mailMessage.Body = Mensaje;
            mailMessage.Subject = Referencia;

            mailMessage.IsBodyHtml = true;
            try
            {
                client.Send(mailMessage);
            }
            catch (Exception e)
            {
                String wwww = "";
            }


        }
    }
}
