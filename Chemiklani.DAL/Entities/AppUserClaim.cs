using Microsoft.AspNet.Identity.EntityFramework;

namespace Chemiklani.DAL.Entities
{
    public class AppUserClaim:IdentityUserClaim<int>, IEntity<int>
    {
        
    }
}