using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models
{
    public class UserUpdatePasswordDTO
    {
        [Required(ErrorMessage = "L'ancien mot de passe doit être renseigné")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Le nouveau mot de passe doit être renseigné")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "La confirmation du nouveau mot de passe doit être renseignée")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Les mots de passe ne correspondent pas.")]
        public string ConfirmedPassword { get; set; }
    }
}
