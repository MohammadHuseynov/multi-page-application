using Microsoft.AspNetCore.Mvc;
using MultiPageApplication.ApplicationServices.Dtos;
using MultiPageApplication.ApplicationServices.Services.Contracts;
using ResponseFramework;

namespace MultiPageApplication.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApplicationService _productApplicationService;

        // Inject the IProductApplicationService
        public ProductController(IProductApplicationService productApplicationService)
        {
            _productApplicationService = productApplicationService;
        }

        // GET: Products/Index
        public async Task<IActionResult> Index()
        {
            var response = await _productApplicationService.GetAllProductAsync();
            if (!response.IsSuccessful)
            {
                ViewBag.ErrorMessage = response.ErrorMessage ?? "An error occurred.";
            }
            return View(response.Result ?? new List<GetProductDto>());
        }

        // GET: /Product/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var response = await _productApplicationService.GetByIdProductAsync(id);
            if (!response.IsSuccessful || response.Result == null)
            {
                return NotFound();
            }
            return View(response.Result);
        }

        // GET: /Product/Create
        public IActionResult Create()
        {
            // Return a view to create a new product
            // Location: /Views/Product/Create.cshtml
            return View();
        }

        // POST: /Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostProductDto postProductDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _productApplicationService.PostProductDtoAsync(postProductDto);
                if (!response.IsSuccessful)
                {
                    ModelState.AddModelError(string.Empty, response.ErrorMessage ?? "Failed to create product.");
                    return View(postProductDto);
                }
                return RedirectToAction(nameof(Index));
            }
            // If model is not valid, return the view with the entered data
            return View(postProductDto);
        }

        // GET: /Product/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var response = await _productApplicationService.GetByIdProductAsync(id);
            if (!response.IsSuccessful || response.Result == null)
            {
                return NotFound();
            }
            // Map GetProductDto to PutProductDto for the edit view
            var model = new PutProductDto
            {
                Id = response.Result.Id,
                Title = response.Result.Title,
                UnitPrice = response.Result.UnitPrice,
                Quantity = response.Result.Quantity
            };
            return View(model);
        }

        // POST: /Product/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PutProductDto putProductDto)
        {
            if (id != putProductDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var response = await _productApplicationService.PutProductDtoAsync(putProductDto);
                if (!response.IsSuccessful)
                {
                    ModelState.AddModelError(string.Empty, response.ErrorMessage ?? "Failed to update product.");
                    return View(putProductDto);
                }
                return RedirectToAction(nameof(Index));
            }
            // If model is not valid, return the view with the entered data
            return View(putProductDto);
        }

        // GET: /Product/Delete/{id}
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _productApplicationService.GetByIdProductAsync(id);
            if (!response.IsSuccessful || response.Result == null)
            {
                return NotFound();
            }
            // Return a view to confirm deletion
            // Location: /Views/Product/Delete.cshtml
            return View(response.Result);
        }

        // POST: /Product/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var response = await _productApplicationService.DeleteAsync(id);
            if (!response.IsSuccessful)
            {
                TempData["Error"] = response.ErrorMessage ?? "Failed to delete product.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
