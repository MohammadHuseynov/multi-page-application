using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiPageApplication.ApplicationServices.Services.Contracts;
using MultiPageApplication.Models.DomainModels.ProductAggregates;

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
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productApplicationService.PostProductDtoAsync(product);
                return RedirectToAction(nameof(Index));
            }
            // If model is not valid, return the view with the entered data
            return View(product);
        }

        // GET: /Product/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public async Task<IActionResult> Edit(Guid id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _productApplicationService.PutProductDtoAsync(product);
                return RedirectToAction(nameof(Index));
            }
            // If model is not valid, return the view with the entered data
            return View(product);
        }

        // GET: /Product/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            var product = await _productApplicationService.GetByIdProductAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await _productApplicationService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Product/Details/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(Guid id)
        {
            var product = await _productApplicationService.GetByIdProductAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            // Return a view to show product details
            // Location: /Views/Product/Details.cshtml
            return View(product);
        }

        // POST: /Product/Details/{id}
        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsConfirmed(Guid id)
        {
            var product = await _productApplicationService.GetByIdProductAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            // Return a view to show product details
            // Location: /Views/Product/Details.cshtml
            return View(product);
        }

        // Optional: You can add more actions as needed for your application
        // Example: Search, Filter, etc.
        // Ensure you have the necessary views created in /Views/Product/
        // Example: /Views/Product/Search.cshtml, /Views/Product/Filter.cshtml, etc.
        // Ensure you have the necessary views created in /Views/Product/
        // Example: /Views/Product/Search.cshtml, /Views/Product/Filter.cshtml, etc.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Search(string searchTerm)
        //{
        //    var products = await _productApplicationService.GetAllProductAsync();
        //    var filteredProducts = products.Where(p => p.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
        //    // Return a view to show the search results
        //    // Location: /Views/Product/SearchResults.cshtml
        //    return View("SearchResults", filteredProducts);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Filter(decimal minPrice, decimal maxPrice)
        //{
        //    var products = await _productApplicationService.GetAllProductAsync();
        //    var filteredProducts = products.Where(p => p.UnitPrice >= minPrice && p.UnitPrice <= maxPrice).ToList();
        //    // Return a view to show the filtered results
        //    // Location: /Views/Product/FilterResults.cshtml
        //    return View("FilterResults", filteredProducts);
        //}

        //// Ensure you have the necessary views created in /Views/Product/
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Sort(string sortOrder)
        //{
        //    var products = await _productApplicationService.GetAllProductAsync();
        //    switch (sortOrder)
        //    {
        //        case "title_desc":
        //            products = products.OrderByDescending(p => p.Title).ToList();
        //            break;
        //        case "price_asc":
        //            products = products.OrderBy(p => p.UnitPrice).ToList();
        //            break;
        //        case "price_desc":
        //            products = products.OrderByDescending(p => p.UnitPrice).ToList();
        //            break;
        //        default:
        //            products = products.OrderBy(p => p.Title).ToList();
        //            break;
        //    }
        //    // Return a view to show the sorted results
        //    // Location: /Views/Product/SortResults.cshtml
        //    return View("SortResults", products);
        //}

        //// Ensure you have the necessary views created in /Views/Product/
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Paginate(int pageNumber, int pageSize)
        //{
        //    var products = await _productApplicationService.GetAllProductAsync();
        //    var paginatedProducts = products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        //    // Return a view to show the paginated results
        //    // Location: /Views/Product/PaginatedResults.cshtml
        //    return View("PaginatedResults", paginatedProducts);
        //}

        //// Ensure you have the necessary views created in /Views/Product/
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ExportToCsv()
        //{
        //    var products = await _productApplicationService.GetAllProductAsync();
        //    var csvContent = "Id,Title,UnitPrice,Quantity\n" + 
        //                     string.Join("\n", products.Select(p => $"{p.Id},{p.Title},{p.UnitPrice},{p.Quantity}"));
        //    var bytes = System.Text.Encoding.UTF8.GetBytes(csvContent);
        //    return File(bytes, "text/csv", "products.csv");
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ImportFromCsv(IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //    {
        //        ModelState.AddModelError("", "Please select a valid CSV file.");
        //        return View("Import"); // Ensure you have an Import view
        //    }
        //    using (var reader = new StreamReader(file.OpenReadStream()))
        //    {
        //        var line = await reader.ReadLineAsync();
        //        while (line != null)
        //        {
        //            var values = line.Split(',');
        //            if (values.Length == 4 && Guid.TryParse(values[0], out var id) &&
        //                decimal.TryParse(values[2], out var unitPrice) &&
        //                int.TryParse(values[3], out var quantity))
        //            {
        //                var product = new Product
        //                {
        //                    Id = id,
        //                    Title = values[1],
        //                    UnitPrice = unitPrice,
        //                    Quantity = quantity
        //                };
        //                await _productApplicationService.PostProductDtoAsync(product);
        //            }
        //            line = await reader.ReadLineAsync();
        //        }
        //    }
        //    return RedirectToAction(nameof(Index));
        //}

        //// Ensure you have the necessary views created in /Views/Product/
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ImportFromExcel(IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //    {
        //        ModelState.AddModelError("", "Please select a valid Excel file.");
        //        return View("ImportExcel"); // Ensure you have an ImportExcel view
        //    }
        //    using (var stream = new MemoryStream())
        //    {
        //        await file.CopyToAsync(stream);
        //        stream.Position = 0;
        //        using (var reader = ExcelReaderFactory.CreateReader(stream))
        //        {
        //            while (reader.Read())
        //            {
        //                if (reader.FieldCount >= 4 && Guid.TryParse(reader.GetValue(0)?.ToString(), out var id) &&
        //                    decimal.TryParse(reader.GetValue(2)?.ToString(), out var unitPrice) &&
        //                    int.TryParse(reader.GetValue(3)?.ToString(), out var quantity))
        //                {
        //                    var product = new Product
        //                    {
        //                        Id = id,
        //                        Title = reader.GetValue(1)?.ToString(),
        //                        UnitPrice = unitPrice,
        //                        Quantity = quantity
        //                    };
        //                    await _productApplicationService.PostProductDtoAsync(product);
        //                }
        //            }
        //        }
        //    }
        //    return RedirectToAction(nameof(Index));
        //
    }


}
