using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class TransactionDto
    {
        [Required(ErrorMessage = "Benefit ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Benefit ID value must be greater than 0")]
        public int BenefitId { get; set; }

        [Required(ErrorMessage = "Card ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Card ID value must be greater than 0")]
        public int CardId { get; set; }
    }
}