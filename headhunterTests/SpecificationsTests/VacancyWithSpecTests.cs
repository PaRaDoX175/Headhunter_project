using headhunter.Sorting;
using headhunter.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace headhunterTests.SpecificationsTests
{
    [TestFixture]
    public class VacancyWithSpecTests
    {
        [Test]
        public void VacancyWithSpec_PassingPagination()
        {
            // var pag = new Pagination
            // {
            //     PageIndex = 1,
            //     PageSize = 6,
            //     Sort = "asc",
            //     Search = ""
            // };

            // var spec = new VacancyWithSpec(pag);
            // var search = spec.Criteria.Body.ToString();

            // Assert.That(spec.OrderBy, Is.Not.Null);
            // Assert.That(spec.OrderByDesc, Is.Null);
            // Assert.That(spec.isPagingEnabled, Is.True);
            // Assert.That(search.Contains("(IsNullOrEmpty(value(headhunter.Specifications.VacancyWithSpec+<>c__DisplayClass0_0).pagination.Search) OrElse x.Position.ToLower().Contains(value(headhunter.Specifications.VacancyWithSpec+<>c__DisplayClass0_0).pagination.Search))"));
        }
    }
}
