using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VShopWEB.Models;
using VShopWEB.Services.Contracts;

namespace VShopWEB.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public ProductsController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductsViewModel>>> Index()
    {
        var products = await _productService.GetAllAsync();
        if (products == null)
        {
            return View("Error");
        }

        return View(products);
    }

    [HttpGet]
    public async Task<ActionResult> Create()
    {
        ViewBag.CategoryId = new SelectList(await
            _categoryService.GetAllAsync(), "CategoryId", "Name");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductsViewModel productsViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _productService.CreateProduct(productsViewModel);

            if (result != null)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        else
        {
            ViewBag.CategoryId = new SelectList(await
                _categoryService.GetAllAsync(), "CategoryId", "Name");
        }

        return View(productsViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        ViewBag.CategoryId = new SelectList(await
                _categoryService.GetAllAsync(), "CategoryId", "Name");

        var product = await _productService.GetByIdAsync(id);

        if (product is null) //We're using IS NULL here bc there's a possibility of the item not to exist!
        {
            return View("Error");
        }

        return View(product);
    }

    [HttpPost]
    public async Task<ActionResult> Update(ProductsViewModel productsViewModel)
    {
        if (ModelState.IsValid)
        {
            var product = await _productService.UpdateProductAsync(productsViewModel);

            if (product is not null) return RedirectToAction(nameof(Index));
        }

        return View(productsViewModel);
    }

    [HttpGet]
    public async Task<ActionResult<ProductsViewModel>> Delete(int id)
    {

        var product = await _productService.GetByIdAsync(id);

        if (product is null)
        {
            return View("Error");
        }

        return View(product);
    }

    [HttpPost(), ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _productService.DeleteProductById(id);

        if (!product)
        {
            return View("Error");
        }

        return RedirectToAction("Index");
    }
}
