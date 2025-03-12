using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandsHandlers;

public class CopyFileCommandHandler : CommandHandler
{
    private readonly IFileSystemManager _fileSystem;

    public CopyFileCommandHandler(IFileSystemManager fileSystem)
    {
        _fileSystem = fileSystem;
    }

    protected override bool CheckCommand(string[] args)
    {
        return args.Length > 1 && string.Equals(args[0], "file", StringComparison.OrdinalIgnoreCase) && string.Equals(args[1], "copy", StringComparison.OrdinalIgnoreCase);
    }

    protected override ICommand CreateCommand(string[] args)
    {
        return new CopyFileCommand(_fileSystem, args);
    }
}