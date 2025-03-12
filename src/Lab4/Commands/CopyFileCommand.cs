using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class CopyFileCommand : ICommand
{
    private readonly IFileSystemManager _fileSystem;
    private readonly string _sourcePath;
    private readonly string _destinationPath;

    public CopyFileCommand(IFileSystemManager fileSystem, string[] args)
    {
        if (args.Length != 4)
        {
            throw new ArgumentException("Invalid number of arguments");
        }

        _fileSystem = fileSystem;
        _sourcePath = args[2];
        _destinationPath = args[3];
    }

    public CommandResult Execute()
    {
        FileSystemResult result = _fileSystem.CopyFile(_sourcePath, _destinationPath);
        return result is FileSystemResult.Success ? new CommandResult.Success() : new CommandResult.Fail();
    }
}
