namespace Lab5.Domain.Entities;

public class Transaction
{
    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }

    public decimal Amount { get; private set; }

    public TransactionType Type { get; private set; }

    public DateTime Timestamp { get; private set; }

    public Transaction(Guid accountId, decimal amount, TransactionType type)
    {
        Id = Guid.NewGuid();
        UserId = accountId;
        Amount = amount;
        Type = type;
        Timestamp = DateTime.UtcNow;
    }

    public Transaction(Guid accountId, decimal amount, TransactionType type, Guid id, DateTime timestamp)
    {
        Id = id;
        UserId = accountId;
        Amount = amount;
        Type = type;
        Timestamp = timestamp;
    }
}
