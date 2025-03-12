using Lab5.Domain.Entities;

namespace Lab5.Application.Interfaces;

public interface ITransactionRepository
{
    Task AddTransactionAsync(Transaction transaction);

    Task<IEnumerable<Transaction>> GetTransactionsByUserIdAsync(Guid userId);
}
