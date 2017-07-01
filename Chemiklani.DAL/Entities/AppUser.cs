﻿using Microsoft.AspNet.Identity.EntityFramework;

namespace Chemiklani.DAL.Entities
{
    public class AppUser : IdentityUser<int, AppUserLogin, AppUserRole, AppUserClaim>
    {
    }
}
