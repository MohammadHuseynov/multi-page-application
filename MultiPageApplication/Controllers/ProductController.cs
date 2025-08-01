using Microsoft.AspNetCore.Mvc;

namespace MultiPageApplication.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
