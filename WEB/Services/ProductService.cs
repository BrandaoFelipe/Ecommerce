using System.Text;
using System.Text.Json;
using VShopWEB.Models;
using VShopWEB.Services.Contracts;

namespace VShopWEB.Services;

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

    public async Task<IEnumerable<ProductsViewModel>> GetAllAsync()
    {
        var client = _client.CreateClient("ProductAPI"); // create a httpclientfactory instance that will create a link to the productsapi

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

    public async Task<ProductsViewModel> GetByIdAsync(int id)
    {
        var client = _client.CreateClient("ProductAPI");

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
    public async Task<ProductsViewModel> CreateProduct(ProductsViewModel productVm)
    {
        var client = _client.CreateClient("ProductAPI");

        StringContent content = new StringContent(JsonSerializer.Serialize(productVm), 
            Encoding.UTF8, "application/json");
        
        using var response = await client.GetAsync(apiEndpoint + content);

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
    public async Task<ProductsViewModel> UpdateProductAsync(ProductsViewModel productVm)
    {
        var client = _client.CreateClient("ProductAPI");

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

    public async Task<bool> DeleteProductById(int id)
    {
        var client = _client.CreateClient("ProductAPI");

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
}
