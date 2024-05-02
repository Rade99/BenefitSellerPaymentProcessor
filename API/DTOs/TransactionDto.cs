using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class TransactionDto
    {
        [Required(ErrorMessage = "Benefit ID is required")]
        public int BenefitId { get; set; }

        [Required(ErrorMessage = "Card ID is required")]
        public int CardId { get; set; }

        [Required(ErrorMessage = "Merchant ID is required")]
        public int MerchantId { get; set; }
    }
}