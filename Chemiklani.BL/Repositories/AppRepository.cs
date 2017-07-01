using Chemiklani.DAL;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;

namespace Chemiklani.BL.Repositories
{
    public class AppRepository<TEntity, TKey> : EntityFrameworkRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>, new()
    {

        public new AppDbContext Context => (AppDbContext)base.Context;

        public AppRepository(IUnitOfWorkProvider provider, IDateTimeProvider dateTimeNowProvider) : base(provider, dateTimeNowProvider)
        {
        }
    }
}