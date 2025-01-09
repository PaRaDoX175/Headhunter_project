using headhunter.Entities;
using Microsoft.EntityFrameworkCore;

namespace headhunter.Repository
{
    public class VacancyRepository : IVacancyRepository
    {
        private readonly StoreContext _context;

        public VacancyRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<List<Vacancy>> GetSame(int id, string position)
        {
            var s = position.ToLower().Split(' ').ToList();

            var vacancies = await _context.Vacancies.ToListAsync();

            return vacancies
                .Where(x => x.Id != id && s.Any(y => x.Position.ToLower().Contains(y)))
                .Take(2)
                .ToList();
        }
    }
}
