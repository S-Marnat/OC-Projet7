using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models
{
    public class RatingCreateDTO
    {
        [Required(ErrorMessage = "La note donnée par Moody's doit être renseignée")]
        [MinLength(1, ErrorMessage = "La note donnée par Moody's doit contenir au moins 1 caractère")]
        [MaxLength(4, ErrorMessage = "La note donnée par Moody's ne doit pas dépasser 4 caractères")]
        [RegularExpression(@"^(Aaa|Aa[1-3]|A[1-3]|Baa[1-3]|Ba[1-3]|B[1-3]|Caa|Ca|C|P-[1-3]|NP)$", ErrorMessage = "Le format de la note donnée par Moody's n'est pas valide")]
        public string MoodysRating { get; set; }


        [Required(ErrorMessage = "La note donnée par Standard & Poor's doit être renseignée")]
        [MinLength(1, ErrorMessage = "La note donnée par Standard & Poor's doit contenir au moins 1 caractère")]
        [MaxLength(4, ErrorMessage = "La note donnée par Standard & Poor's ne doit pas dépasser 4 caractères")]
        [RegularExpression(@"^(AAA|AA[+-]?|A[+-]?|BBB[+-]?|BB[+-]?|B[+-]?|CCC[+-]?|CC|C|D|SD)$", ErrorMessage = "Le format de la note donnée par Standard & Poor's n'est pas valide")]
        public string SandPRating { get; set; }


        [Required(ErrorMessage = "La note donnée par Fitch doit être renseignée")]
        [MinLength(1, ErrorMessage = "La note donnée par Fitch doit contenir au moins 1 caractère")]
        [MaxLength(4, ErrorMessage = "La note donnée par Fitch ne doit pas dépasser 4 caractères")]
        [RegularExpression(@"^(AAA|AA[+-]?|A[+-]?|BBB[+-]?|BB[+-]?|B[+-]?|CCC[+-]?|CC|C|D|RD)$", ErrorMessage = "Le format de la note donnée par Fitch n'est pas valide")]
        public string FitchRating { get; set; }
    }
}
