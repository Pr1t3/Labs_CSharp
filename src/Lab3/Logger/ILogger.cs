using Itmo.ObjectOrientedProgramming.Lab3.MessageEntity;

namespace Itmo.ObjectOrientedProgramming.Lab3.Logger;

public interface ILogger
{
    public void Log(string addressee, Message message);
}