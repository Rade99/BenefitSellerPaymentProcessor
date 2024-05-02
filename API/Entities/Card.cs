using Microsoft.EntityFrameworkCore;

namespace API.Entities
{
    [Index(nameof(CardNumber), IsUnique = true)]
    public class Card
    {
        public int ID { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal Balance { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }
    }
}