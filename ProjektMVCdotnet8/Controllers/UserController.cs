using Microsoft.AspNetCore.Mvc;
using ProjektMVCdotnet8.Models;
using ProjektMVCdotnet8.Entities;
using Microsoft.AspNetCore.Identity;
using ProjektMVCdotnet8.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Security.Policy;

namespace ProjektMVCdotnet8.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;
        public ApplicationDbContext _context;
        SignInManager<UserEntity> _signInManager;
        public class InputModel
        {
            [Display(Name = "Nazwa Użytkownika")]
            public string? UserName { get; set; }
            [Display(Name = "Kraj Pochodzenia")]
            public string? Country { get; set; }

            [Display(Name = "Miasto")]
            public string? City { get; set; }
            [Display(Name = "Avatar")]
            public string? Avatar { get; set; }
            public int? Points { get; set; }
            [Display(Name = "O Mnie")]
            [StringLength(250, ErrorMessage = "Opis nie może być dłuższy niż 250 znaków.")]
            public string? Description { get; set; }
        }
        public UserController(UserManager<UserEntity> userManager, ApplicationDbContext context, SignInManager<UserEntity> signInManager)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }

        // Akcja do wyświetlania profilu użytkownika
        public IActionResult Profile(string username)
        {
            var user = _userManager.FindByNameAsync(username).Result;

            if (user == null)
            {
                return NotFound();
            }

            var userProfileModel = new InputModel
            {
                UserName = user.UserName,
                Avatar = user.Avatar,
                Country = user.Country,
                City = user.City,
                Points = user.Points,
                Description = user.Description
            };

            return View(userProfileModel); // Przekazanie obiektu zawierającego wybrane właściwości użytkownika do widoku
        }
        public async Task<IActionResult> AddChat()
        {
            var user1 = await _userManager.GetUserAsync(User);
            var user2 = _context.Users.FirstOrDefault(u => u.UserName == (string)Request.Form["uname"]);

            foreach(var chat in _context.Chats)
            {
                if((chat.User1Nick == user1.UserName && chat.User2Nick == user2.UserName) || (chat.User1Nick == user2.UserName && chat.User2Nick == user1.UserName))
                {
                    return RedirectToAction("Chat", "Home", new { chatId = chat.Id });
                }
            }

            var chatEntity = new ChatEntity();
            chatEntity.ChattingUser1 = user1;
            chatEntity.User1Nick = user1.UserName;
            chatEntity.ChattingUser2 = user2;
            chatEntity.User2Nick = user2.UserName;
            _context.Add(chatEntity);

            MessageEntity messageEntity = new MessageEntity();
            messageEntity.MessageContent = "Rozpoczęcie chatu";
            messageEntity.UsedChat = chatEntity;
            messageEntity.currentChat = chatEntity.Id;
            messageEntity.UsingUser = user2;
            messageEntity.SendDate = DateTime.Now;
            _context.Add(messageEntity);

            _context.SaveChanges();

            int chatId = chatEntity.Id;

            return RedirectToAction("Chat","Home", new { chatId = chatId });
        }
    }
}