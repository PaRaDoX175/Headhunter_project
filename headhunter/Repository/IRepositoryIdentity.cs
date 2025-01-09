using headhunter.Entities;

namespace headhunter.Repository
{
    public interface IRepositoryIdentity
    {
        Task<AppUser> GetEntityById(int id);
    }
}
