using headhunter.Entities;
using headhunter.Sorting;

namespace headhunter.Specifications
{
    public class VacancyWithSpec : BaseSpecification<Vacancy>
    {
        public VacancyWithSpec(ProductSpecParams pagination) : base(x => string.IsNullOrEmpty(pagination.Search) || x.Position.ToLower().Contains(pagination.Search))
        {
            AddOrderBy(x => x.Salary);
            Pagination((int)pagination.PageSize, (int)(pagination.PageSize * (pagination.PageIndex - 1)));

            if (!string.IsNullOrEmpty(pagination.Sort))
            {
                switch (pagination.Sort)
                {
                    case "asc":
                        AddOrderBy(x => x.Salary);
                        break;
                    case "desc":
                        AddOrderByDesc(x => x.Salary);
                        break;
                }
            }
        }
    }
}
