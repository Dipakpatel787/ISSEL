using ISSEL.Models.Common;
using ISSEL.Services.Abstraction;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;

using MailKit.Net.Smtp;
using System.Text;
using System.Threading.Tasks;

namespace ISSEL.Services.Repository
{
    public  class EmailService : IEmailService
    {
        private readonly EmailServiceOptions Options;

        public EmailService(IOptions<AppSettings> aAppSettings)
        {
            Options = aAppSettings.Value.EmailServiceOptions;
        }

        public void SendEmail(string aRecipientName, string aRecipientAddress, string aSubject, string aText)
        {
            var vSender = new MailboxAddress(Options.SenderName, Options.SenderAddress);
            var vRecipient = new MailboxAddress(aRecipientName, aRecipientAddress);

            var vMessage = new MimeMessage
            {
                Subject = aSubject,
                Body = new TextPart("html")
                {
                    Text = aText
                }
            };

            vMessage.From.Add(vSender);
            vMessage.To.Add(vRecipient);
            //var copyAddress = new MailboxAddress(string.Empty, Options.CopyAddress);
            //vMessage.Cc.Add(copyAddress);

            Task.Run(() =>
            {
                using var vClient = new SmtpClient();
                vClient.Connect(Options.Host, Options.Port, Options.UseSsl);
                // uncomment if the SMTP server requires authentication
                vClient.Authenticate(Options.UserName, Options.Password);
                vClient.Send(vMessage);
                vClient.Disconnect(true);
            });
        }
    }
}
