using headhunter.Entities;
using headhunter.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace headhunter.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }


        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetList()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public Task<T> GetEntityWithSpec(BaseSpecification<T> spec, int id)
        {
            var result = ApplySpecification(spec).FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public Task<List<T>> GetListAsyncSpec(BaseSpecification<T> spec)
        {
            return ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> CountAsync(BaseSpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        private IQueryable<T> ApplySpecification(BaseSpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }

        public async Task Add(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteAll()
        {
            _context.Set<T>().RemoveRange(_context.Set<T>());
        }
    }
}
