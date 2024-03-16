using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ProjektMVCdotnet8.Areas.Identity.Data
{
    public class UserEntity : IdentityUser
    {
        public string Nick { get; set; }
    }
}

