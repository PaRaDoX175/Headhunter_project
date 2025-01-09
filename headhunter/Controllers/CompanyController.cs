using AutoMapper;
using headhunter.Dtos;
using headhunter.Entities;
using headhunter.Errors;
using headhunter.Repository;
using headhunter.Sorting;
using headhunter.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace headhunter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly IGenericRepository<Company> _repo;
        private readonly IGenericRepository<Vacancy> _repoVac;
        private readonly ILogger<CompanyController> _logger;
        private readonly IMapper _mapper;
        private readonly IVacancyRepository _vacancyRepository;

        public CompanyController(IGenericRepository<Company> repo, IGenericRepository<Vacancy> repoVac, IMapper mapper, IVacancyRepository vacancyRepository)
        {
            _repo = repo;
            _repoVac = repoVac;
            _mapper = mapper;
            _vacancyRepository = vacancyRepository;
        }

        [HttpPost]
        public async Task<CompanyDto> PostCompany([FromBody] Company company)
        {
            await _repo.Add(company);
            var companyDto = _mapper.Map<Company, CompanyDto>(company);
            return companyDto;
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<CompanyDto>> UpdateCompany(int id, [FromBody] Vacancy vac)
        {
            vac.CompanyId = id;
            var c = await _repo.GetById(id);
            vac.CompanyName = c.Name;
            await _repoVac.Add(vac);

            var companyDto = _mapper.Map<Company, CompanyDto>(await _repo.GetById(id));

            return companyDto;
        }

        [HttpGet("vacancy")]
        public async Task<ActionResult<Pagination<VacancyDto>>> GetVacancies([FromQuery] string sort, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 6, [FromQuery] string search = "")
        {
            var prodSpecParams = new ProductSpecParams
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Sort = sort,
                Search = search
            };
            var v = new VacancyWithSpec(prodSpecParams);

            var vacancies = await _repoVac.GetListAsyncSpec(v);

            List<Vacancy> all;
            int count;

            if (search != "")
            {
                count = vacancies.Count;
            }
            else
            {
                all = await _repoVac.GetList();
                count = all.Count;
            }

            if (vacancies == null)
            {
                return NotFound(new ApiException(404));
            }

            var vacanciesDto = _mapper.Map<List<Vacancy>, List<VacancyDto>>(vacancies);

            return Ok(new Pagination<VacancyDto>(pageIndex, pageSize, vacanciesDto, count));
        }

        [HttpGet("all_vacancy")]
        public async Task<ActionResult<int>> GetVacanciesCount()
        {
            var vacanciesCount = await _repoVac.GetList();

            if (vacanciesCount == null)
            {
                return NotFound(new ApiException(404));
            }

            return vacanciesCount.Count;
        }

        [HttpGet("vacancy/{id}")]
        public async Task<VacancyDto> GetVacancyById(int id)
        {
            var vac = await _repoVac.GetById(id);
            var res = _mapper.Map<Vacancy, VacancyDto>(vac);
            return res;
        }

        [HttpGet("vacancy/{id}/same_vacancy")]
        public async Task<ActionResult<List<VacancyDto>>> GetSameVacancy(int id)
        {
            var vac = await _repoVac.GetById(id);
            var same = await _vacancyRepository.GetSame(id, vac.Position);
            return Ok(same);
        }

        [HttpGet("all_company")]
        public async Task<ActionResult<int>> GetCompaniesCount()
        {
            var companiesCount = await _repo.GetList();

            if (companiesCount == null)
            {
                return NotFound(new ApiException(404));
            }

            return companiesCount.Count;
        }


        [HttpGet("company")]
        public async Task<Pagination<CompanyDto>> GetCompanies([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 6, [FromQuery] string search = "", [FromQuery] string sort = "")
        {
            var prodSpecParams = new ProductSpecParams
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Sort = sort,
                Search = search
            };

            var c = new CompanyWithSpec(prodSpecParams);

            var companies = await _repo.GetListAsyncSpec(c);
            var all = await _repo.GetList();
            int count = all.Count;

            var companyDto = _mapper.Map<List<Company>, List<CompanyDto>>(companies);

            return new Pagination<CompanyDto>(pageIndex, pageSize, companyDto, count);
        }

        [HttpGet("company/{id}")]
        public async Task<CompanyDto> GetCompanyById(int id)
        {
            var companySpec = new CompanyWithSpec(id);
            var company = await _repo.GetEntityWithSpec(companySpec, id);
            var companyDto = _mapper.Map<Company, CompanyDto>(company);
            return companyDto;
        }
    }
}
