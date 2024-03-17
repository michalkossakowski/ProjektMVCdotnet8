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
                    CategoryEntity category = new CategoryEntity("Elektronika");
                    _context.Add(category);

                    category = new CategoryEntity("Programowanie");
                    _context.Add(category);

                    category = new CategoryEntity("Komputery");
                    _context.Add(category);

                    category = new CategoryEntity("Sieci");
                    _context.Add(category);

                    category = new CategoryEntity("Spawanie");
                    _context.Add(category);

                    category = new CategoryEntity("Elektryka");
                    _context.Add(category);
                    await _context.SaveChangesAsync();
                }
            }
            else if (_context.Posts.IsNullOrEmpty())
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == "WERYKTEST@PL");
                PostEntity posts = new PostEntity();
                var categories = _context.Categories.Where(x => x.CategoryName.Equals("Programowanie")).ToList();
                posts.AuthorUser = user;
                posts.Title = "Tytul_1";
                posts.PostContent = "lorem ipsum w gipsum";
                posts.CreatedDate = DateTime.Now;
                posts.Categories = categories;
                _context.Add(posts);
                posts.AttachmentSource = ("29a5ddb2-5544-4fdd-993c-5139fd04d0e4_WolfAttack5.png");
                await _context.SaveChangesAsync();
            }
        }

    }
}
