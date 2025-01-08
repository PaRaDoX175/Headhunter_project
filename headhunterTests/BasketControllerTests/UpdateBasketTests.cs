using headhunter.Controllers;
using headhunter.Entities;
using headhunter.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace headhunterTests.BasketControllerTests
{
    [TestFixture]
    public class UpdateBasketTests
    {
        private readonly BasketController _sut;
        private readonly Mock<IBasketRepository> basketRepo = new Mock<IBasketRepository>();
        public UpdateBasketTests()
        {
            _sut = new BasketController(basketRepo.Object);
        }

        static string basketId = "basketId";

        Basket basket = new Basket
        {
            Id = basketId,
            Items = new List<BasketItem>
            {
                new BasketItem
                {
                    Id = 1,
                    Position = "Position",
                    Salary = 3000,
                    CompanyName = "CompanyName",
                    Location = "Almaty"
                }
            }
        };

        [Test]
        public async Task UpdateBasket_ReturnBasket()
        {
            basketRepo.Setup(x => x.UpdateBasketAsync(basket)).ReturnsAsync(basket);

            var res = await _sut.UpdateBasket(basket);

            Assert.IsNotNull(res);
            var result = (OkObjectResult)res.Result;
            var value = (Basket)result.Value;
            Assert.AreEqual(basket.Items.Count, value.Items.Count);
        }

        [Test]
        public async Task UpdateBasket_ReturnBadRequest()
        {
            basketRepo.Setup(x => x.UpdateBasketAsync(It.IsAny<Basket>())).ReturnsAsync(() => null);

            var res = await _sut.UpdateBasket(It.IsAny<Basket>());

            Assert.IsNotNull(res);
            Assert.IsInstanceOf<BadRequestObjectResult>(res.Result);
        }
    }
}
