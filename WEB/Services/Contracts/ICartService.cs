using WEB.Models;

namespace WEB.Services.Contracts
{
    public interface ICartService
    {
        Task<CartViewModel> GetCartByUserIdAsync(string userId, string token);
        Task<CartViewModel> AddItemToCartAsync(CartViewModel cart, string token);
        Task<CartViewModel> UpdateCartAsync(CartViewModel cart, string token);
        Task<bool> RemoveItemFromCartAsync(int cartId, string token);

        Task<bool> ApplyCouponAsync(CartViewModel cart, string couponCode, string token);
        Task<bool> RemoveCouponAsync(int userId, string token);
        Task<bool> ClearCartAsync(int userId, string token);

        Task<CartViewModel> CheckOutAsync(CartHeaderViewModel cartHeader, string token);

    }
}
