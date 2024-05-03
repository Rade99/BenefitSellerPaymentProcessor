using API.DTOs;
using API.Entities;
using API.Services;
using FakeItEasy;
using FluentAssertions;

namespace API.Tests.Services
{
    public class TransactionServiceTests
    {
        private readonly ITransactionService _transactionService;
        private readonly IDataAccessService _dataAccessService;

        public TransactionServiceTests()
        {
            // Dependencies
            _dataAccessService = A.Fake<IDataAccessService>();

            // SUT (System Under Test)
            _transactionService = new TransactionService(_dataAccessService);
        }

        private void SetupMockData(TransactionDto transactionDto, Benefit benefit, Card card, User user, CustomerCompany company)
        {
            A.CallTo(() => _dataAccessService.GetBenefitByIdAsync(A<int>._)).Returns(Task.FromResult(benefit));
            A.CallTo(() => _dataAccessService.GetCardByIdAsync(A<int>._)).Returns(Task.FromResult(card));
            A.CallTo(() => _dataAccessService.GetUserByIdAsync(A<int>._)).Returns(Task.FromResult(user));
            A.CallTo(() => _dataAccessService.GetCompanyByUserIdAsync(A<int>._)).Returns(Task.FromResult(company));
        }


        [Fact]
        public async Task ProcessTransaction_InvalidBenefit_ReturnsBadRequest()
        {
            // Arrange
            var transactionDto = new TransactionDto
            {
                BenefitId = 12
            };

            A.CallTo(() => _dataAccessService.GetBenefitByIdAsync(transactionDto.BenefitId))
                .Returns(Task.FromResult<Benefit?>(null));

            // Act
            var result = await _transactionService.ProcessTransactionAsync(transactionDto);

            // Assert
            result.IsSuccessful.Should().BeFalse();
            result.Message.Should().Contain($"Benefit with ID {transactionDto.BenefitId} does not exist.");
        }

        [Fact]
        public async Task ProcessTransaction_InvalidCard_ReturnsBadRequest()
        {
            // Arrange
            var transactionDto = new TransactionDto
            {
                CardId = 5
            };

            A.CallTo(() => _dataAccessService.GetBenefitByIdAsync(A<int>._))
                .Returns(Task.FromResult<Benefit?>(new Benefit()));

            A.CallTo(() => _dataAccessService.GetCardByIdAsync(transactionDto.CardId))
                .Returns(Task.FromResult<Card?>(null));

            // Act
            var result = await _transactionService.ProcessTransactionAsync(transactionDto);

            // Assert
            result.IsSuccessful.Should().BeFalse();
            result.Message.Should().Contain($"Card with ID {transactionDto.CardId} does not exist.");
        }

        [Fact]
        public async Task ProcessTransaction_ExpiredCard_ReturnsBadRequest()
        {
            // Arrange
            var transactionDto = new TransactionDto
            {
                CardId = 456
            };
            var expiredCard = new Card { ExpiryDate = DateTime.UtcNow.AddDays(-1) };

            A.CallTo(() => _dataAccessService.GetCardByIdAsync(A<int>._))
                .Returns(Task.FromResult<Card?>(expiredCard));

            // Act
            var result = await _transactionService.ProcessTransactionAsync(transactionDto);

            // Assert
            result.IsSuccessful.Should().BeFalse();
            result.Message.Should().Be("Card has expired.");
        }

        [Fact]
        public async Task ProcessTransaction_InvalidUserBenefitAccess_ReturnsBadRequest()
        {
            // Arrange
            var transactionDto = A.Fake<TransactionDto>();
            var user = new User { UserType = UserType.Standard };
            var company = new CustomerCompany { BenefitCategoryForStandardUsers = BenefitCategory.FoodAndDrink };
            var merchant = new Merchant { Category = BenefitCategory.Recreation };
            var benefit = new Benefit { Merchant = merchant };
            var card = new Card { ExpiryDate = DateTime.UtcNow.AddDays(1) };

            SetupMockData(transactionDto, benefit, card, user, company);

            // Act
            var result = await _transactionService.ProcessTransactionAsync(transactionDto);

            // Assert
            result.IsSuccessful.Should().BeFalse();
            result.Message.Should().Be("User does not have access to this category of benefits.");
        }

        [Fact]
        public async Task ProcessTransaction_InsufficientFunds_ReturnsBadRequest()
        {
            // Arrange
            var transactionDto = A.Fake<TransactionDto>();
            var user = new User { UserType = UserType.Premium };
            var benefit = new Benefit 
            { 
                Price = 150, 
                Merchant = new Merchant() 
            };
            var insufficientFundsCard = new Card
            {
                ExpiryDate = DateTime.UtcNow.AddDays(1),
                Balance = 100
            };

            SetupMockData(transactionDto, benefit, insufficientFundsCard, user, new CustomerCompany());

            // Act
            var result = await _transactionService.ProcessTransactionAsync(transactionDto);

            // Assert
            result.IsSuccessful.Should().BeFalse();
            result.Message.Should().Be("Insufficient funds on the card.");
        }

        [Fact]
        public async Task ProcessTransaction_StandardUser_ReturnsSuccessResponse()
        {
            // Arrange
            var transactionDto = A.Fake<TransactionDto>();
            var user = new User { UserType = UserType.Standard };
            var merchant = new Merchant { Category = BenefitCategory.FoodAndDrink };
            var benefit = new Benefit 
            { 
                Price = 50, 
                Merchant = merchant 
            };
            var card = new Card
            {
                ExpiryDate = DateTime.UtcNow.AddDays(1),
                Balance = 200
            };        
            var company = new CustomerCompany { BenefitCategoryForStandardUsers = BenefitCategory.FoodAndDrink };

            SetupMockData(transactionDto, benefit, card, user, company);

            // Act
            var result = await _transactionService.ProcessTransactionAsync(transactionDto);

            // Assert
            result.IsSuccessful.Should().BeTrue();
            result.Message.Should().Be("Transaction processed successfully.");
        }

        [Fact]
        public async Task ProcessTransaction_PremiumUser_ReturnsSuccessResponse()
        {
            // Arrange
            var transactionDto = A.Fake<TransactionDto>();
            var user = new User { UserType = UserType.Premium };
            var benefit = new Benefit 
            { 
                Price = 50,
                Merchant = new Merchant()
            };
            var card = new Card
            {
                ExpiryDate = DateTime.UtcNow.AddDays(1),
                Balance = 200
            };

            SetupMockData(transactionDto, benefit, card, user, new CustomerCompany());

            // Act
            var result = await _transactionService.ProcessTransactionAsync(transactionDto);

            // Assert
            result.IsSuccessful.Should().BeTrue();
            result.Message.Should().Be("Transaction processed successfully.");
        }

        [Fact]
        public async Task ProcessTransaction_PlatinumUserWithoutMerchantDiscount_ReturnsSuccessResponse()
        {
            // Arrange
            var transactionDto = A.Fake<TransactionDto>();
            var user = new User { UserType = UserType.Platinum };
            var benefit = new Benefit 
            { 
                Price = 100,
                Merchant = new Merchant()
            };
            var card = new Card
            {
                ExpiryDate = DateTime.UtcNow.AddDays(1),
                Balance = 200
            };

            SetupMockData(transactionDto, benefit, card, user, new CustomerCompany());

            // Act
            var result = await _transactionService.ProcessTransactionAsync(transactionDto);

            // Assert
            result.IsSuccessful.Should().BeTrue();
            result.Message.Should().Be("Transaction processed successfully.");
        }

        [Fact]
        public async Task ProcessTransaction_PlatinumUserWithMerchantDiscount_ReturnsSuccessResponse()
        {
            // Arrange
            var transactionDto = A.Fake<TransactionDto>();
            var user = new User { UserType = UserType.Platinum };
            var merchant = new Merchant { DiscountForPlatinumUsers = 0.10m };
            var benefit = new Benefit 
            { 
                Price = 100,
                Merchant = merchant
            };
            var card = new Card
            {
                ExpiryDate = DateTime.UtcNow.AddDays(1),
                Balance = 200
            };

            var company = new CustomerCompany { };
            company.MerchantsWithDiscountForPlatinumUsers.Add(merchant);

            SetupMockData(transactionDto, benefit, card, user, company);

            // Act
            var result = await _transactionService.ProcessTransactionAsync(transactionDto);

            // Assert
            result.IsSuccessful.Should().BeTrue();
            result.Message.Should().Be("Transaction processed successfully.");
        }
    }
}
