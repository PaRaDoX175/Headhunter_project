using headhunter.Dtos;
using headhunter.Services;

namespace headhunterTests.SendAnEmailTests
{
    [TestFixture]
    public class SendMessageTests
    {
        [Test]
        public void SendMessageToEmail_SuccesfullySend()
        {
            var resume = new ResumeForUser
            {
                Id = 1,
                Name = "Bob",
                AboutMe = "Smth",
                PictureUrl = "phot.jpg",
                Email = "bobban@gmail.com",
                PhoneNumber = "1234567890",
                Profession = "Web-Developer",
                Skills = "Web-Development"
            };

            var sendEmail = new SendAnEmail();
            var res = sendEmail.SendMessageToEmail("absalyamov.ruslan01@gmail.com", "ruslan.absalam@gmail.com", resume);
            Assert.That(res, Is.True);
        }
    }
}
