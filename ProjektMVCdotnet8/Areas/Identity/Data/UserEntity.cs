using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ProjektMVCdotnet8.Areas.Identity.Data
{
    public class UserEntity : IdentityUser
    {
        // to zrobi entity framework - Identity
        public int Id { get; set; }
        public string Nick { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime JoinDate { get; set; }
        public string Avatar { get; set; }
        public int Points { get; set; } // my musimy dodać reszta jest w identity
    }
}

