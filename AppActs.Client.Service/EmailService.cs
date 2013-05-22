using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

using AppActs.Client.Service.Interface;
using AppActs.Client.Model;
using AppActs.Model;
using AppActs.Core.Exceptions;
using System.Reflection;
using System.IO;
using System.Configuration;
using System.Net;
using AppActs.Client.Repository.Interface;


namespace AppActs.Client.Service
{
    public class EmailService : IEmailService
    {
        private readonly AppActs.Client.Model.Settings settings;

        public EmailService(AppActs.Client.Model.Settings settings)
        {
            this.settings = settings;
        } 

        public void SendUserForgotPassword(User accountUser, Guid guidForgotPassword)
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient())
                {
                    string message = this.getTemplate("AppActs.Client.Service.Templates.Email.UserForgotPassword.htm");

                    message = String.Format
                        (
                            message,
                            accountUser.Name,
                            new StringBuilder()
                                .Append(this.settings.Url)
                                .Append("Password-Change/")
                                .Append("?token=")
                                .Append(guidForgotPassword),
                            this.getTemplate("AppActs.Client.Service.Templates.Part.Signature.htm"),
                            settings.Url
                        );

                    using (MailMessage mailMessage = new MailMessage
                        (
                            new MailAddress(this.settings.EmailFrom, this.settings.EmailFromDisplayName),
                            new MailAddress(accountUser.Email)
                        ))
                    {
                        mailMessage.Subject = String.Format("{0}, did you forget your password?", accountUser.Name);
                        mailMessage.Body = message;
                        mailMessage.IsBodyHtml = true;
                        smtpClient.Send(mailMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ServiceLayerException(ex);
            }
        }

        public void SendUserAdded(string userNameAdded, string userNameNew, string userEmailNew, string userPasswordNew)
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient())
                {
                    string message = this.getTemplate("AppActs.Client.Service.Templates.Email.UserAdded.htm");

                    message = String.Format
                        (
                            message,
                            userNameAdded,
                            userNameNew,
                            userEmailNew,
                            userPasswordNew,
                            this.getTemplate("AppActs.Client.Service.Templates.Part.Signature.htm"),
                            settings.Url
                        );

                    using (MailMessage mailMessage = new MailMessage
                        (
                            new MailAddress(this.settings.EmailFrom, this.settings.EmailFromDisplayName),
                            new MailAddress(userEmailNew)
                        ))
                    {
                        mailMessage.Subject = String.Format("{0} you've been added", userNameNew);
                        mailMessage.Body = message;
                        mailMessage.IsBodyHtml = true;
                        smtpClient.Send(mailMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ServiceLayerException(ex);
            }
        }

        private string getTemplate(string path)
        {
            string template = string.Empty;

            using (StreamReader streamReader = new StreamReader
                (Assembly.GetExecutingAssembly().GetManifestResourceStream(path)))
            {
                template = streamReader.ReadToEnd();
            }

            return template;
        }
    }
}
