using AutoMapper;
using headhunter.Dtos;
using headhunter.Entities;
using headhunter.Errors;
using headhunter.Repository;
using headhunter.Sorting;
using headhunter.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace headhunter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResumeController : ControllerBase
    {
        //private readonly StoreContext _context;
        private readonly IMapper _mapper;
        //private readonly ILogger<ResumeController> _logger;
        private readonly IGenericRepository<Resume> _repo;
        private readonly IGenericRepository<ContactInformation> _repoCont;
        private readonly IIdentityGenericRepository<ResumeForUser> _resumeForUser;

        public ResumeController(IMapper mapper, IGenericRepository<Resume> repo, IGenericRepository<ContactInformation> repoCont, IIdentityGenericRepository<ResumeForUser> resumeForUser)
        {
            _resumeForUser = resumeForUser;
            //_context = context;
            _mapper = mapper;
            _repo = repo;
            _repoCont = repoCont;
        }

        [HttpGet("all_resume")]
        public async Task<int> GetResumesCount()
        {
            var data = await _resumeForUser.GetList();
            return data.Count;
        }

        // [HttpGet]
        // public async Task<ActionResult<Pagination<ResumeDto>>> GetAll([FromQuery] string sort, [FromQuery] int pageSize = 6, [FromQuery] int pageIndex = 1, string search = "")
        // {
        //     var prodSpecParams = new ProductSpecParams
        //     {
        //         PageIndex = pageIndex,
        //         PageSize = pageSize,
        //         Sort = sort,
        //         Search = search
        //     };

        //     var spec = new ResumeWithContactSpecification(prodSpecParams);
        //     var repo = await _repo.GetListAsyncSpec(spec);
        //     var all = await _repo.GetList();
        //     int count = all.Count;

        //     if (repo == null)
        //     {
        //         return NotFound(new ApiException(404));
        //     }
        //     var resumesDto = _mapper.Map<List<Resume>, List<ResumeDto>>(repo);

        //     return Ok(new Pagination<ResumeDto>(pageIndex, pageSize, resumesDto, count));
        // }

        // [HttpGet("all_resume")]
        // public async Task<ActionResult<int>> GetResumesCount()
        // {
        //     var resumesCount = await _repo.GetList();

        //     if (resumesCount == null)
        //     {
        //         return NotFound(new ApiException(404));
        //     }

        //     return resumesCount.Count;
        // }

        // [HttpGet("{id}")]
        // public async Task<ActionResult<ResumeDto>> GetById(int id)
        // {
        //     var spec = new ResumeWithContactSpecification(id);
        //     var repo = await _repo.GetEntityWithSpec(spec, id);
        //     if (repo == null)
        //     {
        //         return NotFound(new ApiException(404));
        //     }
        //     var resumeDto = _mapper.Map<Resume, ResumeDto>(repo);
        //     return Ok(resumeDto);
        // }

        // [HttpPost]
        // public async Task<ActionResult<ResumeDto>> PostResume([FromBody] Resume resume)
        // {
        //     await _repo.Add(resume);
        //     var resumeDto = _mapper.Map<Resume, ResumeDto>(resume);
        //     return resumeDto;
        // }

        // [Authorize]
        // [HttpPut("updateresume")]
        // public async Task<ActionResult<ResumeDto>> UpdateResume([FromBody] Resume resume)
        // {
        //     await _repo.Update(resume);
        //     var resumeDto = _mapper.Map<Resume, ResumeDto>(resume);
        //     return resumeDto;
        // }

        //[HttpGet("{id}")]
        //public async Task<Resume> GetById(int id)
        //{
        //    var spec = new ResumeWithContactSpecification(id);
        //    var repo = await _repo.GetEntityWithSpec(spec, id);
        //    if (repo == null)
        //    {
        //        return null;
        //    }
        //    return repo;
        //}

        //[HttpGet("{idTest}")]
        //public async Task<Resume> GetByIdTest(int idTest)
        //{
        //    var repo = await _repo.GetById(idTest);
        //    if (repo == null)
        //    {
        //        //var smth = NotFound(new ApiException(404));
        //        return null;
        //        //return null;
        //    }
        //    //var resumeDto = _mapper.Map<Resume, ResumeDto>(repo);
        //    return repo;
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Resume>> DeleteResumeById(int id)
        //{
        //    var resume = await _context.Resumes.FindAsync(id);
        //    var contact = await _context.ContactInformation.FindAsync(id);

        //    _repo.Delete(resume);
        //    _repoCont.Delete(contact);

        //    await _context.SaveChangesAsync();
        //    return resume;
        //}

        //[HttpDelete]
        //public async Task<ActionResult<string>> DeleteAllResumes()
        //{
        //    _context.Resumes.RemoveRange(_context.Resumes);
        //    _context.ContactInformation.RemoveRange(_context.ContactInformation);
        //    await _context.SaveChangesAsync();

        //    return "Your resumes was deleted!";
        //}
    }
}
