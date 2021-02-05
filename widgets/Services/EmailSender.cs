using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Mail;
using widgets.Data;
using widgets.Services;

namespace widgets
{
    public class EmailSender
    {
        private readonly ILogger<EmailSender> logger;
        private readonly IUrlGenerator _urlGenerator;
        private readonly ApplicationDbContext _dbContext;

        public EmailSender(ILogger<EmailSender> logger, ApplicationDbContext dbContext, IUrlGenerator urlGenerator)
        {
            _urlGenerator = urlGenerator;
            _dbContext = dbContext;
            this.logger = logger;
        }


        public async void SendEmail(string review, string tel,string param)
        {
            try
            {
                logger.LogDebug(param);
                int linkId = _urlGenerator.Decode(param);
                var link = await _dbContext.Widgets.FindAsync(linkId);
                MailMessage message = new MailMessage();
                message.IsBodyHtml = true;
                message.From = new MailAddress("2000.timati@mail.com", "Tima");
                //sultan.kadyrkesh @yandex.ru
                message.To.Add("qwerty_boy00@bk.ru");
                message.Subject = "Отзыв в приложении";
                message.Body = "<p>Отзыв: "+ review +"</p>" + "<p>Tel: "+tel + "</p>Widget: " + link.Name;

                using(SmtpClient client = new SmtpClient("smtp.gmail.com"))
                {
                    client.Credentials = new NetworkCredential("060karma100@gmail.com","aliya2008");
                    client.Port = 587;
                    client.EnableSsl = true;

                    client.Send(message);
                }
            }
            catch (Exception e)
            {
                logger.LogError(e.GetBaseException().Message);
            }
        }
    }
}