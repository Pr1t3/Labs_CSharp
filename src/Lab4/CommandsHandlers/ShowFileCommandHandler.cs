using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.OutputHandler;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandsHandlers;

public class ShowFileCommandHandler : CommandHandler
{
    private readonly IFileSystemManager _fileSystem;
    private readonly IOutputHandlerFactory _outputHandlerFactory;

    public ShowFileCommandHandler(IFileSystemManager fileSystem, IOutputHandlerFactory outputHandlerFactory)
    {
        _fileSystem = fileSystem;
        _outputHandlerFactory = outputHandlerFactory;
    }

    protected override bool CheckCommand(string[] args)
    {
        return args.Length > 1 && string.Equals(args[0], "file", StringComparison.OrdinalIgnoreCase) && string.Equals(args[1], "show", StringComparison.OrdinalIgnoreCase);
    }

    protected override ICommand CreateCommand(string[] args)
    {
        return new ShowFileCommand(_fileSystem, _outputHandlerFactory, args);
    }
}