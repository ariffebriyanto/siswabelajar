using Microsoft.Extensions.Configuration;
using Model.Subdomains.EmailSubdomain;
using Service.Helpers.EmailHelper;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace OneStopRecruitment.Helpers.SmtpHelpers
{
    public class EmailHelper : IEmailHelper
    {
        private readonly string _host;
        private readonly int _port;
        private readonly bool _useSsl;
        private readonly int _timeout;
        private readonly MailAddress _masking;
        private readonly NetworkCredential _credential;

        public EmailHelper(IConfiguration iConfiguration)
        {
            var SmtpSettings = iConfiguration.GetSection("SMTP");
            if (SmtpSettings != null)
            {
                _host = SmtpSettings.GetSection("Host").Value;
                _port = Int32.Parse(SmtpSettings.GetSection("Port").Value);
                _useSsl = Boolean.Parse(SmtpSettings.GetSection("UseSsl").Value);
                _timeout = Int32.Parse(SmtpSettings.GetSection("Timeout").Value);
                _masking = new MailAddress(SmtpSettings.GetSection("Username").Value, SmtpSettings.GetSection("Name").Value);
                _credential = new NetworkCredential
                {
                    UserName = SmtpSettings.GetSection("Username").Value,
                    Password = SmtpSettings.GetSection("Password").Value
                };
            }
        }

        public bool Send(Email email)
        {
            try
            {
                MailMessage message = new MailMessage();
                foreach (var item in email.Recipients)
                {
                    message.To.Add(new MailAddress(item));
                }

                message.From = _masking;
                message.Subject = email.Subject;
                message.SubjectEncoding = Encoding.Unicode;
                message.Body = email.Body;
                message.BodyEncoding = Encoding.Unicode;
                message.IsBodyHtml = email.IsBodyHtml;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = _host;
                smtp.Port = _port;
                smtp.Credentials = _credential;
                smtp.EnableSsl = _useSsl;
                smtp.Timeout = _timeout;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                smtp.Send(message);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
