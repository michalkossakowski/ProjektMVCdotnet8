using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Entities;
using ProjektMVCdotnet8.Models;
using System.Diagnostics;

namespace ProjektMVCdotnet8.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger , ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
            CreateElements();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult AddPost()
        {
            List<CategoryEntity> nameCategory = new List<CategoryEntity>();
            foreach(var el in _context.Categories)
            {
                nameCategory.Add(el);
            }
            
            ViewBag.nameCategory = nameCategory;
            return View();
        }
        public IActionResult PasswordRecovery()
        {
            return View();
        }
        public IActionResult ThxForContact(ContactModel model)
        {
            return View("ThxForContact", model);
        }
/*        public IActionResult Chat()
        {
            return View();
        }*/

        public async Task<IActionResult> Chat()
        {
            return View(await _context.Messages.ToListAsync());
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async void CreateElements()
        {
            if (_context.Categories.IsNullOrEmpty()) //Sprawdza czy tablica jest pusta, je¿eli tak to tworzy elementy do tablicy
            {
                if (ModelState.IsValid)
                {
                    CategoryEntity category = new CategoryEntity();
                    category.CategoryName = "Elektronika";
                    _context.Add(category);

                    category = new CategoryEntity();
                    category.CategoryName = "Programowanie";
                    _context.Add(category);

                    category = new CategoryEntity();
                    category.CategoryName = "Komputery";
                    _context.Add(category);

                    category = new CategoryEntity();
                    category.CategoryName = "Sieci";
                    _context.Add(category);

                    category = new CategoryEntity();
                    category.CategoryName = "Spawanie";
                    _context.Add(category);

                    category = new CategoryEntity();
                    category.CategoryName = "Elektryka";
                    _context.Add(category);
                    await _context.SaveChangesAsync();
                }
            }
        }

    }
}
