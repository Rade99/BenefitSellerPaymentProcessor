using Microsoft.EntityFrameworkCore;

namespace API.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserType UserType { get; set; }
        public string Email { get; set; }

        public Card Card { get; set; }
        public int CustomerCompanyID { get; set; }
        public CustomerCompany CustomerCompany { get; set; }
    }

    public enum UserType
    {
        Standard,
        Premium,
        Platinum
    }
}