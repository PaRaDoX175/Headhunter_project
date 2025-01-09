using AutoMapper;
using headhunter.Dtos;
using headhunter.Entities;
using headhunter.Errors;
using headhunter.Repository;
using headhunter.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;

namespace headhunter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Vacancy> _repoVac;
        private readonly IRepositoryIdentity _repoIdentity;
        private readonly IGenericRepository<ResumeForUser> _resume;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            ITokenService tokenService, IMapper mapper, IGenericRepository<Vacancy> repoVac, IRepositoryIdentity repoIdentity)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _repoVac = repoVac;
            _repoIdentity = repoIdentity;
            // _resume = resume;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> UserRegister([FromBody] RegisterDto register)
        {

            var user = new AppUser
            {
                DisplayName = register.DisplayName,
                FirstName = register.FirstName,
                LastName = register.LastName,
                Resume = register.Resume,
                ResumeId = register.Resume.Id,
                Email = register.Email,
                Password = register.Password,
                UserName = register.DisplayName,
                PhoneNumber = register.PhoneNumber,
                BasketId = register.BasketId
            };

            if (await _userManager.FindByEmailAsync(user.Email) != null)
            {
                return BadRequest(new ApiValidationErrorResponse { Errors = new[] { "This address is already in use!" } });
            }

            var result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
            {
                string[] arr = new string[result.Errors.Count()];
                int i = 0;

                foreach (var item in result.Errors)
                {
                    arr[i] = item.Description;
                    i++;
                }

                return BadRequest(new ApiValidationErrorResponse { Errors = arr });
            }

            return Ok(new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            });
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> UserLogin([FromBody] LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null) return Unauthorized(new ApiException(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
            if (!result.Succeeded) return BadRequest(new ApiException(400));

            return Ok(new UserDto
            {
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email,
                BasketId = user.BasketId
            });
        }

        [Authorize]
        [HttpPost("sendemail")]
        public async Task<ActionResult<EmailResp>> SendEmail([FromQuery] int id, [FromBody] ResumeForUser resume)
        {
            ClaimsPrincipal claims = HttpContext.User;
            var user = await _userManager.FindByEmailAsync(claims.FindFirstValue(ClaimTypes.Email));
            if (user == null) return Unauthorized(new ApiException(401));
            // await _repoIdentity.GetEntityById(user.ResumeId);

            var vacancy = await _repoVac.GetById(id);

            // new SendAnEmail().SendMessageToEmail(user.Email, vacancy.Email, resume);
            new SendAnEmail().SendMessageToEmail(user.Email, "ruslan.absalyamov01@gmail.com", resume);

            return new EmailResp { Message = "The resume was send" };
        }

        // [Authorize]
        [HttpGet("istokenexpired")]
        public ActionResult<bool> IsTokenExpired([FromHeader(Name = "Authorization")] string token)
        {

            token = token.Replace("Bearer ", string.Empty);

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new ApiException(400, "There is no token"));
            }

            return _tokenService.IsTokenExpired(token);
        }

        [Authorize]
        [HttpGet("autologin")]
        public async Task<LoginDto> GetCurrentUser()
        {
            ClaimsPrincipal claim = HttpContext.User;
            var res = await _userManager.FindByEmailAsync(claim.FindFirstValue(ClaimTypes.Email));
            if (res == null) return null;
            return new LoginDto
            {
                Email = res.Email,
                Password = res.Password
            };
        }

        [Authorize]
        [HttpGet("getresume")]
        public async Task<ActionResult<ResumeForUser>> GetResume()
        {
            ClaimsPrincipal claims = HttpContext.User;
            var user = await _userManager.FindByEmailAsync(claims.FindFirstValue(ClaimTypes.Email));

            if (user == null)
            {
                return NotFound(new ApiException(404));
            }

            await _repoIdentity.GetEntityById(user.ResumeId);

            return user.Resume;
        }


        [Authorize]
        [HttpPut("updateresume")]
        public async Task<ActionResult<ResumeForUser>> UpdateResumeToUser([FromBody] ResumeForUser resume)
        {
            ClaimsPrincipal claim = HttpContext.User;
            var user = await _userManager.FindByEmailAsync(claim.FindFirstValue(ClaimTypes.Email));

            if (user == null)
            {
                return NotFound(new ApiException(404));
            }

            var userWithRes = await _userManager.Users.Include(x => x.Resume).FirstOrDefaultAsync(x => x.ResumeId == user.ResumeId);

            userWithRes.Resume.Name = resume.Name;
            userWithRes.Resume.AboutMe = resume.AboutMe;
            userWithRes.Resume.Email = resume.Email;
            userWithRes.Resume.PhoneNumber = resume.PhoneNumber;
            userWithRes.Resume.Profession = resume.Profession;
            userWithRes.Resume.Skills = resume.Skills;
            userWithRes.Resume.PictureUrl = resume.PictureUrl;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(new ApiException(400));
            }
            return user.Resume;
        }


    }
}
