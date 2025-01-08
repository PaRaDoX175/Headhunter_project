using AutoMapper;
using headhunter.Controllers;
using headhunter.Dtos;
using headhunter.Entities;
using headhunter.Interfaces;
using headhunter.Repository;
using headhunter.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace headhunterTests.AccountControllerTests
{
    [TestFixture]
    public class UserLoginTests
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
        public UserLoginTests()
        {
            _sut = new AccountController(userManager.Object, signInManager.Object, tokenService.Object, mapper.Object, repoVac.Object, repoIdentity.Object);
        }

        LoginDto login = new LoginDto
        {
            Email = "bob@gmail.com",
            Password = "Pa$$w0rd"
        };

        AppUser user = new AppUser
        {
            DisplayName = "Bobban",
            FirstName = "Bob",
            LastName = "Bobban",
            UserName = "Bobban",
            Email = "bob@gmail.com",
            Password = "Pa$$w0rd",
            PhoneNumber = "1234567890",
        };

        [Test]
        public async Task UserLogin_LoginDto_ShouldReturnUserDto()
        {
            userManager.Setup(x => x.FindByEmailAsync(login.Email)).ReturnsAsync(user);
            signInManager.Setup(x => x.CheckPasswordSignInAsync(user, login.Password, false)).ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);
            tokenService.Setup(x => x.CreateToken(user)).Returns("This is a token");

            var res = await _sut.UserLogin(login);
            
            Assert.IsNotNull(res);
            Assert.IsInstanceOf<ActionResult<UserDto>>(res);
            var okResult = (OkObjectResult)res.Result;
            Assert.IsInstanceOf<UserDto>((UserDto)okResult.Value);
        }

        [Test]
        public async Task UserLogin_LoginDto_ShouldReturnUnauthorized()
        {
            userManager.Setup(x => x.FindByEmailAsync(login.Email)).ReturnsAsync(() => null);

            var res = await _sut.UserLogin(login);

            Assert.IsNotNull(res);
            Assert.IsInstanceOf<UnauthorizedObjectResult>(res.Result);
        }

        [Test]
        public async Task UserLogin_LoginDto_ShouldReturnBadRequest()
        {
            userManager.Setup(x => x.FindByEmailAsync(login.Email)).ReturnsAsync(user);
            signInManager.Setup(x => x.CheckPasswordSignInAsync(user, login.Password, false)).ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

            var res = await _sut.UserLogin(login);

            Assert.IsNotNull(res);
            Assert.IsInstanceOf<BadRequestObjectResult>(res.Result);
        }
    }
}
