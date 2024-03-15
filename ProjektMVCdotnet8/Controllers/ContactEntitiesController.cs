using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektMVCdotnet8.Entities;

namespace ProjektMVCdotnet8.Controllers
{
    public class ContactEntitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactEntitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ContactEntities
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContactForms.ToListAsync());
        }

        // GET: ContactEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactEntity = await _context.ContactForms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactEntity == null)
            {
                return NotFound();
            }

            return View(contactEntity);
        }

        // GET: ContactEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Topic,ContactType,ContactContent")] ContactEntity contactEntity)
        {
            contactEntity.ContactDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(contactEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactEntity);
        }

        // GET: ContactEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactEntity = await _context.ContactForms.FindAsync(id);
            if (contactEntity == null)
            {
                return NotFound();
            }
            return View(contactEntity);
        }

        // POST: ContactEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Topic,ContactType,ContactContent,ContactDate")] ContactEntity contactEntity)
        {
            if (id != contactEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactEntityExists(contactEntity.Id))
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
            return View(contactEntity);
        }

        // GET: ContactEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactEntity = await _context.ContactForms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactEntity == null)
            {
                return NotFound();
            }

            return View(contactEntity);
        }

        // POST: ContactEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactEntity = await _context.ContactForms.FindAsync(id);
            if (contactEntity != null)
            {
                _context.ContactForms.Remove(contactEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactEntityExists(int id)
        {
            return _context.ContactForms.Any(e => e.Id == id);
        }
    }
}
