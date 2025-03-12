using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class DisconnectCommand : ICommand
{
    private readonly IFileSystemManager _fileSystem;

    public DisconnectCommand(IFileSystemManager fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public CommandResult Execute()
    {
        FileSystemResult result = _fileSystem.Disconnect();
        return result is FileSystemResult.Success ? new CommandResult.Success() : new CommandResult.Fail();
    }
}
