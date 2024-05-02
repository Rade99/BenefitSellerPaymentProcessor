using API.Entities;

namespace API.Services
{
    public interface IDataAccessService
    {
        Task<Card> GetCardByIdAsync(int cardId);
        Task UpdateCardAsync(Card card);
        Task<Merchant> GetMerchantByIdAsync(int merchantId);
        Task AddTransactionAsync(Transaction transaction);
        Task<User> GetUserByCardIdAsync(int cardId);
        Task<CustomerCompany> GetCompanyByUserIdAsync(int userId);
        Task<Benefit> GetBenefitByIdAsync(int benefitId);
        Task<User> GetUserByIdAsync(int userId);
    }
}