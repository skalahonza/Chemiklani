using Chemiklani.DAL.Entities;

namespace Chemiklani.BL.DTO
{
    public class BaseDTO:IEntity<int>
    {
        public int Id { get; set; }
    }
}