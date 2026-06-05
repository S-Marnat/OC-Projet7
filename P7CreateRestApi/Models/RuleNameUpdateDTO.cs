using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models
{
    public class RuleNameUpdateDTO
    {
        [Required(ErrorMessage = "Le nom de la règle doit être renseigné")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La description de la règle doit être renseignée")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Le champ concernant le JSON doit être renseigné")]
        public string Json { get; set; }

        [Required(ErrorMessage = "Le champ concernant le template doit être renseigné")]
        public string Template { get; set; }

        [Required(ErrorMessage = "Le champ concernant la requête SQL complète doit être renseigné")]
        public string SqlStr { get; set; }

        [Required(ErrorMessage = "Le champ concernant le fragment SQL doit être renseigné")]
        public string SqlPart { get; set; }
    }
}
