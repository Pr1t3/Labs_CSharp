using Itmo.ObjectOrientedProgramming.Lab3.MessageEntity;

namespace Itmo.ObjectOrientedProgramming.Lab3.Logger;

public class ConsoleLogger : ILogger
{
    public void Log(string addressee, Message message)
    {
        Console.WriteLine(DateTime.Now + ": " + addressee + " addressee got new message: " + message.Title + "\n" + message.Text);
    }
}