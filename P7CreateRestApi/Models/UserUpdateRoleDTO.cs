using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models
{
    public class UserUpdateRoleDTO
    {
        [Required(ErrorMessage = "Le nouveau rôle de l'utilisateur doit être renseigné")]
        [RegularExpression(@"^(Admin|User)$", ErrorMessage = "Le rôle ne peut qu'être Admin ou User")]
        public string Role { get; set; }
    }
}
