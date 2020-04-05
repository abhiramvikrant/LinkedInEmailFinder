using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using LinkedEmailFinder.DataAccess;
using LinkedEmailFinder.DataAccess.Repository;
using LinkedInEmailFinder.Models;
using System.Linq;
namespace LinkedInEmailFinder.Email
{
   public class EmailSupport
    {
        private string EmailServer { get; set; }
        private string From { get; set; }

        public string To { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

       
        LinkedInEmailFinder_DBContext db = null;
        
        

        public EmailSupport()
        {
            
        }
        MailMessage mail = null;
        public EmailSupport(string To, string Subject, string Message)
        {
            db = new LinkedInEmailFinder_DBContext();
            this.EmailServer = db.Config.Where(l => l.KeyName == "EmailServer").ToList()[0].ToString();
            this.From = db.Config.Where(l => l.KeyName == "From").ToList()[0].ToString();

            mail = new MailMessage(this.From, To, Subject, Message);
          

        }

        public void SendEmail()
        {
            try
            {
                SmtpClient client = new SmtpClient(this.EmailServer);
                client.Send(mail);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
         
          

            
        
        }
    }
}
