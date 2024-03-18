using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Entities;

namespace ProjektMVCdotnet8.Controllers
{
    public class ChatEntitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChatEntitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ChatEntities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Chats.ToListAsync());
        }

        // GET: ChatEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatEntity = await _context.Chats
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chatEntity == null)
            {
                return NotFound();
            }

            return View(chatEntity);
        }

        // GET: ChatEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ChatEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] ChatEntity chatEntity)
        {
            _context.Add(chatEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            /*if (ModelState.IsValid)
            {
                _context.Add(chatEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chatEntity);*/
        }

        // GET: ChatEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatEntity = await _context.Chats.FindAsync(id);
            if (chatEntity == null)
            {
                return NotFound();
            }
            return View(chatEntity);
        }

        // POST: ChatEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] ChatEntity chatEntity)
        {
            if (id != chatEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chatEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChatEntityExists(chatEntity.Id))
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
            return View(chatEntity);
        }

        // GET: ChatEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatEntity = await _context.Chats
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chatEntity == null)
            {
                return NotFound();
            }

            return View(chatEntity);
        }

        // POST: ChatEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chatEntity = await _context.Chats.FindAsync(id);
            if (chatEntity != null)
            {
                _context.Chats.Remove(chatEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChatEntityExists(int id)
        {
            return _context.Chats.Any(e => e.Id == id);
        }
    }
}
