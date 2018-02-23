using Microsoft.AspNet.Identity.EntityFramework;

namespace Chemiklani.DAL.Entities
{
    public class AppUserLogin : IdentityUserLogin<int>, IEntity<int>
    {
        public int Id { get; set; }
    }
}