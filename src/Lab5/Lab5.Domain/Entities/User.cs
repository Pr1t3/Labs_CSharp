using Lab5.Domain.ResultTypes;
using System.Text.RegularExpressions;

namespace Lab5.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }

    public string Login { get; private set; }

    public string Password { get; private set; }

    public decimal Balance { get; private set; }

    public User(string login, string password)
    {
        if (string.IsNullOrWhiteSpace(login))
        {
            throw new ArgumentException("Login cannot be empty.");
        }

        if (!Regex.IsMatch(login, @"^[a-zA-Z0-9]{5,20}$"))
        {
            throw new ArgumentException("Login must be contain only numbers and letters and must be between 5 to 20 characters.");
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Password hash cannot be empty.");
        }

        Id = Guid.NewGuid();
        Login = login;
        Password = password;
        Balance = 0;
    }

    public User(string login, string password, Guid id, decimal balance)
    {
        if (string.IsNullOrWhiteSpace(login))
        {
            throw new ArgumentException("Login cannot be empty.");
        }

        if (!Regex.IsMatch(login, @"^[a-zA-Z0-9]{5,20}$"))
        {
            throw new ArgumentException("Login must be contain only numbers and letters and must be between 5 to 20 characters.");
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Password hash cannot be empty.");
        }

        Id = id;
        Login = login;
        Password = password;
        Balance = balance;
    }

    public UserResult Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            return new UserResult.Fail();
        }

        Balance += amount;
        return new UserResult.Success();
    }

    public UserResult Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            return new UserResult.Fail();
        }

        if (Balance < amount)
        {
            return new UserResult.Fail();
        }

        Balance -= amount;
        return new UserResult.Success();
    }
}