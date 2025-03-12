#pragma warning disable CA2007
using Lab5.Application.Interfaces;
using Lab5.Application.Operations;
using Lab5.Application.Services;
using Lab5.Infrastructure.Data;
using Lab5.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Lab5.Presentation;

internal class Program
{
    private static async Task Main(string[] args)
    {
        string connectionString = "Host=localhost;Port=54321;Database=atmdb;Username=postgres;Password=postgres";
        ServiceProvider serviceProvider = new ServiceCollection()
            .AddSingleton<IUserRepository>(provider => new UserRepository(connectionString))
            .AddSingleton<ITransactionRepository>(provider => new TransactionRepository(connectionString))
            .AddSingleton<IPasswordHasher, PasswordHasher>()
            .AddSingleton<CreateUserAccount>()
            .AddSingleton<ShowBalance>()
            .AddSingleton<WithdrawMoney>()
            .AddSingleton<DepositMoney>()
            .AddSingleton<CheckOperationHistory>()
            .AddSingleton<DeleteUserAccount>()
            .AddSingleton<IUserService, UserService>()
            .AddSingleton<IATMService, ATMService>()
            .AddSingleton<Menu>()
            .BuildServiceProvider();

        Menu? menu = serviceProvider.GetService<Menu>();
        if (menu != null)
        {
            await menu.StartAsync();
        }
    }
}
