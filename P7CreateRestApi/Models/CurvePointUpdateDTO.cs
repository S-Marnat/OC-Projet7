using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models
{
    public class CurvePointUpdateDTO
    {
        [Required(ErrorMessage = "La date à laquelle la valeur du point est valable doit être renseignée")]
        public DateTime AsOfDate { get; set; }

        [Required(ErrorMessage = "Le nombre d'années doit être renseigné")]
        [Range(0, double.MaxValue, ErrorMessage = "Le nombre d'années renseigné doit être positive")]
        public double Term { get; set; }

        [Required(ErrorMessage = "La valeur du point doit être renseignée")]
        [Range(0, double.MaxValue, ErrorMessage = "La valeur du point renseignée doit être positive")]
        public double CurvePointValue { get; set; }
    }
}
