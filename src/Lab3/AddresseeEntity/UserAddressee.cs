using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab3.MessageEntity;
using Itmo.ObjectOrientedProgramming.Lab3.UserEntity;

namespace Itmo.ObjectOrientedProgramming.Lab3.AddresseeEntity;

public class UserAddressee(User user) : IAddressee
{
    private readonly User _user = user;

    public AddresseeResult SendMessage(Message message)
    {
        _user.GetMessage(message);

        return new AddresseeResult.Success();
    }

    public override string? ToString()
    {
        return "User";
    }
}