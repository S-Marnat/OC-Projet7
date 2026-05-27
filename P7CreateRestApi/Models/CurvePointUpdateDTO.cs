using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models
{
    public class CurvePointUpdateDTO
    {
        [Range(0, 255, ErrorMessage = "L'ID de la courbe doit être compris entre 0 et 255")]
        public byte? CurveId { get; set; }

        public DateTime? AsOfDate { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Le nombre d'années doit être positif")]
        public double? Term { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "La valeur du point doit être positive")]
        public double? CurvePointValue { get; set; }
    }
}
