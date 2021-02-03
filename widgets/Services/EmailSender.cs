using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Mail;

namespace widgets
{
    public class EmailSender
    {
        private readonly ILogger<EmailSender> logger;

        public EmailSender(ILogger<EmailSender> logger)
        {
            this.logger = logger;
        }
        public void SendEmailDefault(string review, string tel)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.IsBodyHtml = true;
                message.From = new MailAddress("2000.timati@mail.com", "Tima");
                message.To.Add("sultan.kadyrkesh@yandex.ru");
                message.Subject = "Отзыв в приложении";
                message.Body = "Отзыв: "+ review +"\n" + "Tel: "+tel;

                using(SmtpClient client = new SmtpClient("smtp.gmail.com"))
                {
                    client.Credentials = new NetworkCredential("060karma100@gmail.com","aliya2008");
                    client.Port = 587;
                    client.EnableSsl = true;

                    client.Send(message);
                    logger.LogDebug(review);
                }
            }
            catch (Exception e)
            {
                logger.LogError(e.GetBaseException().Message);
            }
        }
    }
}