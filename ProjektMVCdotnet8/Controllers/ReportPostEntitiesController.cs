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
    public class ReportPostEntitiesController : Controller
    {
        public readonly ApplicationDbContext _context;

        public ReportPostEntitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReportPostEntities
        public async Task<IActionResult> Index()
        {
            return View(await _context.ReportPosts.ToListAsync());
        }

        // GET: ReportPostEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportPostEntity = await _context.ReportPosts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reportPostEntity == null)
            {
                return NotFound();
            }

            return View(reportPostEntity);
        }

        // GET: ReportPostEntities/Create
        public IActionResult Create(int repPost)
        {
            ViewBag.repPost = repPost;
            return View();
        }

        // POST: ReportPostEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ReportContent")] ReportPostEntity reportPostEntity,int repPost)
        {
            reportPostEntity.ReportedPost = _context.Posts.FirstOrDefault(p => p.Id == repPost);
            reportPostEntity.postId = repPost;
            _context.Add(reportPostEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Home");
            //return RedirectToAction(nameof(Index));
            /*if (ModelState.IsValid)
            {
                _context.Add(reportPostEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reportPostEntity);*/
        }

        // GET: ReportPostEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportPostEntity = await _context.ReportPosts.FindAsync(id);
            if (reportPostEntity == null)
            {
                return NotFound();
            }
            return View(reportPostEntity);
        }

        // POST: ReportPostEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReportContent")] ReportPostEntity reportPostEntity)
        {
            if (id != reportPostEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reportPostEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportPostEntityExists(reportPostEntity.Id))
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
            return View(reportPostEntity);
        }

        // GET: ReportPostEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportPostEntity = await _context.ReportPosts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reportPostEntity == null)
            {
                return NotFound();
            }

            return View(reportPostEntity);
        }

        // POST: ReportPostEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reportPostEntity = await _context.ReportPosts.FindAsync(id);
            if (reportPostEntity != null)
            {
                _context.ReportPosts.Remove(reportPostEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportPostEntityExists(int id)
        {
            return _context.ReportPosts.Any(e => e.Id == id);
        }
    }
}
