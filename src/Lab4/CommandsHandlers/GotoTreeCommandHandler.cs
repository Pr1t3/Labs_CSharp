using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandsHandlers;

public class GotoTreeCommandHandler : CommandHandler
{
    private readonly IFileSystemManager _fileSystem;

    public GotoTreeCommandHandler(IFileSystemManager fileSystem)
    {
        _fileSystem = fileSystem;
    }

    protected override bool CheckCommand(string[] args)
    {
        return args.Length > 1 && string.Equals(args[0], "tree", StringComparison.OrdinalIgnoreCase) && string.Equals(args[1], "goto", StringComparison.OrdinalIgnoreCase);
    }

    protected override ICommand CreateCommand(string[] args)
    {
        return new GotoTreeCommand(_fileSystem, args);
    }
}