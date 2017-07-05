using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chemiklani.BL.DTO
{
    public class TeamDetailDTO : BaseDTO
    {
        [Required(ErrorMessage = "Musíte vyplnit název týmu.")]
        public string Name { get; set; }
        public ICollection<string> Members { get; set; }
        public string Room { get; set; }
    }
}