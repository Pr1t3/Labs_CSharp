#pragma warning disable CA2007
using Lab5.Application.Interfaces;
using Lab5.Application.ResultTypes;
using Lab5.Domain.Entities;

namespace Lab5.Application.Operations;

public class DeleteUserAccount
{
    private readonly IUserRepository _userRepository;

    public DeleteUserAccount(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResultT> ExecuteAsync(User user)
    {
        try
        {
            await _userRepository.DeleteUserAsync(user);
        }
        catch
        {
            return new ResultT.Fail();
        }

        return new ResultT.Success();
    }
}
