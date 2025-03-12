using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab3.AddresseeEntity;
using Itmo.ObjectOrientedProgramming.Lab3.MessageEntity;

namespace Itmo.ObjectOrientedProgramming.Lab3.Logger;

public class LoggingAddresseeDecorator(IAddressee decoratee, ILogger logger) : IAddressee
{
    private readonly IAddressee _decoratee = decoratee;
    private readonly ILogger _logger = logger;
    private readonly string addresseeType = decoratee.ToString() ?? "Decoratee";

    public AddresseeResult SendMessage(Message message)
    {
        AddresseeResult result = _decoratee.SendMessage(message);
        if (result is AddresseeResult.Success)
        {
            _logger.Log(addresseeType, message);
        }

        return result;
    }
}