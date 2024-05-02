using API.Controllers;
using API.DTOs;
using API.Services;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace API.Tests.Controllers
{
    public class TransactionControllerTests
    {
        private TransactionController _transactionController;
        private ITransactionService _transactionService;

        public TransactionControllerTests()
        {
            // Dependencies
            _transactionService = A.Fake<ITransactionService>();

            // SUT (System Under Test)
            _transactionController = new TransactionController(_transactionService);
        }

        [Fact]
        public async Task ProcessTransaction_ValidTransaction_ReturnsOkResult()
        {
            // Arrange
            A.CallTo(() => _transactionService.ProcessTransactionAsync(A<TransactionDto>._))
                .Returns(new ResponseDto { IsSuccessful = true, Message = "Transaction successful", Data = null });

            // Act
            var result = await _transactionController.ProcessTransaction(null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            okResult.Value.Should().Be("Transaction successful");
        }

        [Fact]
        public async Task ProcessTransaction_InvalidTransaction_ReturnsBadRequest()
        {
            // Arrange
            A.CallTo(() => _transactionService.ProcessTransactionAsync(A<TransactionDto>._))
                .Returns(new ResponseDto { IsSuccessful = false, Message = "Insufficient funds", Data = null });

            // Act
            var result = await _transactionController.ProcessTransaction(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            badRequestResult.Value.Should().Be("Insufficient funds");
        }

        [Fact]
        public async Task ProcessTransaction_InvalidTransaction_ReturnsBadRequestWhenModelStateIsInvalid()
        {
            // Arrange
            _transactionController.ModelState.AddModelError("TransactionDto", "Invalid transaction model");

            // Act
            var result = await _transactionController.ProcessTransaction(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            badRequestResult.Value.Should().BeOfType<SerializableError>();
        }
    }
}
