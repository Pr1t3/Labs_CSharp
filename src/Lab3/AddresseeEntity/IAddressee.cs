using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab3.MessageEntity;

namespace Itmo.ObjectOrientedProgramming.Lab3.AddresseeEntity;

public interface IAddressee
{
    AddresseeResult SendMessage(Message message);
}