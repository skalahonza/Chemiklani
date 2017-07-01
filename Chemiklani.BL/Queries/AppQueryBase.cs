using Chemiklani.DAL;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;

namespace Chemiklani.BL.Queries
{
    public abstract class AppQueryBase<TResult> : EntityFrameworkQuery<TResult>
    {

        public new AppDbContext Context => (AppDbContext) base.Context;

        protected AppQueryBase(IUnitOfWorkProvider provider) : base(provider)
        {
        }
    }
}