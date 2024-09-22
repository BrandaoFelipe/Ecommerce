using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using WEB.Models;
using WEB.Services.Contracts;

namespace WEB.Services
{
    public class CartService : ICartService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly JsonSerializerOptions? _options;
        private const string apiEndPoint = "/api/cart";
        private CartViewModel cartVM = new CartViewModel();

        public CartService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<CartViewModel> GetCartByUserIdAsync(string userId, string token)
        {
            var client = _clientFactory.CreateClient("CartAPI");

            PutTokenInHeaderAuthorization(token, client);

            using (var response = await client.GetAsync($"{apiEndPoint}/getCart/{userId}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiReponse = await response.Content.ReadAsStreamAsync();
                    cartVM = await JsonSerializer.DeserializeAsync<CartViewModel>(apiReponse, _options);
                }
                else
                {
                    return null;    
                }
            }

            return cartVM;
        }

        public async Task<CartViewModel> AddItemToCartAsync(CartViewModel cart, string token)
        {
            var client = _clientFactory.CreateClient("CartAPI");

            PutTokenInHeaderAuthorization(token, client);

            StringContent content = new StringContent(JsonSerializer.Serialize(cart), Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync($"{apiEndPoint}/addCart/", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiReponse = await response.Content.ReadAsStreamAsync();
                    cartVM = await JsonSerializer.DeserializeAsync<CartViewModel>(apiReponse, _options);
                }
                else
                {
                    return null;
                }
            }

            return cartVM;
        }

        public async Task<CartViewModel> UpdateCartAsync(CartViewModel cart, string token)
        {
            var client = _clientFactory.CreateClient("CartAPI");

            PutTokenInHeaderAuthorization(token, client);

            CartViewModel cartUpdated = new CartViewModel();

            using (var response = await client.PutAsJsonAsync($"{apiEndPoint}/updateCart/", cartVM))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiReponse = await response.Content.ReadAsStreamAsync();
                    cartUpdated = await JsonSerializer.DeserializeAsync<CartViewModel>(apiReponse, _options);
                }
                else
                {
                    return null;
                }
            }

            return cartUpdated;

        }

        public async Task<bool> RemoveItemFromCartAsync(int cartId, string token)
        {
            var client = _clientFactory.CreateClient("CartAPI");

            PutTokenInHeaderAuthorization(token, client);

            using (var response = await client.DeleteAsync($"{apiEndPoint}/deleteCart/" + cartVM))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }

            return false;
        }
        
        public Task<bool> ClearCartAsync(int userId, string token)
        {
            throw new NotImplementedException();
        }

        public Task<CartViewModel> CheckOutAsync(CartHeaderViewModel cartHeader, string token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ApplyCouponAsync(CartViewModel cart, string couponCode, string token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveCouponAsync(int userId, string token)
        {
            throw new NotImplementedException();
        }

        
        
        private static void PutTokenInHeaderAuthorization(string token, HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

    }
}
