using System.Collections.Generic;

namespace Chemiklani.DAL.Entities
{
    public class Team:IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<string> Members { get; set; }
        public string Room { get; set; }
        public int? Category { get; set; }
        public string School { get; set; }
    }
}