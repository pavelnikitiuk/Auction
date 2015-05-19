using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Auction.Domain.Abstract;
using Auction.Domain.EmailSender;

namespace Auction.Domain.EmailSender
{
   public class GoogleEmailSender:IEmailSender
    {

        public  void Send(EmailModel model)
        {
            string smtpHost = "smtp.gmail.com";///
            int smtpPort = 587;
            string login = model.From;
            string pass =model.Pass;
            SmtpClient client = new SmtpClient(smtpHost, smtpPort);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(login, pass);
            client.EnableSsl = true;
            string from = model.From;
            string to = model.To;
            string subject = model.Subject;
            string body = model.Body;
            MailMessage mess = new MailMessage(from, to, subject, body);
            try
            {
                client.Send(mess);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadKey();
            }
        }
    }
}
