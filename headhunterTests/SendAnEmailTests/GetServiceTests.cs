using Google.Apis.Gmail.v1;
using headhunter.Services;

namespace headhunterTests.SendAnEmailTests
{
    [TestFixture]
    public class GetServiceTests
    {
        [Test]
        public void GetService_ReturnGmailService()
        {
            var res = new SendAnEmail().GetService();

            Assert.IsNotNull(res);
            Assert.IsInstanceOf<GmailService>(res);
        }
    }
}
