using System.ComponentModel.DataAnnotations;

namespace Chemiklani.BL.DTO
{
    public class RegisterNewUserDTO
    {
        [Required(ErrorMessage = "Uživatelské jméne je povinné.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email je povinný a mìl by být validní."), DataType(DataType.EmailAddress, ErrorMessage = "Email není validní.")]
        public string Email { get; set; }

        public bool IsAdmin { get; set; }
    }
}