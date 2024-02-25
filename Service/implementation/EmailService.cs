using Domain.Helpers;
using Infra.IRepositories;
using MimeKit;
using Service.IService;
using MailKit.Net.Smtp;

namespace Service.implementation
{
    public class EmailService : IEmailService
    {
        #region Fields

        private readonly EmailSettings emailSettings;
        private readonly IEmployeeRepository userRepository;

        #endregion


        #region Constructor

        public EmailService(EmailSettings emailSettings, IEmployeeRepository userRepository)
        {
            this.emailSettings = emailSettings;
            this.userRepository = userRepository;
        }

        #endregion



        #region Functions

        public async Task SendEmailAsync(string emailAddress, string message, string subject)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(emailSettings.Host, emailSettings.Port, true);
                    client.Authenticate(emailSettings.FromEmail, emailSettings.Password);
                    var bodyBuilder = new BodyBuilder
                    {
                        HtmlBody = $"{message}",
                        TextBody = "welcome"
                    };
                    var mimeMessage = new MimeMessage
                    {
                        Body = bodyBuilder.ToMessageBody()
                    };
                    mimeMessage.From.Add(new MailboxAddress("Dexter", emailSettings.FromEmail));
                    mimeMessage.To.Add(new MailboxAddress("testing", emailAddress));
                    mimeMessage.Subject = subject;
                    await client.SendAsync(mimeMessage);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                
            }
        }


        #endregion
    }
}
