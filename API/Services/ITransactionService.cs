using API.DTOs;

namespace API.Services
{
    public interface ITransactionService
    {
        Task<ResponseDto> ProcessTransactionAsync(TransactionDto transactionDto);
    }
}