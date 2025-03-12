#pragma warning disable CA2007
using Lab5.Application.Interfaces;
using Lab5.Application.ResultTypes;
using Lab5.Application.Services;
using Lab5.Domain.Entities;

namespace Lab5.Application.Operations;

public class CreateUserAccount
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserAccount(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<ResultT> ExecuteAsync(string login, string password)
    {
        string hashedPassword = _passwordHasher.HashPassword(password);

        try
        {
            var user = new User(login, hashedPassword);
            await _userRepository.CreateUserAsync(user);
        }
        catch
        {
            return new ResultT.Fail();
        }

        return new ResultT.Success();
    }
}
