using Microsoft.AspNetCore.Mvc;

namespace ProjektMVCdotnet8.Controllers
{
    public class CategoriesController : Controller
    {
      
        public IActionResult CategoryIndex()
        {
            return View();
        }
        public IActionResult Elektronika()
        {
            return View("CategoryIndex");
        }
        public IActionResult Informatyka()
        {
            return View("CategoryIndex");
        }
        public IActionResult DYI()
        {
            return View("CategoryIndex");
        }
    }
}
