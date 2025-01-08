using AutoMapper;
using headhunter.Controllers;
using headhunter.Dtos;
using headhunter.Entities;
using headhunter.Repository;
using headhunter.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace headhunterTests.AccountControllerTests
{
    [TestFixture]
    public class SendEmailTests
    {
        private readonly AccountController _sut;
        private static readonly Mock<IUserStore<AppUser>> userStoreMock = new Mock<IUserStore<AppUser>>();
        private static readonly Mock<UserManager<AppUser>> userManager = new Mock<UserManager<AppUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
        private readonly Mock<SignInManager<AppUser>> signInManager = new Mock<SignInManager<AppUser>>
            (userManager.Object, new Mock<IHttpContextAccessor>().Object, new Mock<IUserClaimsPrincipalFactory<AppUser>>().Object, null, null, null, null);
        private readonly Mock<ITokenService> tokenService = new Mock<ITokenService>();
        private readonly Mock<IMapper> mapper = new Mock<IMapper>();
        private readonly Mock<IGenericRepository<Vacancy>> repoVac = new Mock<IGenericRepository<Vacancy>>();
        private readonly Mock<IRepositoryIdentity> repoIdentity = new Mock<IRepositoryIdentity>();
        public SendEmailTests()
        {
            _sut = new AccountController(userManager.Object, signInManager.Object, tokenService.Object, mapper.Object, repoVac.Object, repoIdentity.Object);
        }

        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, "bob@test.email"),
            new Claim(ClaimTypes.GivenName, "Bobban")
        };

        AppUser user = new AppUser
        {
            DisplayName = "Bobban",
            FirstName = "Bob",
            LastName = "Bobban",
            Resume = new ResumeForUser
            {
                Id = 1,
                Name = "Bob",
                AboutMe = "Smth",
                PictureUrl = "photo.jpg",
                Email = "bob@gmail.com",
                PhoneNumber = "1234567890",
                Profession = "Web-Developer",
                Skills = "Web-Development"
            },
            ResumeId = 1,
            UserName = "Bobban",
            Email = "bob@gmail.com",
            Password = "Pa$$w0rd",
            PhoneNumber = "1234567890",
        };

        [Test]
        public async Task SendEmail_IdAndFindUser_ReturnString()
        {
            int id = 1;
            var vacancy = new Vacancy
            {
                Id = 1,
                Position = "Web-Developer",
                Salary = 5000,
                Department = "Web-Development",
                Location = "Somewhere",
                Requirements = "Idk",
                Responsibilities = "Idk",
                Email = "absalyamov.ruslan01@gmail.com",
                CompanyId = 1
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            
            var controllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            controllerContext.HttpContext.User = principal;
            _sut.ControllerContext = controllerContext;

            userManager.Setup(x => x.FindByEmailAsync(principal.FindFirstValue(ClaimTypes.Email))).ReturnsAsync(user);
            repoIdentity.Setup(x => x.GetEntityById(user.ResumeId)).ReturnsAsync(user);
            repoVac.Setup(x => x.GetById(id)).ReturnsAsync(vacancy);

            var res = await _sut.SendEmail(id);

            Assert.IsNotNull(res);
            Assert.That(res.Value, Is.EqualTo("The resume was send"));
        }

        [Test]
        public async Task SendEmail_UserIsNull_ShouldReturnUnauthorized()
        {
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);
            var controllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            controllerContext.HttpContext.User = principal;
            _sut.ControllerContext = controllerContext;

            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(() => null);

            var res = await _sut.SendEmail(1);

            Assert.IsNotNull(res);
            Assert.IsInstanceOf<UnauthorizedObjectResult>(res.Result);
        }
    }
}
