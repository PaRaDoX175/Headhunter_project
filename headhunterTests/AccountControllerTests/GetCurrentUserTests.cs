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
    public class GetCurrentUserTests
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
        public GetCurrentUserTests()
        {
            _sut = new AccountController(userManager.Object, signInManager.Object, tokenService.Object, mapper.Object, repoVac.Object, repoIdentity.Object);
        }

        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, "bob@gmail.com"),
            new Claim(ClaimTypes.GivenName, "Bobban")
        };

        [Test]
        public async Task GetCurrentUser_ShouldReturnLoginDto()
        {
            var user = new AppUser
            {
                DisplayName = "Bobban",
                FirstName = "Bob",
                LastName = "Bobban",
                UserName = "Bobban",
                Email = "bob@gmail.com",
                Password = "Pa$$w0rd",
                PhoneNumber = "1234567890",
            };

            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var controllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            controllerContext.HttpContext.User = claimsPrincipal;

            userManager.Setup(x => x.FindByEmailAsync(user.Email)).ReturnsAsync(user);

            _sut.ControllerContext = controllerContext;

            var res = await _sut.GetCurrentUser();

            Assert.IsNotNull(res);
            Assert.IsInstanceOf<LoginDto>(res);
            Assert.That(res.Email, Is.EqualTo(user.Email));
            Assert.That(res.Password, Is.EqualTo(user.Password));
        }

        [Test]
        public async Task GetCurrentUser_ShouldReturnNull()
        {
            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var controllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            controllerContext.HttpContext.User = claimsPrincipal;

            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(() => null);

            _sut.ControllerContext = controllerContext;

            var res = await _sut.GetCurrentUser();

            Assert.IsNull(res);
        }
    }
}
