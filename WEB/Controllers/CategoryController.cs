using Microsoft.AspNetCore.Mvc;
using VShopWEB.Models;
using VShopWEB.Services.Contracts;

namespace VShopWEB.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<ActionResult<IEnumerable<CategoryViewModel>>> Index()
    {
        var category = await _categoryService.GetAllAsync();
        if(category == null)
        {
            return View("Error");
        }
        return View(category);
    }
}
