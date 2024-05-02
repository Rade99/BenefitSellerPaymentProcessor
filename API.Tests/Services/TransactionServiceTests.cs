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
        public async Task ProcessTransaction_InvalidMerchant_ReturnsBadRequest()
        {
            // Arrange
            var transactionDto = new TransactionDto
            {
                MerchantId = 456
            };
            var card = new Card { ExpiryDate = DateTime.UtcNow.AddDays(1) };

            A.CallTo(() => _dataAccessService.GetBenefitByIdAsync(A<int>._))
                .Returns(Task.FromResult<Benefit?>(new Benefit()));

            A.CallTo(() => _dataAccessService.GetCardByIdAsync(A<int>._))
                .Returns(Task.FromResult<Card?>(card));

            A.CallTo(() => _dataAccessService.GetMerchantByIdAsync(transactionDto.MerchantId))
                .Returns(Task.FromResult<Merchant?>(null));

            // Act
            var result = await _transactionService.ProcessTransactionAsync(transactionDto);

            // Assert
            result.IsSuccessful.Should().BeFalse();
            result.Message.Should().Contain($"Merchant with ID {transactionDto.MerchantId} does not exist.");
        }

        [Fact]
        public async Task ProcessTransaction_InvalidUserBenefitAccess_ReturnsBadRequest()
        {
            // Arrange
            var transactionDto = A.Fake<TransactionDto>();
            var user = new User { UserType = UserType.Standard };
            var company = new CustomerCompany { BenefitCategoryForStandardUsers = BenefitCategory.FoodAndDrink };
            var merchant = new Merchant { Category = BenefitCategory.Recreation };

            var card = new Card { ExpiryDate = DateTime.UtcNow.AddDays(1) };

            A.CallTo(() => _dataAccessService.GetBenefitByIdAsync(A<int>._))
                .Returns(Task.FromResult<Benefit?>(new Benefit()));

            A.CallTo(() => _dataAccessService.GetCardByIdAsync(A<int>._))
                .Returns(Task.FromResult<Card?>(card));

            A.CallTo(() => _dataAccessService.GetMerchantByIdAsync(A<int>._))
                .Returns(Task.FromResult<Merchant?>(merchant));

            A.CallTo(() => _dataAccessService.GetUserByIdAsync(A<int>._))
                .Returns(Task.FromResult<User?>(user));

            A.CallTo(() => _dataAccessService.GetCompanyByUserIdAsync(A<int>._))
                .Returns(Task.FromResult<CustomerCompany?>(company));

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
            var benefit = new Benefit { Price = 150 };

            var insufficientFundsCard = new Card 
            { 
                ExpiryDate = DateTime.UtcNow.AddDays(1), 
                Balance = 100 
            };

            A.CallTo(() => _dataAccessService.GetBenefitByIdAsync(A<int>._))
                .Returns(Task.FromResult<Benefit?>(benefit));

            A.CallTo(() => _dataAccessService.GetCardByIdAsync(A<int>._))
                .Returns(Task.FromResult<Card?>(insufficientFundsCard));

            A.CallTo(() => _dataAccessService.GetMerchantByIdAsync(A<int>._))
                .Returns(Task.FromResult<Merchant?>(new Merchant()));

            A.CallTo(() => _dataAccessService.GetUserByIdAsync(A<int>._))
                .Returns(Task.FromResult<User?>(user));

            A.CallTo(() => _dataAccessService.GetCompanyByUserIdAsync(A<int>._))
                .Returns(Task.FromResult<CustomerCompany?>(new CustomerCompany()));

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
            var benefit = new Benefit { Price = 50 };
            var card = new Card
            {
                ExpiryDate = DateTime.UtcNow.AddDays(1),
                Balance = 200
            };
            var merchant = new Merchant { Category = BenefitCategory.FoodAndDrink };
            var company = new CustomerCompany { BenefitCategoryForStandardUsers = BenefitCategory.FoodAndDrink };

            A.CallTo(() => _dataAccessService.GetBenefitByIdAsync(A<int>._))
                .Returns(Task.FromResult<Benefit?>(benefit));

            A.CallTo(() => _dataAccessService.GetCardByIdAsync(A<int>._))
                .Returns(Task.FromResult<Card?>(card));

            A.CallTo(() => _dataAccessService.GetMerchantByIdAsync(A<int>._))
                .Returns(Task.FromResult<Merchant?>(merchant));

            A.CallTo(() => _dataAccessService.GetUserByIdAsync(A<int>._))
                .Returns(Task.FromResult<User?>(user));

            A.CallTo(() => _dataAccessService.GetCompanyByUserIdAsync(A<int>._))
                .Returns(Task.FromResult<CustomerCompany?>(company));

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
            var benefit = new Benefit { Price = 50 };
            var card = new Card
            {
                ExpiryDate = DateTime.UtcNow.AddDays(1),
                Balance = 200
            };

            A.CallTo(() => _dataAccessService.GetBenefitByIdAsync(A<int>._))
                .Returns(Task.FromResult<Benefit?>(benefit));

            A.CallTo(() => _dataAccessService.GetCardByIdAsync(A<int>._))
                .Returns(Task.FromResult<Card?>(card));

            A.CallTo(() => _dataAccessService.GetMerchantByIdAsync(A<int>._))
                .Returns(Task.FromResult<Merchant?>(new Merchant()));

            A.CallTo(() => _dataAccessService.GetUserByIdAsync(A<int>._))
                .Returns(Task.FromResult<User?>(user));

            A.CallTo(() => _dataAccessService.GetCompanyByUserIdAsync(A<int>._))
                .Returns(Task.FromResult<CustomerCompany?>(new CustomerCompany()));

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
            var benefit = new Benefit { Price = 100 };
            var card = new Card
            {
                ExpiryDate = DateTime.UtcNow.AddDays(1),
                Balance = 200
            };
     
            A.CallTo(() => _dataAccessService.GetBenefitByIdAsync(A<int>._))
                .Returns(Task.FromResult<Benefit?>(benefit));

            A.CallTo(() => _dataAccessService.GetCardByIdAsync(A<int>._))
                .Returns(Task.FromResult<Card?>(card));

            A.CallTo(() => _dataAccessService.GetMerchantByIdAsync(A<int>._))
                .Returns(Task.FromResult<Merchant?>(new Merchant()));

            A.CallTo(() => _dataAccessService.GetUserByIdAsync(A<int>._))
                .Returns(Task.FromResult<User?>(user));

            A.CallTo(() => _dataAccessService.GetCompanyByUserIdAsync(A<int>._))
                .Returns(Task.FromResult<CustomerCompany?>(new CustomerCompany()));

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
            var benefit = new Benefit { Price = 100 };
            var card = new Card
            {
                ExpiryDate = DateTime.UtcNow.AddDays(1),
                Balance = 200
            };
            var merchant = new Merchant { DiscountForPlatinumUsers = 0.10m };
            var company = new CustomerCompany{};
            company.MerchantsWithDiscountForPlatinumUsers.Add(merchant);

            A.CallTo(() => _dataAccessService.GetBenefitByIdAsync(A<int>._))
                .Returns(Task.FromResult<Benefit?>(benefit));

            A.CallTo(() => _dataAccessService.GetCardByIdAsync(A<int>._))
                .Returns(Task.FromResult<Card?>(card));

            A.CallTo(() => _dataAccessService.GetMerchantByIdAsync(A<int>._))
                .Returns(Task.FromResult<Merchant?>(merchant));

            A.CallTo(() => _dataAccessService.GetUserByIdAsync(A<int>._))
                .Returns(Task.FromResult<User?>(user));

            A.CallTo(() => _dataAccessService.GetCompanyByUserIdAsync(A<int>._))
                .Returns(Task.FromResult<CustomerCompany?>(company));

            // Act
            var result = await _transactionService.ProcessTransactionAsync(transactionDto);

            // Assert
            result.IsSuccessful.Should().BeTrue();
            result.Message.Should().Be("Transaction processed successfully.");
        }
    }
}
