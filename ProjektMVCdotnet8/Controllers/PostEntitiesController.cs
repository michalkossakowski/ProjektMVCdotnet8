using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektMVCdotnet8.Entities;
using ProjektMVCdotnet8.Models;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using ProjektMVCdotnet8.Areas.Identity.Data;
using System.Runtime.ConstrainedExecution;
using Microsoft.AspNetCore.Identity;

namespace ProjektMVCdotnet8.Controllers
{
    public class PostEntitiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserEntity> _userManager;

        public PostEntitiesController(ApplicationDbContext context, UserManager<UserEntity> userManager)
        {
            _userManager = userManager;//do sprawdzenia uzytkowknika
            _context = context;
        }

        // GET: PostEntities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Posts.ToListAsync());
        }

        // GET: PostEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postEntity = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postEntity == null)
            {
                return NotFound();
            }
            return View(postEntity);
        }

        // GET: PostEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PostEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,PostContent,AttachmentSource,Categories")] PostModel postModel)
        {
            /*if (ModelState.IsValid)
            {
                _context.Add(postEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(postEntity);*/
            PostEntity postEntity = new PostEntity();

            //postEntity.Id = postModel.Id;
            postEntity.Title = postModel.Title;
            postEntity.PostContent = postModel.PostContent;
            var loggedUser = await _userManager.GetUserAsync(User);
            //var user = _context.Users.FirstOrDefault(u => u.UserName == loggedUser);// tu zmieniajcie mail
            postEntity.AuthorUser = loggedUser;
            postEntity.CreatedDate = DateTime.Now;
            if (postModel.AttachmentSource != null && postModel.AttachmentSource.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "attachments");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + postModel.AttachmentSource.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await postModel.AttachmentSource.CopyToAsync(fileStream);
                }
                Console.WriteLine(uniqueFileName);
                postEntity.AttachmentSource = uniqueFileName;
            }
            else
            {
                postEntity.AttachmentSource = null;
            }
            postEntity.Categories = new List<CategoryEntity>();
            var selectedCategoryIds = Request.Form["SelectedCategories"].ToList();
            foreach (var categoryId in selectedCategoryIds)
            {
                var category = _context.Categories.FirstOrDefault(c => c.Id == int.Parse(categoryId));
                if (category != null)
                {
                    postEntity.Categories.Add(category);
                }
            }
            
            


            _context.Add(postEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: PostEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postEntity = await _context.Posts.FindAsync(id);
            if (postEntity == null)
            {
                return NotFound();
            }
            return View(postEntity);
        }

        // POST: PostEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,PostContent,CreatedDate,AttachmentSource")] PostEntity postEntity)
        {
            if (id != postEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostEntityExists(postEntity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(postEntity);
        }

        // GET: PostEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postEntity = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postEntity == null)
            {
                return NotFound();
            }

            return View(postEntity);
        }

        // POST: PostEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postEntity = await _context.Posts.FindAsync(id);
            if (postEntity != null)
            {
                _context.Posts.Remove(postEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostEntityExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
