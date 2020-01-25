using System.ComponentModel.DataAnnotations;

namespace Chemiklani.BL.DTO
{
    public class UserDto:BaseDTO
    {
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }       

        public bool IsAdmin { get; set; }
    }
}