using System;
using System.Net.Mime;
using MailKit;
using MailKit.Search;
using MailKit.Net;
using MimeKit;
using MailKit.Net.Smtp;

namespace Server.BOL
{
    public class EmailBOL
    {
        private readonly String campusEMail = "campus.store.mail@gmail.com";
        private readonly String password = "nubJosqoxgyg9qukpo";
        private readonly String fromEMail = "campus.store.mail@gmail.com";
        private readonly String fromName = "Campus Store";
        public void setTempPassword(String tempPassword, String email, String Name)
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress(fromName, fromEMail));
            mailMessage.To.Add(new MailboxAddress(Name, email));
            mailMessage.Subject = "Campus Store Temporary Password";
            mailMessage.Body = new TextPart("plain")
            {
                Text = "Temporary password for your Campus Store profile: " + tempPassword + "\n"
                + "Be sure to change it under Profile -> Update Password."
            };

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect("smtp.gmail.com", 465, true);
                smtpClient.Authenticate(campusEMail, password);
                smtpClient.Send(mailMessage);
                smtpClient.Disconnect(true);
            }
        }
    }
}