using Riganti.Utils.Infrastructure.Core;
using iPodnik.DAL;
using Riganti.Utils.Infrastructure.EntityFramework;

namespace iPodnik.BL.Repositories
{
    public class AppRepository<TEntity, TKey> : EntityFrameworkRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>, new()
    {

        public new AppDbContext Context => (AppDbContext)base.Context;

        public AppRepository(IUnitOfWorkProvider provider, IDateTimeProvider dateTimeNowProvider) : base(provider, dateTimeNowProvider)
        {
        }
    }
}