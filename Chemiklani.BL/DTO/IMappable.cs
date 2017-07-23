using Riganti.Utils.Infrastructure.Core;

namespace Chemiklani.BL.DTO
{
    public interface IMappable<TEntity>
        where TEntity:IEntity<int>
    {
        void MapFrom(TEntity entity);
        void MapTo(TEntity entity);
    }
}