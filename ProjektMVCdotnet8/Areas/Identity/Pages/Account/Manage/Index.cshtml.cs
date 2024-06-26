﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjektMVCdotnet8.Areas.Identity.Data;

namespace ProjektMVCdotnet8.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public IndexModel(
            UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager,
            IWebHostEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Display(Name = "Nazwa Użytkownika")]
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Numer Telefonu")]
            public string PhoneNumber { get; set; }
            [Display(Name = "Kraj Pochodzenia")]
            public string? Country { get; set; }

            [Display(Name = "Miasto")]
            public string? City { get; set; }
            [Display(Name = "Avatar")]
            public string? Avatar { get; set; }
            public IFormFile? AttachmentSource { get; set; }
            public int? Points { get; set; }
            [Display(Name = "O Mnie")]
            [StringLength(250, ErrorMessage = "Opis nie może być dłuższy niż 250 znaków.")]
            public string? Description { get; set; }
        }

        private async Task<string?> GetCountryAsync(UserEntity user)
        {
            return user.Country ?? null; //ustwia null jeśli będzie pusta
        }
        private async Task<string?> GetCityAsync(UserEntity user)
        {
            return user.City ?? null; //ustwia null jeśli będzie pusta
        }
        private async Task<string?> GetDescriptionAsync(UserEntity user)
        {
            return user.Description ?? null; //ustwia null jeśli będzie pusta
        }
        private async Task<string?> GetAvatarAsync(UserEntity user)
        {
            return user.Avatar ?? null; //ustwia null jeśli będzie pusta
        }
        private async Task<int?> GetPointsAsync(UserEntity user)
        {
            return user.Points ?? null; //ustwia null jeśli będzie pusta
        }

        private async Task LoadAsync(UserEntity user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var country = await GetCountryAsync(user);
            var city = await GetCityAsync(user);
            var avatar = await GetAvatarAsync(user);
            var points = await GetPointsAsync(user);
            var description = await GetDescriptionAsync(user);
            Username = userName;
            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Country = country,
                City = city,
                Avatar = avatar,
                Points = points,
                Description = description
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }
            if (Input.Description != user.Description)
            {
                user.Description = Input.Description;
                await _userManager.UpdateAsync(user);
            }
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Nieoczekiwany błąd z numerem telefonu.";
                    return RedirectToPage();
                }
            }

            if (Input.Country != user.Country)
            {
                user.Country = Input.Country;
                await _userManager.UpdateAsync(user);
            }

            if (Input.City != user.City)
            {
                user.City = Input.City;
                await _userManager.UpdateAsync(user);
            }

            if (Input.AttachmentSource != null && Input.AttachmentSource.Length > 0)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "attachments");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Input.AttachmentSource.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Input.AttachmentSource.CopyToAsync(fileStream);
                }
                user.Avatar = uniqueFileName;
                await _userManager.UpdateAsync(user);
            }
            else
            {
                user.Avatar = null;
            }


            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Twój profil został zmodyfikowany.";
            return RedirectToPage();
        }
    }
}
