using Microsoft.AspNetCore.Mvc;
using ProjektMVCdotnet8.Models;

namespace ProjektMVCdotnet8.Controllers
{
    public class CategoriesController : Controller
    {
      
        public IActionResult CategoryIndex()
        {
            return View();
        }
        public IActionResult Index(string CategoryName)
        {
            ViewBag.CategoryName = CategoryName;
            return View("CategoryIndex", CategoryName);
        }
    }
}
