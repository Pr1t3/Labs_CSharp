using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab3.DisplayEntity;
using Itmo.ObjectOrientedProgramming.Lab3.MessageEntity;

namespace Itmo.ObjectOrientedProgramming.Lab3.AddresseeEntity;

public class DisplayAddressee(Display display, ConsoleColor color) : IAddressee
{
    private readonly Display _display = display;
    private readonly ConsoleColor _color = color;

    public AddresseeResult SendMessage(Message message)
    {
        _display.ShowMessage(message.Text, _color);

        return new AddresseeResult.Success();
    }

    public override string? ToString()
    {
        return "Display";
    }
}