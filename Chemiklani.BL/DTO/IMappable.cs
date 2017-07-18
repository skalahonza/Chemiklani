using Riganti.Utils.Infrastructure.Core;

namespace Chemiklani.BL.DTO
{
    public interface IMappable<TEntity,TDTO>
        where TEntity:IEntity<int>
        where TDTO:BaseDTO
    {
        void MapFrom(TEntity entity);
        TEntity MapTo(TDTO dto);
    }
}