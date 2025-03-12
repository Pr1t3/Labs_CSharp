// TreeListCommand.cs (обновлено)
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class GotoTreeCommand : ICommand
{
    private readonly IFileSystemManager _fileSystem;
    private readonly string _path;

    public GotoTreeCommand(IFileSystemManager fileSystem, string[] args)
    {
        if (args.Length != 3)
        {
            throw new ArgumentException("Invalid number of arguments");
        }

        _fileSystem = fileSystem;
        _path = args[2];
    }

    public CommandResult Execute()
    {
        FileSystemResult result = _fileSystem.GotoDirectory(_path);
        return result is FileSystemResult.Success ? new CommandResult.Success() : new CommandResult.Fail();
    }
}