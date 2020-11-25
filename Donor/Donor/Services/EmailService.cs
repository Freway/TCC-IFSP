using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNet.Identity;

namespace Donor.Services {
    public class EmailService : IIdentityMessageService {

        private static readonly ILog Log = LogManager.GetLogger("AdoNetAppender");

/*        public EmailService(){
            log4net.Config.XmlConfigurator.Configure();
        }
        */
        public Task SendAsync(IdentityMessage message) {
            return ConfigSendGridasync(message);
        }

        private Task ConfigSendGridasync(IdentityMessage message) {
            var client = new SmtpClient{
                Host = "smtp.gmail.com",
                EnableSsl = true,
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("equipedonor@gmail.com", "ifsp@2017")
            };

            var mailMessage = new MailMessage{
                Sender = new MailAddress("equipedonor@gmail.com", "Donor"),
                From = new MailAddress("equipedonor@gmail.com", "Donor")
            };
            mailMessage.To.Add(new MailAddress(message.Destination));
            mailMessage.Subject = message.Subject;
            mailMessage.Body = message.Body;
            mailMessage.IsBodyHtml = true;

            try{
                client.Send(mailMessage);
            }
            catch (Exception e){
                Log.Error(e.Message,e);                      
            }

            return Task.FromResult(0);
        }
    }
}