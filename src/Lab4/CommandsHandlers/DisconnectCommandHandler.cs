using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandsHandlers;

public class DisconnectCommandHandler : CommandHandler
{
    private readonly IFileSystemManager _fileSystem;

    public DisconnectCommandHandler(IFileSystemManager fileSystem)
    {
        _fileSystem = fileSystem;
    }

    protected override bool CheckCommand(string[] args)
    {
        return args.Length > 0 && string.Equals(args[0], "disconnect", StringComparison.OrdinalIgnoreCase);
    }

    protected override ICommand CreateCommand(string[] args)
    {
        return new DisconnectCommand(_fileSystem);
    }
}