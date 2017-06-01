using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Chemiklani.DAL.Entities
{
    public class AppUser : IdentityUser<int, AppUserLogin, AppUserRole, AppUserClaim>
    {
    }
}
