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
using System.Security.Claims;

namespace headhunterTests.AccountControllerTests
{
    [TestFixture]
    public class GetResumeTests
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
        public GetResumeTests()
        {
            _sut = new AccountController(userManager.Object, signInManager.Object, tokenService.Object, mapper.Object, repoVac.Object, repoIdentity.Object);
        }

        static List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, "bob@gmail.com"),
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

        static ClaimsIdentity identity = new ClaimsIdentity(claims);
        ClaimsPrincipal principal = new ClaimsPrincipal(identity);

        [Test]
        public async Task GetResume_FindUser_ShouldReturnResume()
        {
            var controllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            controllerContext.HttpContext.User = principal;
            _sut.ControllerContext = controllerContext;

            userManager.Setup(x => x.FindByEmailAsync(principal.FindFirstValue(ClaimTypes.Email))).ReturnsAsync(user);
            repoIdentity.Setup(x => x.GetEntityById(user.ResumeId)).ReturnsAsync(user);

            var res = await _sut.GetResume();

            Assert.IsNotNull(res);
            Assert.IsInstanceOf<ActionResult<ResumeForUser>>(res);
            Assert.AreEqual(user.Resume, res.Value);
        }

        [Test]
        public async Task GetResume_DontFindUser_ShouldReturnNotFound()
        {
            var controllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            controllerContext.HttpContext.User = principal;
            _sut.ControllerContext = controllerContext;

            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(() => null);

            var res = await _sut.GetResume();

            Assert.IsNotNull(res);
            Assert.IsInstanceOf<NotFoundObjectResult>(res.Result);
        }
    }
}
