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
    public class MessageEntitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MessageEntitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MessageEntities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Messages.ToListAsync());
        }

        // GET: MessageEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messageEntity = await _context.Messages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (messageEntity == null)
            {
                return NotFound();
            }

            return View(messageEntity);
        }

        // GET: MessageEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MessageEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MessageContent")] MessageEntity messageEntity)
        {
            var chat = _context.Chats.Find(1);
            var user = _context.Users.FirstOrDefault(u => u.Email == "WERYKTEST@PL");// tu zmieniajcie mail
            messageEntity.UsedChat = chat;
            messageEntity.UsingUser = user;
            messageEntity.SendDate = DateTime.Now;
            _context.Add(messageEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            /*if (ModelState.IsValid)
            {
                _context.Add(messageEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(messageEntity);*/
        }

        // GET: MessageEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messageEntity = await _context.Messages.FindAsync(id);
            if (messageEntity == null)
            {
                return NotFound();
            }
            return View(messageEntity);
        }

        // POST: MessageEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MessageContent,SendDate")] MessageEntity messageEntity)
        {
            if (id != messageEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(messageEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageEntityExists(messageEntity.Id))
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
            return View(messageEntity);
        }

        // GET: MessageEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messageEntity = await _context.Messages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (messageEntity == null)
            {
                return NotFound();
            }

            return View(messageEntity);
        }

        // POST: MessageEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var messageEntity = await _context.Messages.FindAsync(id);
            if (messageEntity != null)
            {
                _context.Messages.Remove(messageEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageEntityExists(int id)
        {
            return _context.Messages.Any(e => e.Id == id);
        }
    }
}
