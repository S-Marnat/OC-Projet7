using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models
{
    public class TradeUpdateDTO
    {
        [Required(ErrorMessage = "Le compte concerné par le trade doit être renseigné")]
        public string Account { get; set; }

        [Required(ErrorMessage = "Le type de trade doit être renseigné")]
        public string AccountType { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "La quantité achetée doit être positive")]
        public double? BuyQuantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "La quantité vendue doit être positive")]
        public double? SellQuantity { get; set; }

        [DataType(DataType.Currency, ErrorMessage = "Le prix d'achat n'est pas valide")]
        public double? BuyPrice { get; set; }

        [DataType(DataType.Currency, ErrorMessage = "Le prix de vente n'est pas valide")]
        public double? SellPrice { get; set; }

        public DateTime? TradeDate { get; set; }

        [Required(ErrorMessage = "Le nom du produit financier doit être renseigné")]
        public string TradeSecurity { get; set; }

        [Required(ErrorMessage = "Le statut du trade doit être renseigné")]
        public string TradeStatus { get; set; }

        [Required(ErrorMessage = "Le nom du trader responsable doit être renseigné")]
        public string Trader { get; set; }

        [Required(ErrorMessage = "L'indice de référence doit être renseigné")]
        public string Benchmark { get; set; }

        [Required(ErrorMessage = "Le portefeuille auquel appartient le trade doit être renseigné")]
        public string Book { get; set; }

        [Required(ErrorMessage = "Le nom du deal associé doit être renseigné")]
        public string DealName { get; set; }

        [Required(ErrorMessage = "Le type de deal doit être renseigné")]
        public string DealType { get; set; }

        [Required(ErrorMessage = "La source du lien externe doit être renseignée")]
        public string SourceListId { get; set; }

        [Required(ErrorMessage = "Le côté de la transaction doit être renseigné")]
        public string Side { get; set; }
    }
}
