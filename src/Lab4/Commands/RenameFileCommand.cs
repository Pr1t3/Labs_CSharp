using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class RenameFileCommand : ICommand
{
    private readonly IFileSystemManager _fileSystem;
    private readonly string _path;
    private readonly string _newName;

    public RenameFileCommand(IFileSystemManager fileSystem, string[] args)
    {
        if (args.Length != 4)
        {
            throw new ArgumentException("Invalid number of arguments");
        }

        _fileSystem = fileSystem;
        _path = args[2];
        _newName = args[3];
    }

    public CommandResult Execute()
    {
        FileSystemResult result = _fileSystem.RenameFile(_path, _newName);
        return result is FileSystemResult.Success ? new CommandResult.Success() : new CommandResult.Fail();
    }
}
