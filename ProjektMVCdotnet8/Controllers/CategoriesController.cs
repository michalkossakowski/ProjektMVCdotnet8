using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using ProjektMVCdotnet8.Entities;
using ProjektMVCdotnet8.Models;

namespace ProjektMVCdotnet8.Controllers
{
    public class CategoriesController : Controller
    {

        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;

            if (_context.Categories.IsNullOrEmpty()) //Sprawdza czy tablica jest pusta, jeżeli tak to tworzy elementy do tablicy
            {
                CreateElements();
            }
        }
        public IActionResult Index(string CategoryName)
        {
            ViewBag.CategoryName = CategoryName;
            return View("Index", CategoryName);
        }

        //Rekordy testowe ----Usunąć w finalnej wersji----
        public async void CreateElements()
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
