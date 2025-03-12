using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab3.AddresseeEntity;
using Itmo.ObjectOrientedProgramming.Lab3.Logger;
using Itmo.ObjectOrientedProgramming.Lab3.MessageEntity;
using Itmo.ObjectOrientedProgramming.Lab3.MessengerEntity;
using Itmo.ObjectOrientedProgramming.Lab3.TopicEntity;
using Itmo.ObjectOrientedProgramming.Lab3.UserEntity;
using Moq;
using Xunit;

namespace Lab3.Tests;

public class Lab3Test
{
    [Fact]
    public void Scenario1()
    {
        // Arrange
        var user = new User("Person 1");
        var userAddressee = new UserAddressee(user);
        var topic = new Topic("topic 1");
        var message = new Message("Message 1", "param param pam pam", ImportanceLevel.Low);

        // Act
        topic.AddAddressee(userAddressee);
        topic.SendMessage(message);

        // Assert
        UserResult result = user.GetMessageStatus(message);
        if (result is UserResult.GetStatusSuccess success)
        {
            Assert.False(success.Status);
        }
    }

    [Fact]
    public void Scenario2()
    {
        // Arrange
        var user = new User("Person 1");
        var userAddressee = new UserAddressee(user);
        var topic = new Topic("topic 1");
        var message = new Message("Message 1", "param param pam pam", ImportanceLevel.Low);

        // Act
        topic.AddAddressee(userAddressee);
        topic.SendMessage(message);
        user.MarkAsRead(message);

        // Assert
        UserResult result = user.GetMessageStatus(message);
        if (result is UserResult.GetStatusSuccess success)
        {
            // Assert
            Assert.True(success.Status);
        }
    }

    [Fact]
    public void Scenario3()
    {
        // Arrange
        var user = new User("Person 1");
        var userAddressee = new UserAddressee(user);
        var topic = new Topic("Topic 1");
        var message = new Message("Message 1", "param param pam pam", ImportanceLevel.Low);

        // Act
        topic.AddAddressee(userAddressee);
        topic.SendMessage(message);
        user.MarkAsRead(message);

        // Assert
        UserResult result = user.MarkAsRead(message);
        Assert.IsType<UserResult.MarkFail>(result);
    }

    [Fact]
    public void Scenario4()
    {
        // Arrange
        var user = new User("Person 1");
        var addressee = new FilteredAddresseeDecorator(new UserAddressee(user), ImportanceLevel.High);
        var mockLogger = new Mock<ILogger>();
        var loggingDecorator = new LoggingAddresseeDecorator(addressee, mockLogger.Object);

        var message = new Message("Message 1", "param param pam pam", ImportanceLevel.Low);

        // Act
        loggingDecorator.SendMessage(message);

        // Assert
        mockLogger.Verify(logger => logger.Log("User", It.IsAny<Message>()), Times.Never);
    }

    [Fact]
    public void Scenario5()
    {
        // Arrange
        var user = new User("Person 1");
        var addressee = new UserAddressee(user);
        var mockLogger = new Mock<ILogger>();
        var loggingDecorator = new LoggingAddresseeDecorator(addressee, mockLogger.Object);

        var message = new Message("Message 1", "param param pam pam", ImportanceLevel.Low);

        // Act
        loggingDecorator.SendMessage(message);

        // Assert
        mockLogger.Verify(logger => logger.Log("User", message), Times.Once);
    }

    [Fact]
    public void Scenario6()
    {
        // Arrange
        var consoleLogger = new ConsoleLogger();
        var messenger = new Messenger();
        var addressee = new MessengerAddressee(messenger);
        var loggingDecorator = new LoggingAddresseeDecorator(addressee, consoleLogger);

        var message = new Message("Message 1", "param param pam pam", ImportanceLevel.Medium);

        using var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);

        // Act
        loggingDecorator.SendMessage(message);

        // Assert
        string output = consoleOutput.ToString();

        Assert.Equal("Мессенджер: " + message.Text + "\n" + DateTime.Now + ": " + "Messenger addressee got new message: " + message.Title + "\n" + message.Text + '\n', output);
    }

    [Fact]
    public void Scenario7()
    {
        // Arrange
        var user = new User("Person 1");
        var addressee1 = new UserAddressee(user);
        var addressee2 = new FilteredAddresseeDecorator(new UserAddressee(user), ImportanceLevel.High);
        var mockLogger1 = new Mock<ILogger>();
        var mockLogger2 = new Mock<ILogger>();
        var loggingDecorator1 = new LoggingAddresseeDecorator(addressee1, mockLogger1.Object);
        var loggingDecorator2 = new LoggingAddresseeDecorator(addressee2, mockLogger2.Object);

        var message = new Message("Message 1", "param param pam pam", ImportanceLevel.Medium);

        // Act
        loggingDecorator1.SendMessage(message);
        loggingDecorator2.SendMessage(message);

        // Assert
        mockLogger1.Verify(logger => logger.Log("User", message), Times.Once);
        mockLogger2.Verify(logger => logger.Log("User", message), Times.Never);
    }
}
