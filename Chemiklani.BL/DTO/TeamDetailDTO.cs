using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chemiklani.BL.DTO
{
    public class TeamDetailDTO : BaseDTO
    {
        [Required]
        public string Name { get; set; }
        public ICollection<string> Members { get; set; }
    }
}