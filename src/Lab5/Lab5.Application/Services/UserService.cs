#pragma warning disable CA2007
using Lab5.Application.Interfaces;
using Lab5.Application.Operations;
using Lab5.Application.ResultTypes;
using Lab5.Domain.Entities;

namespace Lab5.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly CreateUserAccount _createUserAccount;
    private readonly DeleteUserAccount _deleteUserAccount;

    public UserService(IUserRepository userRepository, CreateUserAccount createUserAccount, DeleteUserAccount deleteUserAccount, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _createUserAccount = createUserAccount;
        _passwordHasher = passwordHasher;
        _deleteUserAccount = deleteUserAccount;
    }

    public async Task<ResultT> CreateUserAsync(string login, string password)
    {
        return await _createUserAccount.ExecuteAsync(login, password);
    }

    public async Task<ResultT> AuthenticateUserAsync(string login, string password)
    {
        User? user = null;
        ResultT result = await _userRepository.GetUserByLoginAsync(login);
        if (result is ResultT.SuccessWithData<User> userResult)
        {
            user = userResult.Data;
        }

        if (user == null)
        {
            return new ResultT.Fail();
        }

        if (!_passwordHasher.VerifyPassword(password, user.Password))
        {
            return new ResultT.Fail();
        }

        return result;
    }

    public async Task<ResultT> DeleteUserAsync(string login)
    {
        User? user = null;
        ResultT result = await _userRepository.GetUserByLoginAsync(login);
        if (result is ResultT.SuccessWithData<User> userResult)
        {
            user = userResult.Data;
        }

        if (user == null)
        {
            return new ResultT.Fail();
        }

        return await _deleteUserAccount.ExecuteAsync(user);
    }
}
