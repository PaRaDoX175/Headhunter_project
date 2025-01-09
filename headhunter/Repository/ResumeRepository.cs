using headhunter.Dtos;
using headhunter.Entities;

namespace headhunter.Repository
{
    public class ResumeRepository : IResumeRepository
    {
        //private readonly IGenericRepository<Resume> _repo;

        //public ResumeRepository(IGenericRepository<Resume> repo)
        //{
        //    _repo = repo;
        //}

        //public List<Resume> GetAllResumes()
        //{
        //    return _repo.GetList();
        //}

        //public Task<Resume> GetResumeById(int id)
        //{
        //    throw new NotImplementedException();
        //}
        private readonly IAppIdentityDbContext _repo;

        public ResumeRepository(IAppIdentityDbContext repo)
        {
            _repo = repo;
        }

        public List<ResumeForUser> GetAllResumes()
        {
            return _repo.Resume.ToList();
        }

        public Task<Resume> GetResumeById(int id)
        {
            throw new NotImplementedException();
        }

    }
}
