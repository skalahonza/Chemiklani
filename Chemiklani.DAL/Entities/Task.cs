using Riganti.Utils.Infrastructure.Core;

namespace Chemiklani.DAL.Entities
{
    public class Task: IEntity<long>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int MaximumPoints { get; set; }
    }
}