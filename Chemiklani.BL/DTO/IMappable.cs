using Chemiklani.DAL.Entities;

namespace Chemiklani.BL.DTO
{
    public interface IMappable<in TEntity>
        where TEntity:IEntity<int>
    {
        void MapFrom(TEntity entity);
        void MapTo(TEntity entity);
    }
}