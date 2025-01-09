using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using headhunter.Dtos;
using MimeKit;

namespace headhunter.Services
{
    public class SendAnEmail : ISendAnEmail
    {
        static string[] Scopes = { GmailService.Scope.MailGoogleCom };
        static string ApplicationName = "ProjectGmailApi";
        static string FilePathCred = "C:\\Ruslan\\headhunter\\GmailAPI\\ClientCredentials\\client_secret.json";
        static string CredentialsInfo = "C:\\Ruslan\\headhunter\\GmailAPI\\CredentialsInfo\\";

        public virtual GmailService GetService()
        {
            UserCredential credential;
            using (FileStream stream = new FileStream(FilePathCred, FileMode.Open, FileAccess.Read))
            {
                string FilePath = Path.Combine(CredentialsInfo, "APITokenCredentials");

                if (!Directory.Exists(FilePath))
                {
                    return null;
                }
                credential = AuthAsync(stream, FilePath);
            }

            GmailService service = CreateGmailService(credential, ApplicationName);
            return service;
        }

        public UserCredential AuthAsync(FileStream stream, string FilePath)
        {
            if (stream == null || FilePath == null)
            {
                return null;
            } else
            {
                return GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.FromStream(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(FilePath, true)).Result;            
            }
        }

        public GmailService CreateGmailService(UserCredential credential, string ApplicationName)
        {
            if (credential == null || ApplicationName == null)
            {
                return null;
            }
            return new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        public string Base64UrlEncode(string input)
        {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(inputBytes)
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");
        }

        public Message CreateMessage(string sender, string to, string subject, ResumeForUser body)
        {
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $@"<html><body>
                        <h1>{body.Name}</h1>
                        <p>{body.AboutMe}</p>
                        <p><strong>{body.Skills}</strong></p>
                      </body></html>"
            };

            var message = new Message();

            var emailContent = $"From: {sender}\nTo: {to}\nSubject: {subject}\nContent-Type: text/html\n\n{bodyBuilder.HtmlBody}";
            var encodedEmail = Base64UrlEncode(emailContent);
            message.Raw = encodedEmail;
            return message;
        }

        public virtual void SendMessage(GmailService service, string from, string to, string subject, ResumeForUser body)
        {
            try
            {
                var senderEmail = from;
                var newMessage = CreateMessage(senderEmail, to, subject, body);
                service.Users.Messages.Send(newMessage, "me").Execute();
                MyLogger.Instance.Logger.LogInformation("Message succesfully send!");
            }
            catch (Exception ex)
            {
                MyLogger.Instance.Logger.LogError($"Something went wrong! Error Message - {ex.Message}");
            }
        }


        public bool SendMessageToEmail(string from, string to, ResumeForUser resume)
        {
            try
            {
                var service = GetService();

                if (service == null)
                {
                    throw new ArgumentNullException("service", "Something went wrong or service is null");
                }

                SendMessage(service, from, to, "You've been sent a resume", resume);
                return true;

            } catch
            {
                return false;
            }
        }
    }
}
