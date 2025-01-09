using headhunter.Entities;
using headhunter.Sorting;

namespace headhunter.Specifications
{
    public class CompanyWithSpec : BaseSpecification<Company>
    {
        public CompanyWithSpec(int id) : base(x => x.Id == id)
        {
            AddIncludes(x => x.Vacancies);
        }

        public CompanyWithSpec(ProductSpecParams pagination)
        {
            AddIncludes(x => x.Vacancies);
            Pagination((int)pagination.PageSize, (int)(pagination.PageSize * (pagination.PageIndex - 1)));
        }


        //private static Expression<Func<Company, bool>> BuildCriteria(Pagination p)
        //{
        //    if(string.IsNullOrEmpty(p.Search))
        //    {
        //        return x => true;
        //    } else
        //    {
        //        var v = p.Search.ToLower();
        //        return x => x.Vacancies.Any(x => x.Position.ToLower().Contains(v));
        //    }

        //}

        //private static Expression<Func<Vacancy, VacancyWithSpec>> Sorting(Pagination p)
        //{
        //    if (!string.IsNullOrEmpty(p.Sort))
        //    {
        //        return x => new VacancyWithSpec(p);
        //    } else
        //    {
        //        return null;
        //    }
        //}
    }
}
