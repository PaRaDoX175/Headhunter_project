using headhunter.Entities;
using headhunter.Repository;
using headhunter.Sorting;
using headhunter.Specifications;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace headhunterTests.SpecificationsTests
{
    [TestFixture]
    public class SpecificationEvaluatorTests
    {
        [Test]
        public void SpecificationEvaluator_GetQuery_WithId1()
        {
            int resumeId = 1;

            var resumes = new List<Resume>
            {
                new Resume
                {
                    Id = resumeId,
                    Name = "Ruslan",
                    AboutMe = "AboutMe",
                    PictureUrl = "photo.jpg",
                    ContactInformation = new ContactInformation
                    {
                        Id = resumeId,
                        Email = "r@gmail.com",
                        PhoneNumber = "1234567890",
                    },
                    ContactInformationId = resumeId,
                    Profession = "Web-Developer",
                    Skills = "Development"
                },
                new Resume
                {
                    Id = 2,
                    Name = "Ruslan",
                    AboutMe = "AboutMe",
                    PictureUrl = "photo.jpg",
                    ContactInformation = new ContactInformation
                    {
                        Id = 2,
                        Email = "r@gmail.com",
                        PhoneNumber = "1234567890",
                    },
                    ContactInformationId = 2,
                    Profession = "Web-Developer",
                    Skills = "Development"
                }
            };

            var resumesWithSpec = new ResumeWithContactSpecification(1);

            var specifEval = SpecificationEvaluator<Resume>.GetQuery(resumes.AsQueryable(), resumesWithSpec);

            Assert.That(resumes.Where(x => x.Id == 1), Is.EqualTo(specifEval));
        }

        [Test]
        public void SpecificationEvaluator_GetQuery_WithPagination()
        {
            // int resumeId = 1;

            // var resumes = new List<Resume>
            // {
            //     new Resume
            //     {
            //         Id = resumeId,
            //         Name = "Ruslan",
            //         AboutMe = "AboutMe",
            //         PictureUrl = "photo.jpg",
            //         ContactInformation = new ContactInformation
            //         {
            //             Id = resumeId,
            //             Email = "r@gmail.com",
            //             PhoneNumber = "1234567890",
            //         },
            //         ContactInformationId = resumeId,
            //         Profession = "Web-Developer",
            //         Skills = "Development"
            //     },
            //     new Resume
            //     {
            //         Id = 2,
            //         Name = "Ruslan",
            //         AboutMe = "AboutMe",
            //         PictureUrl = "photo.jpg",
            //         ContactInformation = new ContactInformation
            //         {
            //             Id = 2,
            //             Email = "r@gmail.com",
            //             PhoneNumber = "1234567890",
            //         },
            //         ContactInformationId = 2,
            //         Profession = "Web-Developer",
            //         Skills = "Development"
            //     }
            // };

            // Pagination pagination = new Pagination
            // {
            //     PageSize = 6,
            //     PageIndex = 1,
            //     Sort = "desc",
            //     Search = ""
            // };

            // var resumesWithSpec = new ResumeWithContactSpecification(pagination);

            // var specifEval = SpecificationEvaluator<Resume>.GetQuery(resumes.AsQueryable(), resumesWithSpec);

            // Assert.That(resumes.OrderByDescending(x => x.Name), Is.EqualTo(specifEval));
        }
    }
}
