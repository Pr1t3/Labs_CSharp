using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class ConnectCommand : ICommand
{
    private readonly IFileSystemManager _fileSystem;
    private readonly string _path;
    private readonly string _mode;

    public ConnectCommand(IFileSystemManager fileSystem, string[] args)
    {
        _fileSystem = fileSystem;
        _path = args[1];
        if (args.Length == 4)
        {
            if (args[2] == "-m")
            {
                _mode = args[3];
            }
            else
            {
                throw new ArgumentException("You must specify mode");
            }
        }
        else
        {
            throw new ArgumentException("You must specify mode");
        }
    }

    public CommandResult Execute()
    {
        FileSystemResult result = _fileSystem.Connect(_path, _mode);
        return result is FileSystemResult.Success ? new CommandResult.Success() : new CommandResult.Fail();
    }
}
