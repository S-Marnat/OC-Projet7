using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "L'identifiant utilisateur doit être renseigné")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Le nom de l'utilisateur doit être renseigné")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Le mot de passe doit être renseigné")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "La confirmation du mot de passe doit être renseignée")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas.")]
        public string ConfirmedPassword { get; set; }
    }
}
