using AutoMapper;
using headhunter.Controllers;
using headhunter.Dtos;
using headhunter.Entities;
using headhunter.Repository;
using headhunter.Specifications;
using Moq;
using System.Runtime.CompilerServices;

namespace headhunterTests
{
    [TestFixture]
    public class CompanyControllerTests
    {
        private readonly CompanyController _sut;
        private readonly Mock<IGenericRepository<Company>> _repo = new Mock<IGenericRepository<Company>>();
        private readonly Mock<IGenericRepository<Vacancy>> _repoVac = new Mock<IGenericRepository<Vacancy>>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private readonly Mock<IVacancyRepository> _vacRepo = new();
        public CompanyControllerTests()
        {
            _sut = new CompanyController(_repo.Object, _repoVac.Object, _mapper.Object, _vacRepo.Object);
        }

        [Test]
        public async Task GetVacancies_ShouldReturnListOfVacancies()
        {
            int vacancyId = 1;
            var vacancies = new List<Vacancy>
            {
                new Vacancy
                {
                    Id = vacancyId,
                    Position = "Position",
                    Salary = 5050,
                    Department = "Department",
                    Location = "Location",
                    Requirements = "Requirements",
                    Responsibilities = "Responsibilities",
                    Email = "r@gmail.com",
                    CompanyId = vacancyId
                }
            };

            var vacanciesDto = new List<VacancyDto>
            {
                new VacancyDto
                {
                    Position = "Position",
                    Salary = 5050,
                    Department = "Department",
                    Location = "Location",
                    Requirements = "Requirements",
                    Responsibilities = "Responsibilities",
                    Email = "r@gmail.com",
                }
            };

            _repoVac.Setup(x => x.GetListAsyncSpec(It.IsAny<VacancyWithSpec>())).ReturnsAsync(vacancies);
            _mapper.Setup(x => x.Map<List<Vacancy>, List<VacancyDto>>(vacancies)).Returns(vacanciesDto);

            var res = await _sut.GetVacancies("asc");

            Assert.NotNull(res);
            Assert.IsInstanceOf<List<VacancyDto>>(res.Value);
            Assert.That(vacancies.Count, Is.EqualTo(vacanciesDto.Count));
        }
    }
}
