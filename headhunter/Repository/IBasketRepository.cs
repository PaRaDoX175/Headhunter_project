using headhunter.Entities;

namespace headhunter.Repository
{
    public interface IBasketRepository
    {
        Task<Basket> GetBasketAsync(string id);
        Task<Basket> UpdateBasketAsync(Basket basket);
        Task<Basket> DeleteBasketItem(string basketId, int itemId);
        Task<bool> DeleteBasketAsync(string id);
    }
}
