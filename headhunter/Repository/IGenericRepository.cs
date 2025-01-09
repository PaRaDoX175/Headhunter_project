using headhunter.Specifications;

namespace headhunter.Repository
{
    public interface IGenericRepository<T>
    {
        Task<T> GetById(int id);
        Task<List<T>> GetList();
        Task<T> GetEntityWithSpec(BaseSpecification<T> spec, int id);
        Task<List<T>> GetListAsyncSpec(BaseSpecification<T> spec);
        Task<int> CountAsync(BaseSpecification<T> spec);
        Task Add(T entity);
        Task Update(T entity);
        void Delete(T entity);
        void DeleteAll();
    }
}
