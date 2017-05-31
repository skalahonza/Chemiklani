using System.Collections.Generic;
using System.ComponentModel;

namespace Chemiklani.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<string> Members { get; set; } = new List<string>();
    }
}