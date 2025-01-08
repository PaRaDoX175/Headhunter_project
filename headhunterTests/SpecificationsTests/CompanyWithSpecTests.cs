using headhunter.Sorting;
using headhunter.Specifications;

namespace headhunterTests.SpecificationsTests
{
    [TestFixture]
    public class CompanyWithSpecTests
    {
        [Test]
        [TestCase(1)]
        public void CompanyWithSpec_PassingId(int id)
        {
            //Act
            var companyWithSpec = new CompanyWithSpec(id);

            //Assert
            Assert.That(companyWithSpec.Includes.Any(x => x.ToString().Contains("x.Vacancies")));
        }

        [Test]
        public void CompanyWithSpec_PassingPagination()
        {
            // var pag = new Pagination
            // {
            //     PageIndex = 1,
            //     PageSize = 6,
            //     Sort = "asc",
            //     Search = ""
            // };

            // var companyWithSpec = new CompanyWithSpec(pag);

            // Assert.That(companyWithSpec.Includes.Any(x => x.ToString().Contains("x.Vacancies")));
            // Assert.That(companyWithSpec.isPagingEnabled, Is.True);
        }
    }
}
