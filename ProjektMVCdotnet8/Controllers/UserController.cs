using Microsoft.AspNetCore.Mvc;
using ProjektMVCdotnet8.Models;
using ProjektMVCdotnet8.Entities;
using Microsoft.AspNetCore.Identity;
using ProjektMVCdotnet8.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace ProjektMVCdotnet8.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;
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
        public UserController(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
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
    }
}