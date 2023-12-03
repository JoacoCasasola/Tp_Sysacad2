using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;

namespace libreriaClases
{
    internal class EnvioEmail
    {

        public SmtpClient MiEmail()
        {
            var smtpClient = new SmtpClient("smtp.example.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("tu_correo@example.com", "tu_contraseña"),
                EnableSsl = true,
            };

            return smtpClient;
        }
        
        public MailMessage CrearMensaje(string miEmail, string asunto, string cuerpoMensaje)
        {
            var mensaje = new MailMessage
            {
                From = new MailAddress(miEmail),
                Subject = asunto,
                Body = cuerpoMensaje,
            };

            return mensaje;
        }

        
        public void AgregarDestinatario(MailMessage mensaje, string emailDestinatario)
        {
            mensaje.To.Add(emailDestinatario);
        }      


        public void EnviarMensaje(MailMessage mensaje, SmtpClient smtpClient)
        {
            smtpClient.Send(mensaje);
        }
        
    }
}
