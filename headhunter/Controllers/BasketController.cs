using headhunter.Entities;
using headhunter.Errors;
using headhunter.Repository;
using Microsoft.AspNetCore.Mvc;

namespace headhunter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepo;

        public BasketController(IBasketRepository basketRepo)
        {
            _basketRepo = basketRepo;
        }

        [HttpGet]
        public async Task<ActionResult<Basket>> GetBasketById([FromQuery] string id)
        {
            var basket = await _basketRepo.GetBasketAsync(id);

            return Ok(basket ?? new Basket(id));
        }

        [HttpPost]
        public async Task<ActionResult<Basket>> UpdateBasket([FromBody] Basket basket)
        {
            var updatedBasket = await _basketRepo.UpdateBasketAsync(basket);

            if (updatedBasket == null)
            {
                return BadRequest(new ApiException(400));
            }

            return Ok(updatedBasket);
        }

        [HttpDelete("item")]
        public async Task<ActionResult<Basket>> DeleteBasketItem([FromQuery] string basketId, [FromQuery] int itemId)
        {
            return await _basketRepo.DeleteBasketItem(basketId, itemId);
        }

        [HttpDelete]
        public async Task<bool> DeleteBasketById(string id)
        {
            return await _basketRepo.DeleteBasketAsync(id);
        }
    }
}
