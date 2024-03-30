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
using Microsoft.AspNetCore.Identity; //do sprawdzenia uzytkowknika
using Microsoft.AspNetCore.Mvc; //do sprawdzenia uzytkowknika
using System.Drawing.Printing;
using ProjektMVCdotnet8.Interfaces;
using ProjektMVCdotnet8.Repository;

namespace ProjektMVCdotnet8.Controllers
{
   
    public class MessageEntitiesController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;//do sprawdzenia uzytkowknika
        private readonly IMessageRepository _messageRepository;
        private readonly IChatRepository _chatRepository;

        public MessageEntitiesController(UserManager<UserEntity> userManager, IMessageRepository messageRepository, IChatRepository chatRepository)
        {
            _messageRepository = messageRepository;
            _chatRepository = chatRepository;
            _userManager = userManager;//do sprawdzenia uzytkowknika
        }

        // GET: MessageEntities
        public async Task<IActionResult> Index()
        {
            return View(await _messageRepository.GetAll());
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
        public async Task<IActionResult> Create([Bind("Id,MessageContent")] MessageEntity messageEntity,int chatId)
        {
            var chat = await _chatRepository.GetById(chatId);
            var user = await _userManager.GetUserAsync(User);
            messageEntity.UsedChat = chat;
            messageEntity.currentChat = chatId;
            messageEntity.UsingUser = user;
            messageEntity.SendDate = DateTime.Now;
            _messageRepository.Add(messageEntity);
            return RedirectToAction("Chat", "Home", new { chatId = chatId });
        }
    }
}
