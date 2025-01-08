using headhunter.Controllers;
using headhunter.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace headhunterTests.BasketControllerTests
{
    [TestFixture]
    public class DeleteBasketTests
    {
        private readonly BasketController _sut;
        private readonly Mock<IBasketRepository> basketRepo = new Mock<IBasketRepository>();
        public DeleteBasketTests()
        {
            _sut = new BasketController(basketRepo.Object);
        }

        [Test]
        public async Task DeleteBasketById_ReturnTrue()
        {
            basketRepo.Setup(x => x.DeleteBasketAsync(It.IsAny<string>())).ReturnsAsync(true);

            var res = await _sut.DeleteBasketById(It.IsAny<string>());

            Assert.True(res);
        }

        [Test]
        public async Task DeleteBasketById_ReturnFalse()
        {
            basketRepo.Setup(x => x.DeleteBasketAsync(It.IsAny<string>())).ReturnsAsync(false);

            var res = await _sut.DeleteBasketById(It.IsAny<string>());

            Assert.False(res);
        }
    }
}
