#pragma warning disable CA2007
using Lab5.Application.Interfaces;
using Lab5.Domain.Entities;
using Npgsql;

namespace Lab5.Infrastructure.Data;

public class TransactionRepository : ITransactionRepository
{
    private readonly string _connectionString;

    public TransactionRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task AddTransactionAsync(Transaction transaction)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        string query = "INSERT INTO Transactions (Id, UserId, Amount, Type, Timestamp) VALUES (@Id, @UserId, @Amount, @Type, @Timestamp)";
        using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", transaction.Id);
        command.Parameters.AddWithValue("@UserId", transaction.UserId);
        command.Parameters.AddWithValue("@Amount", transaction.Amount);
        command.Parameters.AddWithValue("@Type", transaction.Type.ToString());
        command.Parameters.AddWithValue("@Timestamp", transaction.Timestamp);

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByUserIdAsync(Guid userId)
    {
        var transactions = new List<Transaction>();

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Transactions WHERE UserId = @UserId ORDER BY Timestamp DESC";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserId", userId);

            await connection.OpenAsync();
            using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                string? readerType = reader["Type"].ToString();
                if (readerType != null)
                {
                    var transaction = new Transaction((Guid)reader["UserId"], reader.GetDecimal(reader.GetOrdinal("Amount")), Enum.Parse<TransactionType>(readerType), (Guid)reader["Id"], reader.GetDateTime(reader.GetOrdinal("Timestamp")));
                    transactions.Add(transaction);
                }
            }
        }

        return transactions;
    }
}
