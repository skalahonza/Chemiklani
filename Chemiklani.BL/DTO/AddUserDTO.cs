using System.ComponentModel.DataAnnotations;

namespace Chemiklani.BL.DTO
{
    public class AddUserDTO : UserDto
    {
        [Required(ErrorMessage = "´Heslo nesmí ýt prázdné."), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Potvrzení hesla nesmí být prázdné"), DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = "Hesla se neshodují.")]
        public string PasswordConfirm { get; set; }
    }
}