using System.ComponentModel.DataAnnotations;

namespace Chemiklani.BL.DTO
{
    public class SignInDTO
    {
        [Required(ErrorMessage = "Uživatelské jméno nesmí být prázdné")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Heslo nesmí být prázdné"), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}