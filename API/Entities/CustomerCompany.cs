using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class CustomerCompany
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        public BenefitCategory BenefitCategoryForStandardUsers { get; set; }
        public List<Merchant> MerchantsWithDiscountForPlatinumUsers { get; set; } = new();
        public List<User> Employees { get; set; }
    }
}