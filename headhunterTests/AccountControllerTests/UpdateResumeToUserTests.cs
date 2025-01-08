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
    public class UpdateResumeToUserTests
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
        public UpdateResumeToUserTests()
        {
            _sut = new AccountController(userManager.Object, signInManager.Object, tokenService.Object, mapper.Object, repoVac.Object, repoIdentity.Object);
        }

        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, "bob@gmail.com"),
            new Claim(ClaimTypes.GivenName, "Bobban")
        };

        static ResumeForUser resume = new ResumeForUser
        {
            Id = 1,
            Name = "Bob",
            AboutMe = "Info",
            PictureUrl = "photo",
            Email = "bob@gmail.com",
            PhoneNumber = "1234567890",
            Profession = "Web-Developer",
            Skills = "Web-Development"
        };

        AppUser user = new AppUser
        {
            DisplayName = "Bobban",
            FirstName = "Bob",
            LastName = "Bobbanov",
            Resume = It.IsAny<ResumeForUser>(),
            ResumeId = It.IsAny<int>(),
            Email = "bob@gmail.com",
            Password = "Privet&!a1",
            PhoneNumber = "1234567890",
        };

        AppUserDto userDto = new AppUserDto
        {
            DisplayName = "Bobban",
            FirstName = "Bob",
            LastName = "Bobbanov",
            Resume = resume,
            Email = "bob@gmail.com",
            Password = "Privet&!a1",
            PhoneNumber = "1234567890",
        };

        [Test]
        public async Task UpdateResumeToUser_ResumeForUser_ShouldReturnUserDto()
        {


            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var controllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            controllerContext.HttpContext.User = claimsPrincipal;
            _sut.ControllerContext = controllerContext;

            userManager.Setup(x => x.FindByEmailAsync(user.Email)).ReturnsAsync(user);
            userManager.Setup(x => x.UpdateAsync(user)).ReturnsAsync(IdentityResult.Success);
            mapper.Setup(x => x.Map<AppUser, AppUserDto>(user)).Returns(userDto);

            var res = await _sut.UpdateResumeToUser(resume);

            Assert.NotNull(res);
            Assert.IsInstanceOf<ActionResult<AppUserDto>>(res);
            // Assert.That(res.Value.Resume.Email, Is.EqualTo(user.Resume.Email));
        }

        [Test]
        public async Task UpdateResumeToUser_ResumeForUser_ShouldReturnNotFound()
        {
            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var controllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            controllerContext.HttpContext.User = claimsPrincipal;
            _sut.ControllerContext = controllerContext;

            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(() => null);

            var res = await _sut.UpdateResumeToUser(resume);

            Assert.NotNull(res);
            Assert.IsInstanceOf<NotFoundObjectResult>(res.Result);
        }

        [Test]
        public async Task UpdateResumeToUser_ResumeForUser_ShouldReturnBadRequest()
        {
            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var controllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            controllerContext.HttpContext.User = claimsPrincipal;
            _sut.ControllerContext = controllerContext;

            userManager.Setup(x => x.FindByEmailAsync(user.Email)).ReturnsAsync(user);
            userManager.Setup(x => x.UpdateAsync(user)).ReturnsAsync(IdentityResult.Failed());

            var res = await _sut.UpdateResumeToUser(resume);

            Assert.NotNull(res);
            Assert.IsInstanceOf<BadRequestObjectResult>(res.Result);
        }
    }
}
