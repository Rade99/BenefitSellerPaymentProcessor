namespace API.Entities
{
    public class Merchant
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public BenefitCategory Category { get; set; }
        public decimal DiscountForPlatinumUsers { get; set; }

        public List<Benefit> Benefits { get; set; }
    }
}