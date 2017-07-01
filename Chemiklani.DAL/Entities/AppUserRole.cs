using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;
using Riganti.Utils.Infrastructure.Core;

namespace Chemiklani.DAL.Entities
{
    public class AppUserRole : IdentityUserRole<int>,IEntity<int>
    {
        public int Id { get; set; }
    }
}