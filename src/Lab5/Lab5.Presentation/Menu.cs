#pragma warning disable CA2007
using Lab5.Application.ResultTypes;
using Lab5.Application.Services;
using Lab5.Domain.Entities;

namespace Lab5.Presentation;

public class Menu
{
    private readonly IUserService _userService;
    private readonly IATMService _atmService;

    private UserSession? _currentSession = null;
    private bool _isAdmin = false;

    public Menu(IUserService userService, IATMService atmService)
    {
        _userService = userService;
        _atmService = atmService;
    }

    public async Task StartAsync()
    {
        while (true)
        {
            Console.Clear();
            if (_currentSession == null && !_isAdmin)
            {
                Console.WriteLine("1. Login as user");
                Console.WriteLine("2. Login as admin");
                Console.WriteLine("3. Register");
                Console.WriteLine("4. Exit");
                Console.Write("Select an option: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await LoginAsUserAsync();
                        break;
                    case "2":
                        LoginAsAdminAsync();
                        break;
                    case "3":
                        await RegisterAsync();
                        break;
                    case "4":
                        Environment.Exit(0);
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
            else if (_isAdmin)
            {
                await ShowAdminMenuAsync();
            }
            else
            {
                await ShowUserMenuAsync();
            }
        }
    }

    private async Task LoginAsUserAsync()
    {
        Console.Clear();
        Console.WriteLine("Logging in as user");
        Console.Write("Login: ");
        string? login = Console.ReadLine();
        Console.Write("Pin Code: ");

        Func<ConsoleKeyInfo, string, bool> keyPressFunc = (key, password) => char.IsNumber(key.KeyChar) && password.Length < 4;
        string password = ReadPassword(keyPressFunc);

        try
        {
            if (login != null && password.Length == 4)
            {
                User? user = null;
                ResultT result = await _userService.AuthenticateUserAsync(login, password);
                if (result is ResultT.SuccessWithData<User> userResult)
                {
                    user = userResult.Data;
                }

                if (user == null)
                {
                    Console.WriteLine("Invalid login or password.");
                }
                else
                {
                    _currentSession = new UserSession(user);
                    Console.WriteLine("Login successful.");
                }
            }
            else
            {
                throw new ArgumentException("Invalid login or password.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("Press any key to try again.");
        Console.ReadKey();
    }

    private void LoginAsAdminAsync()
    {
        Console.Clear();
        Console.WriteLine("Loginning in as admin");
        Console.Write("Password: ");

        Func<ConsoleKeyInfo, string, bool> keyPressFunc = (key, password) => !char.IsControl(key.KeyChar);
        string password = ReadPassword(keyPressFunc);

        if (password == "adminPassword")
        {
            _isAdmin = true;
            Console.WriteLine($"Login successful as admin.");
        }
        else
        {
            Console.WriteLine("Invalid login or password.");
        }

        Console.WriteLine("Press any key to try again.");
        Console.ReadKey();
    }

    private async Task RegisterAsync()
    {
        Console.Clear();
        Console.WriteLine("Register new account");
        Console.Write("Login: ");
        string? login = Console.ReadLine();
        Console.Write("Pin Code: ");

        Func<ConsoleKeyInfo, string, bool> keyPressFunc = (key, password) => char.IsNumber(key.KeyChar) && password.Length < 4;
        string password = ReadPassword(keyPressFunc);
        if (login != null && password.Length == 4)
        {
            ResultT result = await _userService.CreateUserAsync(login, password);
            if (result is ResultT.Fail)
            {
                Console.WriteLine("Registration failed.");
            }
            else
            {
                Console.WriteLine("Registration successful.");
            }
        }
        else
        {
            Console.WriteLine("Registration failed.");
        }

        Console.WriteLine("Press any key to try again.");
        Console.ReadKey();
    }

    private async Task ShowUserMenuAsync()
    {
        if (_currentSession == null)
        {
            return;
        }

        Console.Clear();
        Console.WriteLine($"Welcome, {_currentSession.User.Login}");
        Console.WriteLine("1. Show Balance");
        Console.WriteLine("2. Withdraw Money");
        Console.WriteLine("3. Deposit Money");
        Console.WriteLine("4. Check Operation History");
        Console.WriteLine("5. Logout");

        Console.Write("Select an option: ");
        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                await ShowBalanceAsync();
                break;
            case "2":
                await WithdrawMoneyAsync();
                break;
            case "3":
                await DepositMoneyAsync();
                break;
            case "4":
                await CheckOperationHistoryAsync();
                break;
            case "5":
                Logout();
                break;
            default:
                Console.WriteLine("Invalid option. Press any key to try again.");
                Console.ReadKey();
                break;
        }
    }

    private async Task ShowAdminMenuAsync()
    {
        Console.Clear();
        Console.WriteLine("Welcome, Admin");
        Console.WriteLine("1. Delete Account");
        Console.WriteLine("2. Logout");

        Console.Write("Select an option: ");
        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                await DeleteAccount();
                break;
            case "2":
                Logout();
                break;
            default:
                Console.WriteLine("Invalid option. Press any key to try again.");
                Console.ReadKey();
                break;
        }
    }

    private async Task DeleteAccount()
    {
        Console.Clear();
        Console.Write("Enter account login to delete: ");
        string? accountLogin = Console.ReadLine();

        if (accountLogin != null)
        {
            ResultT result = await _userService.DeleteUserAsync(accountLogin);
            if (result is ResultT.Fail)
            {
                Console.WriteLine("Account deletion failed.");
            }
            else
            {
                Console.WriteLine("Account deleted successfully.");
            }
        }
        else
        {
            Console.WriteLine("Account deletion failed.");
        }

        Console.WriteLine("Press any key to try again.");
        Console.ReadKey();
    }

    private async Task ShowBalanceAsync()
    {
        if (_currentSession == null)
        {
            return;
        }

        Console.Clear();
        ResultT result = await _atmService.ShowBalanceAsync(_currentSession.User.Id);
        if (result is ResultT.SuccessWithData<decimal> balanceResult)
        {
            Console.WriteLine($"Your current balance is: {balanceResult.Data:C}");
        }
        else
        {
            Console.WriteLine("Failed to show balance.");
        }

        Console.WriteLine("Press any key to return to the menu.");
        Console.ReadKey();
    }

    private async Task WithdrawMoneyAsync()
    {
        if (_currentSession == null)
        {
            return;
        }

        Console.Clear();
        Console.Write("Enter amount to withdraw: ");
        string? input = Console.ReadLine();
        if (decimal.TryParse(input, out decimal amount))
        {
            ResultT result = await _atmService.WithdrawMoneyAsync(_currentSession.User.Id, amount);
            if (result is ResultT.Fail)
            {
                Console.WriteLine("Failed to withdraw money.");
            }
            else
            {
                Console.WriteLine("Withdrawal successful.");
            }
        }
        else
        {
            Console.WriteLine("Invalid amount.");
        }

        Console.WriteLine("Press any key to return to the menu.");
        Console.ReadKey();
    }

    private async Task DepositMoneyAsync()
    {
        if (_currentSession == null)
        {
            return;
        }

        Console.Clear();
        Console.Write("Enter amount to deposit: ");
        string? input = Console.ReadLine();
        if (decimal.TryParse(input, out decimal amount))
        {
            ResultT result = await _atmService.DepositMoneyAsync(_currentSession.User.Id, amount);
            if (result is ResultT.Fail)
            {
                Console.WriteLine("Failed to deposit money.");
            }
            else
            {
                Console.WriteLine("Deposit successful.");
            }
        }
        else
        {
            Console.WriteLine("Invalid amount.");
        }

        Console.WriteLine("Press any key to return to the menu.");
        Console.ReadKey();
    }

    private async Task CheckOperationHistoryAsync()
    {
        if (_currentSession == null)
        {
            return;
        }

        Console.Clear();

        ResultT result = await _atmService.CheckOperationHistoryAsync(_currentSession.User.Id);
        if (result is ResultT.SuccessWithData<IEnumerable<Transaction>> transactionResult)
        {
            IEnumerable<Transaction> transactions = transactionResult.Data;
            foreach (Transaction txn in transactions)
            {
                Console.WriteLine($"{txn.Timestamp}: {txn.Type} of {txn.Amount:C}");
            }
        }
        else
        {
            Console.WriteLine("Failed to check operation history.");
        }

        Console.WriteLine("Press any key to return to the menu.");
        Console.ReadKey();
    }

    private void Logout()
    {
        _currentSession = null;
        _isAdmin = false;
        Console.WriteLine("Logged out successfully.");
        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }

    private string ReadPassword(Func<ConsoleKeyInfo, string, bool> keyPressFunc)
    {
        string password = string.Empty;
        ConsoleKeyInfo keyInfo;
        do
        {
            keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password.Remove(password.Length - 1);
                Console.Write("\b \b");
            }
            else if (keyPressFunc(keyInfo, password))
            {
                password += keyInfo.KeyChar;
                Console.Write("*");
            }
        }
        while (keyInfo.Key != ConsoleKey.Enter);
        Console.WriteLine();
        return password;
    }

    private class UserSession
    {
        public User User { get; private set; }

        public UserSession(User user)
        {
            User = user;
        }
    }
}
