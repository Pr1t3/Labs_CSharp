#pragma warning disable CA2007
using Lab5.Application.Operations;
using Lab5.Application.ResultTypes;

namespace Lab5.Application.Services;

public class ATMService : IATMService
{
    private readonly ShowBalance _showBalance;
    private readonly WithdrawMoney _withdrawMoney;
    private readonly DepositMoney _depositMoney;
    private readonly CheckOperationHistory _checkOperationHistory;

    public ATMService(
        ShowBalance showBalance,
        WithdrawMoney withdrawMoney,
        DepositMoney depositMoney,
        CheckOperationHistory checkOperationHistory)
    {
        _showBalance = showBalance;
        _withdrawMoney = withdrawMoney;
        _depositMoney = depositMoney;
        _checkOperationHistory = checkOperationHistory;
    }

    public async Task<ResultT> ShowBalanceAsync(Guid userId)
    {
        return await _showBalance.ExecuteAsync(userId);
    }

    public async Task<ResultT> WithdrawMoneyAsync(Guid userId, decimal amount)
    {
        return await _withdrawMoney.ExecuteAsync(userId, amount);
    }

    public async Task<ResultT> DepositMoneyAsync(Guid userId, decimal amount)
    {
        return await _depositMoney.ExecuteAsync(userId, amount);
    }

    public async Task<ResultT> CheckOperationHistoryAsync(Guid userId)
    {
        return await _checkOperationHistory.ExecuteAsync(userId);
    }
}
