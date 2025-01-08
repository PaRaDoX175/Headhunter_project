using Google.Apis.Gmail.v1.Data;
using headhunter.Dtos;
using headhunter.Services;

namespace headhunterTests.SendAnEmailTests
{
    [TestFixture]
    public class CreateMessageTests
    {
        [Test]
        public void CreateMessage_ReturnMassage()
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

            var res = new SendAnEmail().CreateMessage("absalyamov.ruslan01@gmail.com", "ruslan.absalam@gmail.com", "Its me", resume);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Raw);
            Assert.IsInstanceOf<Message>(res);
        }
    }
}
