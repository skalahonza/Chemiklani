using System.ComponentModel.DataAnnotations;

namespace Chemiklani.BL.DTO
{
    public class AddUserDTO : UserDto
    {
        [Required(ErrorMessage = "�Heslo nesm� �t pr�zdn�."), DataType(DataType.Password), MinLength(6, ErrorMessage = "Heslo mus� m�t alespo� 6 znak�.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Potvrzen� hesla nesm� b�t pr�zdn�"), DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = "Hesla se neshoduj�.")]
        public string PasswordConfirm { get; set; }
    }
}