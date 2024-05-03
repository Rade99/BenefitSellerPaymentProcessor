using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class PaymentProcessorDbContext : DbContext
    {
        public PaymentProcessorDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<CustomerCompany> CustomerCompanies { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Benefit> Benefits { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerCompany>().HasData(
                   new CustomerCompany { ID = 1, Name = "Novatix", BenefitCategoryForStandardUsers = BenefitCategory.FoodAndDrink },
                   new CustomerCompany { ID = 2, Name = "CodeTech", BenefitCategoryForStandardUsers = BenefitCategory.Recreation },
                   new CustomerCompany { ID = 3, Name = "Simplify", BenefitCategoryForStandardUsers = BenefitCategory.Shopping }
               );

            modelBuilder.Entity<User>().HasData(
                   new User { ID = 1, FirstName = "Petar", LastName = "Maric", UserType = UserType.Standard, Email = "petar.maric@gmail.com", CustomerCompanyID = 1 },
                   new User { ID = 2, FirstName = "Milos", LastName = "Saric", UserType = UserType.Standard, Email = "milos.saric@gmail.com", CustomerCompanyID = 1 },
                   new User { ID = 3, FirstName = "Nedeljko", LastName = "Racic", UserType = UserType.Premium, Email = "nedeljko.rac@yahoo.com", CustomerCompanyID = 3 },
                   new User { ID = 4, FirstName = "Stefan", LastName = "Perovic", UserType = UserType.Premium, Email = "stefan.perovic@yahoo.com", CustomerCompanyID = 2 },
                   new User { ID = 5, FirstName = "Mitar", LastName = "Jovic", UserType = UserType.Platinum, Email = "mitarjovic@gmail.com", CustomerCompanyID = 1 },
                   new User { ID = 6, FirstName = "Rista", LastName = "Gocic", UserType = UserType.Platinum, Email = "rista88@gmail.com", CustomerCompanyID = 2 }
                );

            modelBuilder.Entity<Card>().HasData(
                  new Card { ID = 1, CardNumber = "1234567890123456", ExpiryDate = new DateTime(2025, 12, 31), Balance = 120000, UserID = 1 },
                  new Card { ID = 2, CardNumber = "9876543210987654", ExpiryDate = new DateTime(2024, 11, 30), Balance = 22000, UserID = 2 },
                  new Card { ID = 3, CardNumber = "5678901234567890", ExpiryDate = new DateTime(2023, 10, 29), Balance = 1600, UserID = 3 },
                  new Card { ID = 4, CardNumber = "3210987654321098", ExpiryDate = new DateTime(2025, 9, 28), Balance = 5000, UserID = 4 },
                  new Card { ID = 5, CardNumber = "4567890123456789", ExpiryDate = new DateTime(2024, 8, 27), Balance = 10000, UserID = 5 },
                  new Card { ID = 6, CardNumber = "9012345678901234", ExpiryDate = new DateTime(2023, 7, 26), Balance = 9700, UserID = 6 }
                );

            modelBuilder.Entity<Benefit>().HasData(
                 new Benefit { ID = 1, Name = "Discounted Meal", Price = 500, Category = BenefitCategory.FoodAndDrink, MerchantID = 1 },
                 new Benefit { ID = 2, Name = "Gym Membership", Price = 1500, Category = BenefitCategory.Recreation, MerchantID = 2 },
                 new Benefit { ID = 3, Name = "Museum Ticket", Price = 1000, Category = BenefitCategory.Culture, MerchantID = 3 },
                 new Benefit { ID = 4, Name = "Online Course", Price = 10000, Category = BenefitCategory.Education, MerchantID = 4 }
                );

                
            modelBuilder.Entity<Merchant>().HasData(
                new Merchant { ID = 1, Name = "Pause", Category = BenefitCategory.FoodAndDrink, DiscountForPlatinumUsers = 0.10m, CustomerCompanyId = 1 },
                new Merchant { ID = 2, Name = "MegaGym", Category = BenefitCategory.Recreation, DiscountForPlatinumUsers = 0.15m, CustomerCompanyId = 1 },
                new Merchant { ID = 3, Name = "Museum of Contemporary Art", Category = BenefitCategory.Culture, DiscountForPlatinumUsers = 0.20m, CustomerCompanyId = 2 },
                new Merchant { ID = 4, Name = "Educons Online Learning ", Category = BenefitCategory.Education, DiscountForPlatinumUsers = 0.25m, CustomerCompanyId = 3 }
                );

            modelBuilder.Entity<Transaction>().HasData(
                new Transaction { ID = 1, DateTime = DateTime.Now, Amount = 16000, CardID = 1, MerchantID = 1 },
                new Transaction { ID = 2, DateTime = DateTime.Now, Amount = 250, CardID = 2, MerchantID = 2 },
                new Transaction { ID = 3, DateTime = DateTime.Now, Amount = 10000.55m, CardID = 3, MerchantID = 3 },
                new Transaction { ID = 4, DateTime = DateTime.Now, Amount = 1500.12m, CardID = 4, MerchantID = 4 }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}