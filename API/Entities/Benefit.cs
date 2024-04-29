namespace API.Entities
{
    public class Benefit
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
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