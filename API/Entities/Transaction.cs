namespace API.Entities
{
    public class Transaction
    {
        public int ID { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Amount { get; set; }

        public int CardID { get; set; }
        public Card Card { get; set; }
        public int MerchantID { get; set; }
        public Merchant Merchant { get; set; }
    }
}