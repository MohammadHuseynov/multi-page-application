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

        #region [- Index() -]
        // GET: Products/Index
        public async Task<IActionResult> Index()
        {
            var response = await _productApplicationService.GetAllProductAsync();
            if (!response.IsSuccessful)
                ViewBag.ErrorMessage = response.ErrorMessage ?? "An error occurred.";
            
            return View(response.Result ?? new GetAllProductDto());
        }
        #endregion


        #region [- Details() -]
        // GET: /Product/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();
            
            var response = await _productApplicationService.GetByIdProductAsync(new GetByIdProductDto { Id = id });
            if (!response.IsSuccessful || response.Result == null)
                return NotFound();
            
            return View(response.Result);
        }
        #endregion


        #region [- Create(GET) -]
        // GET: /Product/Create
        public IActionResult Create()
        {
            // Return a view to create a new product
            // Location: /Views/Product/Create.cshtml
            return View();
        }
        #endregion

        #region [- Create(POST) -]
        // POST: /Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostProductDto postProductDto)
        {
            if (!ModelState.IsValid)
                return View(postProductDto);
            

            var response = await _productApplicationService.Post(postProductDto);
            if (!response.IsSuccessful)
            {
                ModelState.AddModelError(string.Empty, response.ErrorMessage ?? "Failed to create product.");
                return View(postProductDto);
            }

            TempData["SuccessMessage"] = "Product created successfully!";
            return RedirectToAction(nameof(Index));
        }
        #endregion


        #region [- Edit(GET) -]
        // GET: /Product/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();
            
            // Create a GetByIdProductDto instance with the provided id
            var getByIdProductDto = new GetByIdProductDto { Id = id };
            var response = await _productApplicationService.GetByIdProductAsync(getByIdProductDto);

            if (!response.IsSuccessful || response.Result == null)
                return NotFound();
            
            // Map GetByIdProductDto to PutProductDto for the edit view
            var model = new PutProductDto
            {
                Id = response.Result.Id,
                Title = response.Result.Title,
                UnitPrice = response.Result.UnitPrice,
            };
            return View(model);
        }
        #endregion

        #region [- Edit(POST) -]
        // POST: /Product/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PutProductDto putProductDto)
        {
            if (!ModelState.IsValid)
                return View(putProductDto);

            var response = await _productApplicationService.Put(putProductDto);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError(string.Empty, response.ErrorMessage);
                return View(putProductDto);
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion


        #region [- Delete(GET) -]
        // GET: /Product/Delete/{id}
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            // Create a GetByIdProductDto instance with the provided id
            var getByIdProductDto = new GetByIdProductDto { Id = id };
            var response = await _productApplicationService.GetByIdProductAsync(getByIdProductDto);
            if (!response.IsSuccessful || response.Result == null)
            {
                return NotFound();
            }
            // Return a view to confirm deletion
            // Location: /Views/Product/Delete.cshtml
            return View(response.Result);
        }
        #endregion

        #region [- Delete(POST) -]
        // POST: /Product/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var response = await _productApplicationService.Delete(new DeleteProductDto { Id = id });
            if (!response.IsSuccessful)
            {
                TempData["Error"] = response.ErrorMessage ?? "Failed to delete product.";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

    }
}
