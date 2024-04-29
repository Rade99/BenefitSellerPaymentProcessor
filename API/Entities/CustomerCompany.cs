namespace API.Entities
{
    public class CustomerCompany
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public BenefitCategory BenefitCategoryForStandardUsers { get; set; }
        public List<Merchant> MerchantsWithDiscountForPlatinumUsers { get; set; }
        public List<User> Employees { get; set; }
    }
}