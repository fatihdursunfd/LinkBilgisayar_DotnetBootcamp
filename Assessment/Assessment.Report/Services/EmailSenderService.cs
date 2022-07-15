using Assessment.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Reporting.Services
{
    public class EmailSenderService
    {
        public void Send(UserFile userFile, List<string> admins, bool weekly)
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "Files/", userFile.FileName).Replace("Assessment.Report", "Assessment.API");

            MailMessage mail = new MailMessage();
            admins.ForEach(x =>
            {
                mail.To.Add(x);
            });
            mail.From = new MailAddress("fafihdrsn.1967@gmail.com");

            if (weekly)
            {
                mail.Subject = "Weekly Report";
                mail.Body = $"<p> Customers that most transaction between {DateTime.Now.ToString("dd/MM/yyyy")} and {DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy")}  </p>";
            }
            else
            {
                mail.Subject = "Monthly Report";
                mail.Body = $"<p> Customers count with city {DateTime.Now.ToString("dd / MM / yyyy")} and {DateTime.Now.AddDays(-30).ToString("dd / MM / yyyy") } </p>";
            }

            mail.IsBodyHtml = true;
            mail.Attachments.Add(new Attachment(file));

            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new NetworkCredential("fafihdrsn.1967@gmail.com", "sjyvzbnmvgzsuagu");
            smtp.Send(mail);
        }

    }
}
