using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab3.MessageEntity;

namespace Itmo.ObjectOrientedProgramming.Lab3.AddresseeEntity;

public class GroupAddressee : IAddressee
{
    private readonly IReadOnlyCollection<IAddressee> _addressees;

    public GroupAddressee(IReadOnlyCollection<IAddressee> addressees)
    {
        if (addressees.Any(m => m == null))
        {
            throw new ArgumentException("Addresses can not be null", nameof(addressees));
        }

        _addressees = addressees;
    }

    public AddresseeResult SendMessage(Message message)
    {
        foreach (IAddressee addressee in _addressees)
        {
            AddresseeResult result = addressee.SendMessage(message);
        }

        return new AddresseeResult.Success();
    }

    public override string? ToString()
    {
        return "Group";
    }
}