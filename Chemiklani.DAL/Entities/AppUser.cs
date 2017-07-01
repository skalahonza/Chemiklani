using Microsoft.AspNet.Identity.EntityFramework;

namespace Chemiklani.DAL.Entities
{
    public class AppUser : IdentityUser<long, AppUserLogin, AppUserRole, AppUserClaim>
    {
    }
}
