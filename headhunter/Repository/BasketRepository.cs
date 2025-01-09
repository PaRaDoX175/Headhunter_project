using headhunter.Entities;
using StackExchange.Redis;
using System.Text.Json;

namespace headhunter.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public async Task<Basket> DeleteBasketItem(string basketId, int itemId)
        {
            var basket = await GetBasketAsync(basketId);
            var newBasket = new Basket { Id = basketId, Items = basket.Items.Where(x => x.Id != itemId).ToList() };
            await _database.StringSetAsync(basketId, JsonSerializer.Serialize(newBasket));
            return await GetBasketAsync(basketId);
        }

        public async Task<Basket> GetBasketAsync(string id)
        {
            var data = await _database.StringGetAsync(id);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Basket>(data);
        }

        public async Task<Basket> UpdateBasketAsync(Basket basket)
        {
            var getBasket = await GetBasketAsync(basket.Id) ?? new Basket { Id = basket.Id };

            foreach (var item in basket.Items)
            {
                getBasket.Items.Add(item);
            }

            var created = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(getBasket));

            if (!created)
            {
                return null;
            }

            return await GetBasketAsync(basket.Id);
        }
    }
}
