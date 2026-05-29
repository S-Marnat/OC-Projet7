using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "L'identifiant utilisateur doit être renseigné")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Le mot de passe doit être renseigné")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
