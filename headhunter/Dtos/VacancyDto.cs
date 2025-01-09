using headhunter.Entities;

namespace headhunter.Dtos
{
    public class VacancyDto
    {
        public int Id { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public int Salary { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Requirements { get; set; }
        public string Responsibilities { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public int CompanyId { get; set; }
    }
}
