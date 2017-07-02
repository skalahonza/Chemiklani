using System.ComponentModel.DataAnnotations;

namespace Chemiklani.BL.DTO
{
    public class RegisterNewUserDTO
    {
        [Required(ErrorMessage = "U�ivatelsk� jm�ne je povinn�.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email je povinn� a m�l by b�t validn�."), DataType(DataType.EmailAddress, ErrorMessage = "Email nen� validn�.")]
        public string Email { get; set; }

        public bool IsAdmin { get; set; }
    }
}