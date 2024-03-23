using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Entities;

namespace ProjektMVCdotnet8.Controllers
{
    public class CommentEntitiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserEntity> _userManager;
        public CommentEntitiesController(ApplicationDbContext context, UserManager<UserEntity> userManager)
        {
            _userManager = userManager;//do sprawdzenia uzytkowknika
            _context = context;
        }

        // GET: CommentEntities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Comments.ToListAsync());
        }

        // GET: CommentEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentEntity = await _context.Comments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commentEntity == null)
            {
                return NotFound();
            }

            return View(commentEntity);
        }

        // GET: CommentEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CommentEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CommentContent")] CommentEntity commentEntity)
        {
            /*if (ModelState.IsValid)
            {
                _context.Add(commentEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }*/
            commentEntity.AuthorUser = await _userManager.GetUserAsync(User);
            commentEntity.CreatedDate = DateTime.Now;

            _context.Add(commentEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


            //return View(commentEntity);
        }

        // GET: CommentEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentEntity = await _context.Comments.FindAsync(id);
            if (commentEntity == null)
            {
                return NotFound();
            }
            return View(commentEntity);
        }

        // POST: CommentEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CommentContent,CreatedDate")] CommentEntity commentEntity)
        {
            if (id != commentEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commentEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentEntityExists(commentEntity.Id))
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
            return View(commentEntity);
        }

        // GET: CommentEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentEntity = await _context.Comments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commentEntity == null)
            {
                return NotFound();
            }

            return View(commentEntity);
        }

        // POST: CommentEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commentEntity = await _context.Comments.FindAsync(id);
            if (commentEntity != null)
            {
                _context.Comments.Remove(commentEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentEntityExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }


    }
}
