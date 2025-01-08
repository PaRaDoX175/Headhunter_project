using headhunter;
using headhunter.Dtos;
using headhunter.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace headhunterTests
{
    [TestFixture]
    public class ResumeRepositoryTests
    {
        [Test]
        public void GetAllResumes_Success()
        {
            var mockDbSet = new Mock<DbSet<ResumeForUser>>();

            var testData = new List<ResumeForUser>
            {
                new ResumeForUser
                {
                    Id = 1,
                    Name = "Bob",
                    AboutMe = "SMTH",
                    PictureUrl = "photo.jpg",
                    Email = "bob@test.com",
                    PhoneNumber = "1234567890",
                    Profession = "Web-Developer",
                    Skills = "Web-Development"
                }
            };

            mockDbSet.As<IQueryable<ResumeForUser>>().Setup(m => m.Provider).Returns(testData.AsQueryable().Provider);
            mockDbSet.As<IQueryable<ResumeForUser>>().Setup(m => m.Expression).Returns(testData.AsQueryable().Expression);
            mockDbSet.As<IQueryable<ResumeForUser>>().Setup(m => m.ElementType).Returns(testData.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<ResumeForUser>>().Setup(m => m.GetEnumerator()).Returns(testData.GetEnumerator());

            var mockDbContext = new Mock<IAppIdentityDbContext>();
            mockDbContext.Setup(x => x.Resume).Returns(mockDbSet.Object);

            var sut = new ResumeRepository(mockDbContext.Object);

            var res = sut.GetAllResumes();

            MyLogger.Instance.Logger.LogInformation(res.ToString());

            Assert.AreEqual(testData, res);
        }
    }
}
