using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using ProjektMVCdotnet8.Entities;
using ProjektMVCdotnet8.Models;
using ProjektMVCdotnet8.Areas.Identity.Data;

namespace ProjektMVCdotnet8.Controllers
{
    public class PostController : Controller
    {

        private ApplicationDbContext _context;

        public PostController( ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string CategoryName)
        {
        

            var posts = _context.Posts.Include(p => p.AuthorUser).Include(p => p.Categories)
                .Where(post => post.Categories.Any(category => category.CategoryName.Equals(CategoryName)))
                .OrderByDescending(post => post.CreatedDate).ToList();
            return View("Index", posts);
        }

      
    }
}
