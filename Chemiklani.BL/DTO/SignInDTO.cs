using System.ComponentModel.DataAnnotations;

namespace Chemiklani.BL.DTO
{
    public class SignInDTO
    {
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required."), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}