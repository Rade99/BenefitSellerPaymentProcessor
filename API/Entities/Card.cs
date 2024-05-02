using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    [Index(nameof(CardNumber), IsUnique = true)]
    public class Card
    {
        public int ID { get; set; }
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        [Required]
        public decimal Balance { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }
    }
}