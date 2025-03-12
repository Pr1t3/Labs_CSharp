using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab3.MessageEntity;

namespace Itmo.ObjectOrientedProgramming.Lab3.UserEntity;

public class User
{
    public string Name { get; }

    private readonly List<UserMessage> _userMessages = [];

    public User(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name can not be null, empty or contain only spaces", nameof(name));
        }

        Name = name;
    }

    public void GetMessage(Message message)
    {
        _userMessages.Add(new UserMessage(message));
    }

    public UserResult MarkAsRead(Message message)
    {
        UserMessage? userMessage = _userMessages.FirstOrDefault(mes => mes.Message == message);
        if (userMessage == null)
        {
            return new UserResult.Fail();
        }

        UserResult result = userMessage.MarkAsRead();
        return result;
    }

    public UserResult GetMessageStatus(Message message)
    {
        UserMessage? userMessage = _userMessages.FirstOrDefault(mes => mes.Message == message);
        if (userMessage == null)
        {
            return new UserResult.Fail();
        }

        bool result = userMessage.GetStatus();
        return new UserResult.GetStatusSuccess(result);
    }

    private class UserMessage
    {
        public bool IsRead { get; private set; } = false;

        public Message Message { get; }

        public UserMessage(Message message)
        {
            Message = message;
        }

        public UserResult MarkAsRead()
        {
            if (IsRead)
            {
                return new UserResult.MarkFail();
            }

            IsRead = true;
            return new UserResult.Success();
        }

        public bool GetStatus()
        {
            return IsRead;
        }
    }
}