using API.DTOs;
using API.Entities;

namespace API.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IDataAccessService _dataAccessService;

        public TransactionService(IDataAccessService dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }

        public async Task<ResponseDto> ProcessTransactionAsync(TransactionDto transactionDto)
        {
            var validationResponse = await ValidateBenefitAsync(transactionDto.BenefitId);
            if (!validationResponse.IsSuccessful) return validationResponse;
            
            var benefit = (Benefit)validationResponse.Data;

            validationResponse = await ValidateCardAsync(transactionDto.CardId);
            if (!validationResponse.IsSuccessful) return validationResponse;

            var card = (Card)validationResponse.Data;
           
            var user = await _dataAccessService.GetUserByIdAsync(card.UserID);
            var company = await _dataAccessService.GetCompanyByUserIdAsync(user.ID);
            var merchant = benefit.Merchant;

            validationResponse = ValidateUserBenefitAccess(user.UserType, company.BenefitCategoryForStandardUsers, merchant.Category);
            if (!validationResponse.IsSuccessful) return validationResponse;

            decimal transactionAmount = CalculateTransactionAmount(benefit, user.UserType, company);

            validationResponse = ValidateCardBalance(card, transactionAmount);
            if (!validationResponse.IsSuccessful) return validationResponse;

            card.Balance -= transactionAmount;
            await _dataAccessService.UpdateCardAsync(card);

            var transaction = new Transaction
            {
                DateTime = DateTime.Now,
                Amount = transactionAmount,
                CardID = card.ID,
                MerchantID = merchant.ID
            };
            await _dataAccessService.AddTransactionAsync(transaction);

            var result = new ResponseDto
            {
                IsSuccessful = true,
                Message = "Transaction processed successfully."
            };

            return result;
        }

        private async Task<ResponseDto> ValidateBenefitAsync(int benefitId)
        {
            var result = new ResponseDto();

            var benefit = await _dataAccessService.GetBenefitByIdAsync(benefitId);
            if (benefit == null)
            {
                result.Message = $"Benefit with ID {benefitId} does not exist.";
                return result;
            }

            result.IsSuccessful = true;
            result.Data = benefit;

            return result;
        }

        private async Task<ResponseDto> ValidateCardAsync(int cardId)
        {
            var result = new ResponseDto();

            var card = await _dataAccessService.GetCardByIdAsync(cardId);
            if (card == null)
            {
                result.Message = $"Card with ID {cardId} does not exist.";
                return result;
            }

            if (card.ExpiryDate < DateTime.UtcNow)
            {
                result.Message = "Card has expired.";
                return result;
            }

            result.IsSuccessful = true;
            result.Data = card;

            return result;
        }

        private ResponseDto ValidateCardBalance(Card card, decimal transactionAmount)
        {
            var result = new ResponseDto();

            if (card.Balance < transactionAmount)
            {
                result.Message = "Insufficient funds on the card.";
                return result;
            }

            result.IsSuccessful = true;
            return result;
        }

        private ResponseDto ValidateUserBenefitAccess(UserType userType, BenefitCategory companyBenefitCategory, BenefitCategory merchantBenefitCategory)
        {
            var result = new ResponseDto();

            if (userType == UserType.Standard && companyBenefitCategory != merchantBenefitCategory)
            {
                result.Message = "User does not have access to this category of benefits.";
                return result;
            }

            result.IsSuccessful = true;
            return result;
        }

        private decimal CalculateTransactionAmount(Benefit benefit, UserType userType, CustomerCompany company)
        {          
            decimal transactionAmount;
            var merchant = benefit.Merchant;

            if (userType == UserType.Platinum && company.MerchantsWithDiscountForPlatinumUsers.Contains(merchant))
            {         
                var discount = merchant.DiscountForPlatinumUsers;
                var discountedPrice = benefit.Price * (1 - discount);

                transactionAmount = discountedPrice;
            }
            else
            {
                transactionAmount = benefit.Price;
            }

            return transactionAmount;
        }
    }
}
