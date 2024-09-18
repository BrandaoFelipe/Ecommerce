using System.Text;
using System.Text.Json;
using WEB.Models;
using WEB.Services.Contracts;
using System.Net.Http.Headers;

namespace WEB.Services;

public class CategoryService : ICategoryService
{
    private readonly JsonSerializerOptions _options;
    private readonly IHttpClientFactory _client;
    private const string apiEndpoint = "/api/category/";
    private CategoryViewModel _categoryVM;
    private IEnumerable<CategoryViewModel> _category;

    public CategoryService(IHttpClientFactory client)
    {
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _client = client;
    }

    public async Task<IEnumerable<CategoryViewModel>> GetAllAsync(string token)
    {
        var client = _client.CreateClient("ProductAPI");

        PutTokenInHeaderAuthorization(token, client);

        using var response = await client.GetAsync(apiEndpoint);

        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();

            _category = await JsonSerializer
                .DeserializeAsync<IEnumerable<CategoryViewModel>>(apiResponse, _options);
        }
        else
        {
            return null;
        }
        return _category;
    }

   

    public async Task<CategoryViewModel> GetByIdAsync(int id, string token)
    {
        var client = _client.CreateClient("ProductAPI");

        PutTokenInHeaderAuthorization(token, client);

        using var response = await client.GetAsync(apiEndpoint + id);
        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();
            _categoryVM = await JsonSerializer
                .DeserializeAsync<CategoryViewModel>(apiResponse, _options);
        }
        else
        {
            return null;
        }
        return _categoryVM;
    }

    public async Task<CategoryViewModel> CreateCategory(CategoryViewModel categoryVM, string token)
    {
        var client = _client.CreateClient("ProductAPI");

        PutTokenInHeaderAuthorization(token, client);

        StringContent content = new StringContent(JsonSerializer.Serialize(categoryVM),
            Encoding.UTF8, "application/json");

        using var response = await client.GetAsync(apiEndpoint + content);

        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();
            categoryVM = await JsonSerializer
                .DeserializeAsync<CategoryViewModel>(apiResponse, _options);
        }
        else
        {
            return null;
        }
        return categoryVM;
    }

    public async Task<CategoryViewModel> UpdateCategoryAsync(CategoryViewModel categoryVM, string token)
    {
        var client = _client.CreateClient("ProductAPI");

        PutTokenInHeaderAuthorization(token, client);

        CategoryViewModel categoryUpdated = new CategoryViewModel();

        using var response = await client.PutAsJsonAsync(apiEndpoint, categoryVM);

        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();
            categoryUpdated = await JsonSerializer
                .DeserializeAsync<CategoryViewModel>(apiResponse, _options);
        }
        else
        {
            return null;
        }
        return categoryUpdated;
    }

    public async Task<bool> DeleteCategoryById(int id, string token)
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
