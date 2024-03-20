﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ProjektMVCdotnet8.Areas.Identity.Data
{
    public class UserEntity : IdentityUser
    {
        public string Nick { get; set; }

        [Display(Name = "Kraj pochodzenia")]
        public string? Country { get; set; }

        [Display(Name = "Avatar")]
        public string? Avatar { get; set; }

    }
}

