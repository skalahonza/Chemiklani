using System.ComponentModel.DataAnnotations;

namespace Chemiklani.BL.DTO
{
    public class UserDto:BaseDTO
    {
        [Required(ErrorMessage = "Uživatelské jméne je povinné.")]
        public string UserName { get; set; }       

        public bool IsAdmin { get; set; }
    }
}