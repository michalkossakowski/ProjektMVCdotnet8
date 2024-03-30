using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektMVCdotnet8.Areas.Identity.Data;
using ProjektMVCdotnet8.Entities;
using ProjektMVCdotnet8.Interfaces;

namespace ProjektMVCdotnet8.Controllers
{
    public class ChatEntitiesController : Controller
    {
        private readonly IChatRepository _chatRepository;

        public ChatEntitiesController(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        // GET: ChatEntities
        public async Task<IActionResult> Index()
        {
            return View(await _chatRepository.GetAll());
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
            _chatRepository.Add(chatEntity);
            return RedirectToAction(nameof(Index));
        }
    }
}
