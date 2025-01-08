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

namespace headhunterTests.AccountControllerTests
{
    [TestFixture]
    public class UserRegisterTests
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
        public UserRegisterTests()
        {
            _sut = new AccountController(userManager.Object, signInManager.Object, tokenService.Object, mapper.Object, repoVac.Object, repoIdentity.Object);
        }

        RegisterDto reg = new RegisterDto
        {
            DisplayName = "Bobban",
            FirstName = "Bob",
            LastName = "Bobbanov",
            Resume = new ResumeForUser
            {
                Id = 1,
                Name = "Bob",
                AboutMe = "Info",
                PictureUrl = "photo",
                Email = "bob@gmail.com",
                PhoneNumber = "1234567890",
                Profession = "Web-Developer",
                Skills = "Web-Development"
            },
            Email = "bob@gmail.com",
            Password = "Privet&!a1",
            PhoneNumber = "1234567890",
        };

        [Test]
        public async Task UserRegister_AppUser_ShouldReturnUserDto()
        {
            var register = reg;

            tokenService.Setup(x => x.CreateToken(It.IsAny<AppUser>())).Returns(It.IsAny<string>());

            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(() => null);
            userManager.Setup(x => x.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            var res = await _sut.UserRegister(register);

            Assert.IsNotNull(res);
            Assert.IsInstanceOf<ActionResult<UserDto>>(res);
            var okResult = (OkObjectResult)res.Result;
            Assert.IsInstanceOf<UserDto>((UserDto)okResult.Value);
        }

        [Test]
        public async Task UserRegister_AppUserWithExistingEmail_ShouldReturnBadRequest()
        {
            var user = new AppUser
            {
                DisplayName = reg.DisplayName,
                FirstName = reg.FirstName,
                LastName = reg.LastName,
                UserName = reg.DisplayName,
                Resume = reg.Resume,
                ResumeId = reg.Resume.Id,
                Email = reg.Email,
                Password = reg.Password,
                PhoneNumber = reg.PhoneNumber,
            };

            userManager.Setup(x => x.FindByEmailAsync(user.Email)).ReturnsAsync(user);

            var res = await _sut.UserRegister(reg);

            Assert.IsNotNull(res);
            Assert.IsInstanceOf<BadRequestObjectResult>(res.Result);
        }

        [Test]
        public async Task UserRegister_AppUser_ShouldReturnBadRequest()
        {
            userManager.Setup(x => x.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed());

            var res = await _sut.UserRegister(reg);

            Assert.IsNotNull(res);
            Assert.IsInstanceOf<BadRequestObjectResult>(res.Result);
        }
    }
}
