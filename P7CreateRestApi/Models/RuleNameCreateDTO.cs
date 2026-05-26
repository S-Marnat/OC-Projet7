using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models
{
    public class RuleNameCreateDTO
    {
        [Required(ErrorMessage = "Le nom de la règle doit être renseigné")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La description de la règle doit être renseignée")]
        public string Description { get; set; }

        public string? Json { get; set; }

        public string? Template { get; set; }

        public string? SqlStr { get; set; }

        public string? SqlPart { get; set; }
    }
}
