using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models
{
    public class BidListCreateDTO
    {
        [Required(ErrorMessage = "Le nom du client concerné doit être renseigné")]
        public string Account { get; set; }

        [Required(ErrorMessage = "Le type d'offre doit être renseigné")]
        public string BidType { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "La quantité renseignée doit être positive")]
        public double? BidQuantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "La quantité renseignée doit être positive")]
        public double? AskQuantity { get; set; }

        [DataType(DataType.Currency, ErrorMessage = "Le prix renseigné n'est pas valide")]
        public double? Bid { get; set; }

        [DataType(DataType.Currency, ErrorMessage = "Le prix renseigné n'est pas valide")]
        public double? Ask { get; set; }

        [Required(ErrorMessage = "L'indice de référence doit être renseigné")]
        public string Benchmark { get; set; }

        public DateTime? BidListDate { get; set; }

        [Required(ErrorMessage = "Un commentaire doit être renseigné")]
        public string Commentary { get; set; }

        [Required(ErrorMessage = "Le nom du produit financier doit être renseigné")]
        public string BidSecurity { get; set; }

        [Required(ErrorMessage = "Le statut de l'offre doit être renseigné")]
        public string BidStatus { get; set; }

        [Required(ErrorMessage = "Le nom du trader responsable doit être renseigné")]
        public string Trader { get; set; }

        [Required(ErrorMessage = "Le portefeuille auquel appartient l'offre doit être renseigné")]
        public string Book { get; set; }

        [Required(ErrorMessage = "Le nom du deal associé doit être renseigné")]
        public string DealName { get; set; }

        [Required(ErrorMessage = "Le type de deal doit être renseigné")]
        public string DealType { get; set; }

        [Required(ErrorMessage = "Le côté de la transaction doit être renseigné")]
        public string Side { get; set; }
    }
}
