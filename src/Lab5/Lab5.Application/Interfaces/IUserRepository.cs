using Lab5.Application.ResultTypes;
using Lab5.Domain.Entities;

namespace Lab5.Application.Interfaces;

public interface IUserRepository
{
    Task CreateUserAsync(User user);

    Task<ResultT> GetUserByLoginAsync(string login);

    Task<ResultT> GetUserByIdAsync(Guid userId);

    Task UpdateUserAsync(User user);

    Task DeleteUserAsync(User user);
}
