using Itmo.ObjectOrientedProgramming.Lab3.AddresseeEntity;
using Itmo.ObjectOrientedProgramming.Lab3.MessageEntity;

namespace Itmo.ObjectOrientedProgramming.Lab3.TopicEntity;

public class Topic
{
    private readonly List<IAddressee> _addressees = [];

    public string Name { get; }

    public Topic(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name can not be null, empty or contain only spaces", nameof(name));
        }

        Name = name;
    }

    public void AddAddressee(IAddressee addressee)
    {
        _addressees.Add(addressee);
    }

    public void RemoveAddressee(IAddressee addressee)
    {
        _addressees.Remove(addressee);
    }

    public void SendMessage(Message message)
    {
        foreach (IAddressee addressee in _addressees)
        {
            addressee.SendMessage(message);
        }
    }
}