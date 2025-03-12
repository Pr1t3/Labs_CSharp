using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.OutputHandler;
using Xunit;

namespace Lab4.Tests;

public class Lab4Tests
{
    [Fact]
    public void Scenario1()
    {
        // Arrange
        var fileSystem = new FileSystemManager();
        var outputHandlerFactory = new ConsoleOutputHandlerFactory();
        var parser = new CommandParser(fileSystem, outputHandlerFactory, "📄", "📁", " ");

        // Act
        ICommand command1 = parser.Parse("connect C:\\ -m local");
        ICommand command2 = parser.Parse("disconnect");
        ICommand command3 = parser.Parse("tree list -d 2");
        ICommand command4 = parser.Parse("tree goto Documents");
        ICommand command5 = parser.Parse("file delete C:\\file.txt");

        // Assert
        Assert.IsType<ConnectCommand>(command1);
        Assert.IsType<DisconnectCommand>(command2);
        Assert.IsType<ListTreeCommand>(command3);
        Assert.IsType<GotoTreeCommand>(command4);
        Assert.IsType<DeleteFileCommand>(command5);
    }
}