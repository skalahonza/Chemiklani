using System.ComponentModel.DataAnnotations;

namespace Chemiklani.BL.DTO
{
    public class AddUserDTO : UserDto
    {
        [Required(ErrorMessage = "Password cannot be empty."), DataType(DataType.Password), MinLength(6, ErrorMessage = "Password must be at least 6 charracters long.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password confirm cannot be empty."), DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = "Password, password confirm mismatch.")]
        public string PasswordConfirm { get; set; }
    }
}