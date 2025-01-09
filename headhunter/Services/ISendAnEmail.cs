using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using headhunter.Dtos;

namespace headhunter.Services
{
    public interface ISendAnEmail
    {
        GmailService GetService();
        UserCredential AuthAsync(FileStream stream, string FilePath);
        GmailService CreateGmailService(UserCredential credential, string ApplicationName);
        string Base64UrlEncode(string input);
        Message CreateMessage(string sender, string to, string subject, ResumeForUser body);
        void SendMessage(GmailService service, string from, string to, string subject, ResumeForUser body);
        bool SendMessageToEmail(string from, string to, ResumeForUser resume);
    }
}
