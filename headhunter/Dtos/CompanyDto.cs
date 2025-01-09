using headhunter.Entities;

namespace headhunter.Dtos
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Industry { get; set; }
        public int FoundedYear { get; set; }
        public string Description { get; set; }
        public List<VacancyDto> Vacancies { get; set; }
    }
}
