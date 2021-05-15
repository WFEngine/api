using MailKit.Net.Smtp;
using MimeKit;
using NETCore.Encrypt;
using System;
using System.Threading.Channels;
using System.Threading.Tasks;
using WFEngine.Core.Interfaces;
using WFEngine.Core.Utilities;

namespace WFEngine.Service.Channels
{
    public class RecoveryPasswordChannel : IChannel<string>
    {
        Channel<string> channel;
        ConnectionInfo ci;
        IUnitOfWork uow;


        public RecoveryPasswordChannel(IUnitOfWork _uow)
        {
            channel = Channel.CreateUnbounded<string>();
            ci = ConnectionInfo.Instance;
            uow = _uow;

            ExecuteConsumer();
        }

        public void ExecuteConsumer()
        {
            _ = Task.Run(async () =>
            {
                while (true)
                {
                    var data = await channel.Reader.ReadAsync();
                    if (!string.IsNullOrEmpty(data))
                    {
                        var userExists = uow.User.FindByEmail(data);
                        if (userExists.Success)
                        {
                            var user = userExists.Data;
                            var password = Guid.NewGuid().ToString().Substring(0, 8);
                            var message = new MimeMessage();
                            message.From.Add(new MailboxAddress(ci.SmtpSender, ci.SmtpSender));
                            message.To.Add(new MailboxAddress(data, data));
                            message.Subject = "Yeni Şifre İsteği";

                            message.Body = new TextPart("plain")
                            {
                                Text = $"Yeni Şifreniz {password}"
                            };

                            user.Password = EncryptProvider.Md5(password);

                            uow.User.UpdateUser(user);
                            uow.Commit();

                            using (var client = new SmtpClient())
                            {
                                client.Connect(ci.SmtpHost, ci.SmtpPort, false);
                                client.Authenticate(ci.SmtpSender, ci.SmtpPassword);
                                client.Send(message);
                                client.Disconnect(true);
                            }
                        }

                       
                    }                  

                    await Task.Delay(5000);
                }
            });
        }

        public void SendProducer(string value)
        {
            channel.Writer.WriteAsync(value);
        }
    }
}
