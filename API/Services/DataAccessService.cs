using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class DataAccessService : IDataAccessService
    {
        private readonly PaymentProcessorDbContext _dbContext;
        public DataAccessService(PaymentProcessorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Card> GetCardByIdAsync(int cardId)
        {
            return await _dbContext.Cards.FindAsync(cardId);
        }

        public async Task UpdateCardAsync(Card card)
        {
            _dbContext.Cards.Update(card);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            _dbContext.Transactions.Add(transaction);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CustomerCompany> GetCompanyByUserIdAsync(int userId)
        {
            return await _dbContext.CustomerCompanies
                                    .Include(c => c.MerchantsWithDiscountForPlatinumUsers)
                                    .FirstOrDefaultAsync(c => c.Employees.Any(e => e.ID == userId));
        }

        public async Task<Benefit> GetBenefitByIdAsync(int benefitId)
        {
            return await _dbContext.Benefits
                                    .Include(b => b.Merchant)
                                    .FirstOrDefaultAsync(b => b.ID == benefitId);
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }
    }
}