using System.Net;
using System.Net.Mail;

namespace Shared.Lib.Services;

public class MailService {

    SmtpClient client = new SmtpClient();
    NetworkCredential networkCredential = new NetworkCredential();

    public MailService(string host, 
					   int port, 
					   string userName, 
					   string password, 
					   bool enableSSL = true) {

        this.client.UseDefaultCredentials = false;
        this.client.Host                  = host;
        this.client.Port                  = port;
        this.networkCredential.UserName   = userName;
        this.networkCredential.Password   = password;
		this.client.EnableSsl             = enableSSL;
        this.client.Credentials           = this.networkCredential;
    }

    public bool SendMail(string senderEmail, string[] recipients, string body, string subject, List<Attachment>? attachments) {
        try {
            MailMessage mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(senderEmail);
            foreach (var email in recipients) {
				mailMessage.To.Add(new MailAddress(email));
            }

            mailMessage.IsBodyHtml = true;
            mailMessage.Body       = body;
            mailMessage.Subject    = subject;
            mailMessage.Priority   = MailPriority.High;

            if (attachments != null && attachments.Count > 0) {
                foreach (var attachment in attachments) {
                    mailMessage.Attachments.Add(attachment);
                }
            }

            this.client.Send(mailMessage);

            return true;

        } catch (Exception ex) {
            throw ex;
        }
    }
}