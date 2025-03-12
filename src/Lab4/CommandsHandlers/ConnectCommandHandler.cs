using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandsHandlers;

public class ConnectCommandHandler : CommandHandler
{
    private readonly IFileSystemManager _fileSystem;

    public ConnectCommandHandler(IFileSystemManager fileSystem)
    {
        _fileSystem = fileSystem;
    }

    protected override bool CheckCommand(string[] args)
    {
        return args.Length > 0 && string.Equals(args[0], "connect", StringComparison.OrdinalIgnoreCase);
    }

    protected override ICommand CreateCommand(string[] args)
    {
        return new ConnectCommand(_fileSystem, args);
    }
}