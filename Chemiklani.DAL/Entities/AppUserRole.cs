using Microsoft.AspNet.Identity.EntityFramework;

namespace Chemiklani.DAL.Entities
{
    public class AppUserRole : IdentityUserRole<int>,IEntity<int>
    {
        public int Id { get; set; }
    }
}