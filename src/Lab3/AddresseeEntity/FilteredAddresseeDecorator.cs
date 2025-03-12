using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab3.AddresseeEntity;
using Itmo.ObjectOrientedProgramming.Lab3.MessageEntity;

namespace Itmo.ObjectOrientedProgramming.Lab3.Logger;

public class FilteredAddresseeDecorator(IAddressee decoratee, ImportanceLevel importance) : IAddressee
{
    private readonly IAddressee _decoratee = decoratee;

    private readonly ImportanceLevel _importance = importance;

    public AddresseeResult SendMessage(Message message)
    {
        if (message.Importance > _importance)
        {
            AddresseeResult result = _decoratee.SendMessage(message);
            return result;
        }

        return new AddresseeResult.Fail();
    }
}