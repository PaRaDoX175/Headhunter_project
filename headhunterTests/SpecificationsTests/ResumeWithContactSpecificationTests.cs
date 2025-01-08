using headhunter.Entities;
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
    public class ResumeWithContactSpecificationTests
    {
        [Test]
        public void ResumeWithContactSpecification_PassingPagination()
        {
            // var pag = new Pagination
            // {
            //     PageIndex = 1,
            //     PageSize = 6,
            //     Sort = "desc",
            //     Search = "Ruslan"
            // };

            // var spec = new ResumeWithContactSpecification(pag);
            // string search = spec.Criteria.Body.ToString();  

            // Assert.That(spec.Includes.Any(x => x.ToString().Contains("x.ContactInformation")));
            // Assert.That(spec.isPagingEnabled, Is.True);
            // Assert.That(spec.OrderByDesc, Is.Not.Null);
            // Assert.That(spec.OrderBy, Is.Null);
            // Assert.That(search.Contains("(IsNullOrEmpty(value(headhunter.Specifications.ResumeWithContactSpecification+<>c__DisplayClass0_0).pagination.Search) OrElse x.Name.ToLower().Contains(value(headhunter.Specifications.ResumeWithContactSpecification+<>c__DisplayClass0_0).pagination.Search))"));
        }

        [Test]
        [TestCase(1)]
        public void ResumeWithContactSpecification_PassingId(int resumeId)
        {
            var r = new Resume { ContactInformationId = resumeId };
            var spec = new ResumeWithContactSpecification(resumeId);

            var compile = spec.Criteria.Compile();
            Assert.That(spec.Includes.Any(x => x.ToString().Contains("x.ContactInformation")));
            Assert.That(compile(r));
        }
    }
}
