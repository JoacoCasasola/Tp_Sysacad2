using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Security;

namespace libreriaClases
{
    public class EnvioEmail
    {
        public void EnviarCorreo(string emailDestino, string asunto, string mensaje)
        {
            string emailOrigen = "joaquincasasola29@gmail.com";
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(emailOrigen, "Joaco");
                mailMessage.To.Add(emailDestino);

                mailMessage.Subject = asunto;
                mailMessage.Body = mensaje;
                mailMessage.IsBodyHtml = false;

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential(emailOrigen, "joa2003casa");

                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
