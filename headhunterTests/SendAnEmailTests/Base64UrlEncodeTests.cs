using headhunter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace headhunterTests.SendAnEmailTests
{
    [TestFixture]
    public class Base64UrlEncodeTests
    {
        [Test]
        public void Base64UrlEncode_ReturnConvertString()
        {
            var res = new SendAnEmail().Base64UrlEncode("Hello World! /, + ");

            Assert.IsNotNull(res);
            var decode = Encoding.UTF8.GetString(Convert.FromBase64String(res));
            Assert.That(decode, Is.EqualTo("Hello World! /, + "));
        }
    }
}
