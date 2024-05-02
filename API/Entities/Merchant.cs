using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Merchant
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public BenefitCategory Category { get; set; }
        [Required]
        public decimal DiscountForPlatinumUsers { get; set; }

        public List<Benefit> Benefits { get; set; }
    }
}