using Riganti.Utils.Infrastructure.Core;

namespace Chemiklani.DAL.Entities
{
    public class Score : IEntity<long>
    {
        public long Id { get; set; }
        public Task Task { get; set; }
        public Team Team { get; set; }
        public int Points { get; set; }
    }
}