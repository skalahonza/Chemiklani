using Riganti.Utils.Infrastructure.Core;

namespace Chemiklani.BL.DTO
{
    public class BaseDTO:IEntity<long>
    {
        public long Id { get; set; }
    }
}