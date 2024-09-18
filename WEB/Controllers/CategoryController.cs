using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB.Models;
using WEB.Services.Contracts;



namespace WEB.Controllers;
[Authorize]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryViewModel>>> Index()
    {
        var category = await _categoryService.GetAllAsync(await GetAccessToken());
        if(category == null)
        {
            return View("Error");
        }
        return View(category);
    }

    private async Task<string> GetAccessToken()
    {
        return await HttpContext.GetTokenAsync("access_token");
    }
}
