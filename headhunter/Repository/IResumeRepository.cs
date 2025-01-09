using headhunter.Dtos;
using headhunter.Entities;

namespace headhunter.Repository
{
    public interface IResumeRepository
    {
        Task<Resume> GetResumeById(int id);
        List<ResumeForUser> GetAllResumes();
    }
}
