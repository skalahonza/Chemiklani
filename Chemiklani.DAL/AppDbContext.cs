using System.Data.Entity;
using Chemiklani.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Chemiklani.DAL
{
    public class AppDbContext: IdentityDbContext<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
    {

        public AppDbContext() : base("DB")
        {
        }

        public AppDbContext(string connectionString) : base(connectionString)
        {
        }

        //Tables
        public virtual DbSet<Score> Scores { get; set; }      
        public virtual DbSet<Task> Tasks { get; set; }      
        public virtual DbSet<Team> Teams { get; set; }      

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Relations
        }
    }
}
