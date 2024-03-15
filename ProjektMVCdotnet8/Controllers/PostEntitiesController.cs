using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektMVCdotnet8.Entities;

namespace ProjektMVCdotnet8.Controllers
{
    public class PostEntitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostEntitiesController(ApplicationDbContext context)
        {
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
        public IActionResult Create(int? userId)
        {
            ViewData["useriID"]= userId;
            return View();
        }

        // POST: PostEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AuthorUser,Title,PostContent,CreatedDate,AttachmentSource")] PostEntity postEntity, int? userId)
        {
            Console.WriteLine("ID: "+postEntity.Id);
            //Console.WriteLine(postEntity.AttachmentSource);

            Console.WriteLine("UserID podany: " + userId);
            var user = _context.Users.Find(1);

            Console.WriteLine("UserID: " + postEntity.AuthorUser);
            postEntity.AuthorUser = user;
            ViewData["userID"] = userId;
            if (ModelState.IsValid)
            {
                _context.Add(postEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("nie dodano");
            }
            return View(postEntity);
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
