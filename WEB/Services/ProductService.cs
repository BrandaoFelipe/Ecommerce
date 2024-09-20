using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using WEB.Models;
using WEB.Services.Contracts;

namespace WEB.Services;

public class ProductService : IProductService
{
    private readonly JsonSerializerOptions _options;
    private readonly IHttpClientFactory _client;
    private const string apiEndpoint = "/api/products/";
    private ProductsViewModel _productVM;
    private IEnumerable<ProductsViewModel> _products;

    public ProductService(IHttpClientFactory client)
    {
        _client = client;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<ProductsViewModel>> GetAllAsync(string token)
    {
        var client = _client.CreateClient("ProductAPI"); // create a httpclientfactory instance that will create a link to the productsapi

        PutTokenInHeaderAuthorization(token, client);

        using var response = await client.GetAsync(apiEndpoint); // here the clientfactory gets the endpoints in the productsapi
        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();
            _products = await JsonSerializer
                .DeserializeAsync<IEnumerable<ProductsViewModel>>(apiResponse, _options);
        }
        else
        {
            return null;
        }

        return _products;
    }

    public async Task<ProductsViewModel> GetByIdAsync(int id, string token)
    {
        var client = _client.CreateClient("ProductAPI");

        PutTokenInHeaderAuthorization(token, client);

        using var response = await client.GetAsync(apiEndpoint + id);

        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();
            _productVM = await JsonSerializer
                .DeserializeAsync<ProductsViewModel>(apiResponse, _options);
        }
        else
        {
            return null;
        }

        return _productVM;
    }
    public async Task<ProductsViewModel> CreateProduct(ProductsViewModel productVm, string token)
    {
        var client = _client.CreateClient("ProductAPI");

        PutTokenInHeaderAuthorization(token, client);

        StringContent content = new StringContent(JsonSerializer.Serialize(productVm), 
            Encoding.UTF8, "application/json");
        
        using var response = await client.PostAsync(apiEndpoint, content);

        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();
            _productVM = await JsonSerializer
                .DeserializeAsync<ProductsViewModel>(apiResponse, _options);
        }
        else
        {
            return null;
        }

        return _productVM;
    }
    public async Task<ProductsViewModel> UpdateProductAsync(ProductsViewModel productVm, string token)
    {
        var client = _client.CreateClient("ProductAPI");

        PutTokenInHeaderAuthorization(token, client);

        ProductsViewModel productUpdated = new ProductsViewModel();

        using var response = await client.PutAsJsonAsync(apiEndpoint, productVm);

        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();
            productUpdated = await JsonSerializer
                .DeserializeAsync<ProductsViewModel>(apiResponse, _options);
        }
        else
        {
            return null;
        }

        return productUpdated;
    }

    public async Task<bool> DeleteProductById(int id, string token)
    {
        var client = _client.CreateClient("ProductAPI");

        PutTokenInHeaderAuthorization(token, client);

        using var response = await client.DeleteAsync(apiEndpoint + id);

        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    private static void PutTokenInHeaderAuthorization(string token, HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}
