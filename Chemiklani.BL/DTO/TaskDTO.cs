using System.ComponentModel.DataAnnotations;

namespace Chemiklani.BL.DTO
{
    public class TaskDTO:BaseDTO
    {
        [Required(ErrorMessage = "Musíte vyplnit název úlohy.")]
        public string Name { get; set; }
        
        public string Description { get; set; }

        public int MaximumPoints { get; set; }
    }
}