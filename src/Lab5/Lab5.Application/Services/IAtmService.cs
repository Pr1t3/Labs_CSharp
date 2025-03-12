using Lab5.Application.ResultTypes;

namespace Lab5.Application.Services;

public interface IATMService
{
    Task<ResultT> ShowBalanceAsync(Guid userId);

    Task<ResultT> WithdrawMoneyAsync(Guid userId, decimal amount);

    Task<ResultT> DepositMoneyAsync(Guid userId, decimal amount);

    Task<ResultT> CheckOperationHistoryAsync(Guid userId);
}
