#pragma warning disable CA2007
using Lab5.Application.Interfaces;
using Lab5.Application.ResultTypes;
using Lab5.Domain.Entities;
using Lab5.Domain.ResultTypes;

namespace Lab5.Application.Operations;

public class WithdrawMoney
{
    private readonly IUserRepository _accountRepository;
    private readonly ITransactionRepository _transactionRepository;

    public WithdrawMoney(IUserRepository accountRepository, ITransactionRepository transactionRepository)
    {
        _accountRepository = accountRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task<ResultT> ExecuteAsync(Guid accountId, decimal amount)
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

        UserResult res = account.Withdraw(amount);
        if (res is UserResult.Fail)
        {
            return new ResultT.Fail();
        }

        try
        {
            await _accountRepository.UpdateUserAsync(account);
        }
        catch
        {
            return new ResultT.Fail();
        }

        var transaction = new Transaction(accountId, amount, TransactionType.Withdrawal);
        try
        {
            await _transactionRepository.AddTransactionAsync(transaction);
        }
        catch
        {
            return new ResultT.Fail();
        }

        return new ResultT.Success();
    }
}
