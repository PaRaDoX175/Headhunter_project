using headhunter.Entities;
using headhunter.Sorting;

namespace headhunter.Specifications
{
    public class ResumeWithContactSpecification : BaseSpecification<Resume>
    {
        public ResumeWithContactSpecification(ProductSpecParams pagination) 
            : base(x => (string.IsNullOrEmpty(pagination.Search) || x.Name.ToLower().Contains(pagination.Search)))
        {
            AddIncludes(x => x.ContactInformation);
            Pagination((int)pagination.PageSize, (int)(pagination.PageSize * (pagination.PageIndex - 1)));

            if (!string.IsNullOrEmpty(pagination.Sort))
            {
                switch (pagination.Sort)
                {
                    case "asc":
                        AddOrderBy(x => x.Name);
                        break;
                    case "desc":
                        AddOrderByDesc(x => x.Name);
                        break;
                    default: 
                        AddOrderBy(x => x.Name); 
                        break;
                }
            }
        }

        public ResumeWithContactSpecification(int id) : base(x => x.ContactInformationId == id)
        {
            AddIncludes(x => x.ContactInformation);
        }
    }
}
