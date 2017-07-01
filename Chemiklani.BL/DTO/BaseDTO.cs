using Riganti.Utils.Infrastructure.Core;

namespace Chemiklani.BL.DTO
{
    public class BaseDTO:IEntity<int>
    {
        public int Id { get; set; }
    }
}