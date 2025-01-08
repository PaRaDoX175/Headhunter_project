using headhunter.Controllers;
using headhunter.Entities;
using headhunter.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace headhunterTests.BasketControllerTests
{
    [TestFixture]
    public class GetBasketByIdTests
    {
        private readonly BasketController _sut;
        private readonly Mock<IBasketRepository> basketRepo = new Mock<IBasketRepository>();
        public GetBasketByIdTests()
        {
            _sut = new BasketController(basketRepo.Object);
        }

        static string basketId = "basket1";

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
        public async Task GetBasketById_ReturnNewBasket()
        {
            basketRepo.Setup(x => x.GetBasketAsync(basketId)).ReturnsAsync(() => null);

            var res = await _sut.GetBasketById(basketId);

            Assert.IsNotNull(res);
            var result = (OkObjectResult)res.Result;
            var value = (Basket)result.Value;
            Assert.AreEqual(basketId, value.Id);
            Assert.IsTrue(value.Items.Count == 0);
        }

        [Test]
        public async Task GetBasketById_ReturnExistBasket()
        {
            basketRepo.Setup(x => x.GetBasketAsync(basketId)).ReturnsAsync(basket);

            var res = await _sut.GetBasketById(basketId);

            Assert.IsNotNull(res);
            var result = (OkObjectResult)res.Result;
            var value = (Basket)result.Value;
            Assert.IsTrue(value.Items.Count > 0);
        }
    }
}
