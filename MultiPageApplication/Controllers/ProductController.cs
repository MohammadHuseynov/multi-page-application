using Microsoft.AspNetCore.Mvc;
using MultiPageApplication.ApplicationServices.Dtos;
using MultiPageApplication.ApplicationServices.Services.Contracts;


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
            var products = await _productApplicationService.GetAllProductAsync();
            // You'll need to create a View for this at /Views/Product/Index.cshtml
            return View(products);
        }

        // GET: /Product/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var product = await _productApplicationService.GetByIdProductAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
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
                await _productApplicationService.PostProductDtoAsync(postProductDto);
                return RedirectToAction(nameof(Index));
            }
            // If model is not valid, return the view with the entered data
            return View(postProductDto);
        }

        // GET: /Product/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _productApplicationService.GetByIdProductAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            // Return a view to edit the product
            // Location: /Views/Product/Edit.cshtml
            return View(product);
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
                await _productApplicationService.PutProductDtoAsync(putProductDto);
                return RedirectToAction(nameof(Index));
            }
            // If model is not valid, return the view with the entered data
            return View(putProductDto);
        }

        // GET: /Product/Delete/{id}
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _productApplicationService.GetByIdProductAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            // Return a view to confirm deletion
            // Location: /Views/Product/Delete.cshtml
            return View(product);
        }

        // POST: /Product/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _productApplicationService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
