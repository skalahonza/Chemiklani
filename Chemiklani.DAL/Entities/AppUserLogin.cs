using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Chemiklani.DAL.Entities
{
    public class AppUserLogin : IdentityUserLogin<int>
    {
        public int Id { get; set; }
    }
}