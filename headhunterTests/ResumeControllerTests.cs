using AutoMapper;
using headhunter.Controllers;
using headhunter.Dtos;
using headhunter.Entities;
using headhunter.Repository;
using headhunter.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace headhunterTests
{
    [TestFixture]
    public class ResumeControllerTests
    {
        private readonly ResumeController _sut;
        private readonly Mock<IGenericRepository<Resume>> _repo = new Mock<IGenericRepository<Resume>>();
        private readonly Mock<IGenericRepository<ContactInformation>> _repoCont = new Mock<IGenericRepository<ContactInformation>>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();

        public ResumeControllerTests()
        {
            _sut = new ResumeController(_mapper.Object, _repo.Object, _repoCont.Object);
        }

        [Test]
        public async Task GetById_ShouldReturnResume_WhenCustomerExist()
        {
            var resumeId = 1;

            Resume resume = new Resume
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
            };

            var resumeDto = new ResumeDto
            {
                Name = "Ruslan",
                AboutMe = "AboutMe",
                PictureUrl = "photo.jpg",
                Email = "r@gmail.com",
                PhoneNumber = "1234567890",
                Profession = "Web-Developer",
                Skills = "Development"
            };

            _repo.Setup(x => x.GetList()).ReturnsAsync(new List<Resume> { resume });

            _repo.Setup(x => x.GetEntityWithSpec(It.IsAny<ResumeWithContactSpecification>(), It.IsAny<int>())).ReturnsAsync(resume);
            _mapper.Setup(x => x.Map<Resume, ResumeDto>(resume)).Returns(resumeDto);

            var actionResult = await _sut.GetById(resumeId);

            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<ActionResult<ResumeDto>>(actionResult);
            var okResult = (OkObjectResult)actionResult.Result;
            Assert.IsInstanceOf<ResumeDto>((ResumeDto)okResult.Value);

            //var res = (ResumeDto)okResult.Value;
            //Assert.That(resumeId, Is.EqualTo(res.Id));
        }

        [Test]
        public async Task GetById_ShouldReturnNull_WhenRepoIsEmpty()
        {
            _repo.Setup(x => x.GetList()).ReturnsAsync(new List<Resume>
            {
                new Resume
                {
                    Id = 1,
                    Name = "Ruslan",
                    AboutMe = "AboutMe",
                    PictureUrl = "photo.jpg",
                    ContactInformation = null,
                    ContactInformationId = 1,
                    Profession = "Web-Developer",
                    Skills = "Development"
                }
            });
            _repo.Setup(x => x.GetEntityWithSpec(It.IsAny<ResumeWithContactSpecification>(), It.IsAny<int>())).ReturnsAsync(() => null);

            var actionResult = await _sut.GetById(1);
            Assert.IsNull(actionResult.Value);
        }

        [Test]
        public async Task GetById_ShouldReturnNotFound_WhenThereIsNoId()
        {
            int resumeId = 1;
            _repo.Setup(x => x.GetList()).ReturnsAsync(new List<Resume>());

            var actionResult = await _sut.GetById(resumeId);
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult.Result);
        }

        [Test]
        public async Task GetAll_ShouldReturnResumes_WhenRepoIsNotEmpty()
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
                }
            };

            var resumesDto = new List<ResumeDto>
            {
                new ResumeDto
                {
                    Name = "Ruslan",
                    AboutMe = "AboutMe",
                    PictureUrl = "photo.jpg",
                    Email = "r@gmail.com",
                    PhoneNumber = "1234567890",
                    Profession = "Web-Developer",
                    Skills = "Development"
                }
            };

            _repo.Setup(x => x.GetList()).ReturnsAsync(resumes);
            _repo.Setup(x => x.GetListAsyncSpec(It.IsAny<ResumeWithContactSpecification>())).ReturnsAsync(resumes);
            _mapper.Setup(x => x.Map<List<Resume>, List<ResumeDto>>(resumes)).Returns(resumesDto);


            var res = await _sut.GetAll("asc");

            Assert.IsInstanceOf<OkObjectResult>(res.Result);

            var okResult = (OkObjectResult)res.Result;
            Assert.IsInstanceOf<List<ResumeDto>>(okResult.Value);
        }

        [Test]
        public async Task GetAll_ShouldReturnBadRequest_WhenRepoIsEmpty()
        {
            _repo.Setup(x => x.GetList()).ReturnsAsync(new List<Resume>());

            var res = await _sut.GetAll("asc");
            Assert.IsInstanceOf<BadRequestObjectResult>(res.Result);
        }

        [Test]
        public async Task GetAll_ShouldReturnNotFound_WhenYouCantGetList()
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
                }
            };

            _repo.Setup(x => x.GetList()).ReturnsAsync(resumes);
            _repo.Setup(x => x.GetListAsyncSpec(It.IsAny<ResumeWithContactSpecification>())).ReturnsAsync(() => null);

            var res = await _sut.GetAll("asc");

            Assert.IsInstanceOf<NotFoundObjectResult>(res.Result);
        }

        [Test]
        public async Task PostResume_ShouldReturnResume_IfWasAddedInDb()
        {
            int resumeId = 1;
            var resume = new Resume
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
            };

            var resumeDto = new ResumeDto
            {
                Name = "Ruslan",
                AboutMe = "AboutMe",
                PictureUrl = "photo.jpg",
                Email = "r@gmail.com",
                PhoneNumber = "1234567890",
                Profession = "Web-Developer",
                Skills = "Development"
            };

            _repo.Setup(x => x.Add(resume));
            _mapper.Setup(x => x.Map<Resume, ResumeDto>(resume)).Returns(resumeDto);

            var res = await _sut.PostResume(resume);

            _repo.Verify(x => x.Add(resume), Times.Once);
            Assert.IsInstanceOf<ResumeDto>(res.Value);
            Assert.That(resume.Name, Is.EqualTo(resumeDto.Name));
        }

        //[Test]
        //public async Task PostResume_ShouldReturnBadRequest_IfFieldIsMissing()
        //{
        //    int resumeId = 1;
        //    var resume = new Resume
        //    {
        //        Id = resumeId,
        //        AboutMe = "AboutMe",
        //        PictureUrl = "photo.jpg",
        //        ContactInformation = new ContactInformation
        //        {
        //            Id = resumeId,
        //            Email = "r@gmail.com",
        //            PhoneNumber = "1234567890",
        //        },
        //        ContactInformationId = resumeId,
        //        Profession = "Web-Developer",
        //        Skills = "Development"
        //    };

        //    //_repo.Setup(x => x.Add(resume)).Returns(() => Task.FromResult(new HttpResponseMessage(HttpStatusCode.BadRequest)));

        //    //var res = await _sut.PostResume(resume);

        //    //Assert.IsInstanceOf<BadRequestObjectResult>(res.Result);
        //    var res = await _sut.PostResume(resume);
        //    Assert.IsNotNull(res.Result);
        //    _repo.Verify(x => x.Add(resume), Times.Once);
        //}

        //[Test]
        //public async Task GetById_TEST()
        //{
        //    var resumeId = 1;
        //    _repo.Setup(x => x.GetById(resumeId)).ReturnsAsync(new Resume
        //    {
        //        Id = resumeId,
        //        Name = "Ruslan",
        //        AboutMe = "AboutMe",
        //        PictureUrl = "photo.jpg",
        //        ContactInformation = new ContactInformation
        //        {
        //            Id = resumeId,
        //            Email = "r@gmail.com",
        //            PhoneNumber = "1234567890",
        //        },
        //        ContactInformationId = resumeId,
        //        Profession = "Web-Developer",
        //        Skills = "Development"
        //    });

        //    var res = await _sut.GetByIdTest(resumeId);
        //    Assert.IsNotNull(res);
        //    Assert.That(resumeId, Is.EqualTo(res.Id));
        //}




        //[Test]
        //public async Task GetById_ThrowAnError_IfIdIsGreater()
        //{
        //    var resumeId = 4;
        //    var spec = new ResumeWithContactSpecification(resumeId);
        //    _repo.Setup(x => x.GetList()).Returns(new List<Resume>
        //    {
        //        new Resume { Id = 1 },
        //        new Resume { Id = 2 }
        //    });
        //    _repo.Setup(x => x.GetEntityWithSpec(spec, resumeId)).ReturnsAsync(new Resume());
        //    var actionResult = await _sut.GetById(resumeId);
        //    var res = actionResult.Value;
        //    if (res != null)
        //    {
        //        Assert.That(resumeId, Is.EqualTo(res.Id));
        //    }
        //}


    }
}
