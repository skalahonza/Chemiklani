using System.ComponentModel.DataAnnotations;

namespace Chemiklani.BL.DTO
{
    public class RegisterNewUserDTO
    {
        [Required]
        public string UserName { get; set; }

        public bool IsAdmin { get; set; }
    }
}