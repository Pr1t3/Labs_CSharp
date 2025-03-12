#pragma warning disable CA2007
using Lab5.Application.Interfaces;
using Lab5.Application.ResultTypes;
using Lab5.Domain.Entities;
using Npgsql;
using System.Data;

namespace Lab5.Infrastructure.Data;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task CreateUserAsync(User user)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        string query = "INSERT INTO Users (Id, Login, PasswordHash, Balance) VALUES (@Id, @Login, @PasswordHash, @Balance)";
        using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", user.Id);
        command.Parameters.AddWithValue("@Login", user.Login);
        command.Parameters.AddWithValue("@PasswordHash", user.Password);
        command.Parameters.AddWithValue("@Balance", user.Balance);

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }

    public async Task<ResultT> GetUserByLoginAsync(string login)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            using var command = new NpgsqlCommand();
            command.CommandText = "SELECT * FROM Users WHERE Login = @Login";
            command.Connection = connection;
            command.Parameters.AddWithValue("@Login", SqlDbType.VarChar).Value = login;

            await connection.OpenAsync();
            using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                string? readerLogin = reader["Login"].ToString();
                string? readerPasswordHash = reader["PasswordHash"].ToString();
                if (readerLogin != null && readerPasswordHash != null)
                {
                    var user = new User(readerLogin, readerPasswordHash, (Guid)reader["Id"], reader.GetDecimal(reader.GetOrdinal("Balance")));
                    return new ResultT.SuccessWithData<User>(user);
                }
            }
        }

        return new ResultT.Fail();
    }

    public async Task<ResultT> GetUserByIdAsync(Guid userId)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            using var command = new NpgsqlCommand();
            command.CommandText = "SELECT * FROM Users WHERE Id = @Id";
            command.Connection = connection;
            command.Parameters.AddWithValue("@Id", SqlDbType.VarChar).Value = userId;

            await connection.OpenAsync();
            using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                string? readerLogin = reader["Login"].ToString();
                string? readerPasswordHash = reader["PasswordHash"].ToString();
                if (readerLogin != null && readerPasswordHash != null)
                {
                    var user = new User(readerLogin, readerPasswordHash, (Guid)reader["Id"], reader.GetDecimal(reader.GetOrdinal("Balance")));
                    return new ResultT.SuccessWithData<User>(user);
                }
            }
        }

        return new ResultT.Fail();
    }

    public async Task UpdateUserAsync(User user)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        string query = "UPDATE Users SET Balance = @Balance WHERE Id = @Id";
        using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@Balance", user.Balance);
        command.Parameters.AddWithValue("@Id", user.Id);

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteUserAsync(User user)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        string query = "DELETE FROM Users WHERE Id = @Id";
        using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", user.Id);

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }
}
