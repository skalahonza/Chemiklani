using Riganti.Utils.Infrastructure.Core;
using iPodnik.DAL;
using Riganti.Utils.Infrastructure.EntityFramework;

namespace iPodnik.BL.Queries.FirstLevel
{
    public abstract class AppFirstLevelQueryBase<TResult> : EntityFrameworkFirstLevelQueryBase<TResult> where TResult : class
    {
        public new AppDbContext Context => (AppDbContext)base.Context;

        protected AppFirstLevelQueryBase(IUnitOfWorkProvider unitOfWorkProvider) : base(unitOfWorkProvider)
        {
        }
    }
}