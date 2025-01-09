namespace headhunter.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string Industry { get; set; }
        public int FoundedYear { get; set; }
        public string Description { get; set; }
        public List<Vacancy> Vacancies { get; set; }
    }
}
