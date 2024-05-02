using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Transaction
    {
        public int ID { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public decimal Amount { get; set; }

        public int CardID { get; set; }
        public Card Card { get; set; }
        public int MerchantID { get; set; }
        public Merchant Merchant { get; set; }
    }
}