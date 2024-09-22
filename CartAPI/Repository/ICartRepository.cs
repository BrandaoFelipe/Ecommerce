using CartAPI.DTOs;
using System.Globalization;

namespace CartAPI.Repository
{
    public interface ICartRepository
    {
        Task<CartDTO> GetCartByUserId(string userId);
        Task<CartDTO> UpdateCartAsync(CartDTO cartDTO);
        Task<bool> DeleteItemCartAsync(int cartItemId);
        Task<bool> ApplyCouponsAsync(string userId, string couponId);
        Task<bool> DeleteCouponsAsync(string userId);
        Task<bool> CleanCartAsync(string userId);


    }
}
