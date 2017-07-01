using System.Collections.Generic;
using Riganti.Utils.Infrastructure.Core;

namespace Chemiklani.DAL.Entities
{
    public class Team:IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<string> Members { get; set; }
    }
}