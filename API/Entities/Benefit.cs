using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Benefit
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public BenefitCategory Category { get; set; }

        public int MerchantID { get; set; }
        public Merchant Merchant { get; set; }
    }

    public enum BenefitCategory
    {
        FoodAndDrink,
        Recreation,
        Education,
        Culture,
        Traveling,
        Shopping
    }
}