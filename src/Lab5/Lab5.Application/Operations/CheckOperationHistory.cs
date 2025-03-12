#pragma warning disable CA2007
using Lab5.Application.Interfaces;
using Lab5.Application.ResultTypes;
using Lab5.Domain.Entities;

namespace Lab5.Application.Operations;

public class CheckOperationHistory
{
    private readonly ITransactionRepository _transactionRepository;

    public CheckOperationHistory(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<ResultT> ExecuteAsync(Guid accountId)
    {
        IEnumerable<Transaction>? transactions = null;
        try
        {
            transactions = await _transactionRepository.GetTransactionsByUserIdAsync(accountId);
        }
        catch
        {
            return new ResultT.Fail();
        }

        if (transactions == null)
        {
            return new ResultT.Fail();
        }

        return new ResultT.SuccessWithData<IEnumerable<Transaction>>(transactions);
    }
}
