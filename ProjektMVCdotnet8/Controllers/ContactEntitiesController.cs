using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Entities;

namespace ProjektMVCdotnet8.Controllers
{

    public class ContactEntitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactEntitiesController(ApplicationDbContext context)
        {
            _context = context;

            // tworzenie przykaldowych danych
            if (_context.ContactForms.IsNullOrEmpty()) //Sprawdza czy tablica jest pusta, jeżeli tak to tworzy elementy do tablicy
            {
                CreateElements();
            }
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
            contactEntity.ContactDate = DateTime.Now; // ustawienie daty kontaktu przy tworzeniu
            if (ModelState.IsValid)
            {
                _context.Add(contactEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction("ThxForContact", "Home",contactEntity);
                //return RedirectToAction(nameof(Index));
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
        //Rekordy testowe ----Usunąć w finalnej wersji----
        public async Task CreateElements()
        {
            if (ModelState.IsValid)
            {
                ContactEntity contact = new ContactEntity();
                contact.Email = "joe@mama.com";
                contact.Topic = "Nie jestem moderatorem a widze panel moderatora";
                contact.ContactType = "Znalazłem błąd";
                contact.ContactContent = "Wydaję mi się że to trochę niebezpieczne ale bede korzystał póki mogę";
                contact.ContactDate = DateTime.Now;
                _context.Add(contact);

                contact = new ContactEntity();
                contact.Email = "niewidomyMaciek@onet.pl";
                contact.Topic = "Nic nie widzę";
                contact.ContactType = "Strona nie działa";
                contact.ContactContent = "Wchodzę na stronę a tu ciemno, dobrze że mogę kliknąć w formularz kontaktowy";
                contact.ContactDate = DateTime.Now;
                _context.Add(contact);

                contact = new ContactEntity();
                contact.Email = "WrumWruom@bmw.pl";
                contact.Topic = "Powinniście dodać coś o samochodach";
                contact.ContactType = "Propozycja zmian";
                contact.ContactContent = "Uwielbiam wasze forum ale przydała by się kategoria motoryzacja muszę zapytać się o poradę jak wstwić nowy silnik do mojego 30 letniego bmw bo stary niestety uległ awari";
                contact.ContactDate = DateTime.Now;
                _context.Add(contact);

                contact = new ContactEntity();
                contact.Email = "GrzegorzHejter@wp.pl";
                contact.Topic = "Obrzydliwa czcionka";
                contact.ContactType = "Chciałbym podzielić się swoją opinią";
                contact.ContactContent = "Uważam że wasza strona powinna zmienić czcionkę na comic sans jest taka mięciutka i miła w czytaniu a to co jest teraz to skandal";
                contact.ContactDate = DateTime.Now;
                _context.Add(contact);

                contact = new ContactEntity();
                contact.Email = "cojarobietutaj@sanah.kabanosy";
                contact.Topic = "Zastanawialiście się kiedyś jak samkuje drewno";
                contact.ContactType = "Inne";
                contact.ContactContent = "No właśnie ja też nie";
                contact.ContactDate = DateTime.Now;
                _context.Add(contact);

                await _context.SaveChangesAsync();
            }
        }
    }
}
