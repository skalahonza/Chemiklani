using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace iPodnik.DAL
{
    public class AppDbContext:DbContext// : IdentityDbContext<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
    {

        public AppDbContext() : base("Data Source=(localdb)\\mssqllocaldb; Integrated Security=true; Initial Catalog=Chemiklani")
        {
        }

        public AppDbContext(string connectionString) : base(connectionString)
        {
        }

        //Tables
        //public virtual DbSet<Attachment> Attachments { get; set; }      

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Relations
        }
    }
}
