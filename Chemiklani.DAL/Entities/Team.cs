using System.Collections.Generic;

namespace Chemiklani.DAL.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<string> Members { get; set; } = new List<string>();
    }
}