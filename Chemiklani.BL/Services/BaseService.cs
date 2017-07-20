using Chemiklani.DAL;

namespace Chemiklani.BL.Services
{
    public class BaseService
    {
        protected AppDbContext CreateDbContext()
        {
            return new AppDbContext();
        }
    }
}