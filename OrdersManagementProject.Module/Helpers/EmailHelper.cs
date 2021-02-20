using System;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using Domain.Options;
using MailKit.Net.Smtp;
using MimeKit;

namespace Helpers
{
    public static class EmailHelper
    {
        #region SendEmail

        public static bool SendEmail(Session session, MimeMessage message, out string errorMessage)
        {
            bool result = false;
            errorMessage = "";

            ProgramOptions programOptions = ProgramOptions.GetInstance(session);
            if (session == null)
            {
                Tracing.Tracer.LogError("Session does not exists.");
                errorMessage += Environment.NewLine + "Session does not exists.";
            }
            if (programOptions == null)
            {
                Tracing.Tracer.LogError("Program options does not exists.");
                errorMessage += Environment.NewLine + "Program options does not exists.";
            }
            if (message == null)
            {
                Tracing.Tracer.LogError("The content of email is not specified.");
                errorMessage += Environment.NewLine + "The content of email is not specified.";
            }
            if (string.IsNullOrEmpty(programOptions.SenderEmailAddress))
            {
                Tracing.Tracer.LogError("Sender Email Address is not specified.");
                errorMessage += Environment.NewLine + "Sender Email Address is not specified.";
            }
            if (string.IsNullOrEmpty(programOptions.SmtpClientName))
            {
                Tracing.Tracer.LogError("SMTP Client Name is not specified.");
                errorMessage += Environment.NewLine + "SMTP Client Name is not specified.";
            }
            if (programOptions.SMTPPort == 0)
            {
                Tracing.Tracer.LogError("SMTP Port is not specified.");
                errorMessage += Environment.NewLine + "SMTP Port is not specified.";
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                return result;
            }

            using (SmtpClient smtpClient = new SmtpClient())
            {
                try
                {
                    smtpClient.Connect(programOptions.SmtpClientName, programOptions.SMTPPort, programOptions.EnableSSL);
                    smtpClient.AuthenticationMechanisms.Remove("XOAUTH2");
                    smtpClient.Authenticate(programOptions.SenderEmailAddress, programOptions.SenderEmailPassword);
                    smtpClient.Send(message);
                    result = true;
                }
                catch (Exception ex)
                {
                    Tracing.Tracer.LogError(ex);
                    result = false;
                }
                finally
                {
                    smtpClient.Disconnect(true);
                    smtpClient.Dispose();
                }
            }

            return result;
        }

        #endregion
    }
}
