#pragma warning disable CA2007
using Lab5.Application.Interfaces;
using Lab5.Application.ResultTypes;
using Lab5.Domain.Entities;

namespace Lab5.Application.Operations;

public class ShowBalance
{
    private readonly IUserRepository _accountRepository;

    public ShowBalance(IUserRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<ResultT> ExecuteAsync(Guid accountId)
    {
        User? account = null;
        ResultT result = await _accountRepository.GetUserByIdAsync(accountId);
        if (result is ResultT.SuccessWithData<User> userResult)
        {
            account = userResult.Data;
        }

        if (account == null)
        {
            return new ResultT.Fail();
        }

        return new ResultT.SuccessWithData<decimal>(account.Balance);
    }
}
