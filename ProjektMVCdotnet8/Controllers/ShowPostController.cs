﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Entities;
using ProjektMVCdotnet8.Models;

namespace ProjektMVCdotnet8.Controllers
{
    public class ShowPostController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ShowPostController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string CategoryName)
        {
            var posts = _context.Posts.Where(post => post.Categories.Any(category => category.CategoryName.Equals(CategoryName))).ToList();
            return View("Index", posts);
        }
    }
}
