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
using ProjektMVCdotnet8.Interfaces;
using ProjektMVCdotnet8.Repository;

namespace ProjektMVCdotnet8.Controllers
{
    public class ContactEntitiesController : Controller
    {
        private readonly IContactRepository _contactRepository;

        public ContactEntitiesController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        // GET: ContactEntities
        public async Task<IActionResult> Index()
        {
            return View(await _contactRepository.GetAll());
        }

        // GET: ContactEntities/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var contactEntity = await _contactRepository.GetById(id);
            return View(contactEntity);
        }

        // GET: ContactEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactEntities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Topic,ContactType,ContactContent")] ContactEntity contactEntity)
        {
            contactEntity.ContactDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                _contactRepository.Add(contactEntity);
                return RedirectToAction("ThxForContact", "Home", contactEntity);
            }
            return View(contactEntity);
        }

        // GET: ContactEntities/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var contactEntity = await _contactRepository.GetById(id);

            return View(contactEntity);
        }

        // POST: ContactEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _contactRepository.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
