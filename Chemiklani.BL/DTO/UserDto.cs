using System.ComponentModel.DataAnnotations;

namespace Chemiklani.BL.DTO
{
    public class UserDto:BaseDTO
    {
        [Required(ErrorMessage = "U�ivatelsk� jm�ne je povinn�.")]
        public string UserName { get; set; }       

        public bool IsAdmin { get; set; }
    }
}