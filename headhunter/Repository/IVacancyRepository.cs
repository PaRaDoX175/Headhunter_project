using headhunter.Entities;

namespace headhunter.Repository
{
    public interface IVacancyRepository
    {
        Task<List<Vacancy>> GetSame(int id, string position);
    }
}
