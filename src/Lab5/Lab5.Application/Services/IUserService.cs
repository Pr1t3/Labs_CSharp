using Lab5.Application.ResultTypes;

namespace Lab5.Application.Services;

public interface IUserService
{
    Task<ResultT> CreateUserAsync(string login, string password);

    Task<ResultT> DeleteUserAsync(string login);

    Task<ResultT> AuthenticateUserAsync(string login, string password);
}
