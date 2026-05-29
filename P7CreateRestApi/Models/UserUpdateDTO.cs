using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models
{
    public class UserUpdateDTO
    {
        [Required(ErrorMessage = "Le nom de l'utilisateur doit être renseigné")]
        public string Fullname { get; set; }
    }
}
