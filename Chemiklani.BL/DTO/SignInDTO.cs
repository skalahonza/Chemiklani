using System.ComponentModel.DataAnnotations;

namespace Chemiklani.BL.DTO
{
    public class SignInDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}