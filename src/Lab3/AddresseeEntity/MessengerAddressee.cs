using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab3.MessageEntity;
using Itmo.ObjectOrientedProgramming.Lab3.MessengerEntity;

namespace Itmo.ObjectOrientedProgramming.Lab3.AddresseeEntity;

public class MessengerAddressee(Messenger messenger) : IAddressee
{
    private readonly Messenger _messenger = messenger;

    public AddresseeResult SendMessage(Message message)
    {
        _messenger.DisplayMessage(message.Text);

        return new AddresseeResult.Success();
    }

    public override string? ToString()
    {
        return "Messenger";
    }
}