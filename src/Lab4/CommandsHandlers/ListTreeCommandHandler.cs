using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.OutputHandler;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandsHandlers;

public class ListTreeCommandHandler : CommandHandler
{
    private readonly IFileSystemManager _fileSystem;
    private readonly IOutputHandlerFactory _outputHandlerFactory;
    private readonly string _fileSymbol;
    private readonly string _directorySymbol;
    private readonly string _spaceSymbol;

    public ListTreeCommandHandler(IFileSystemManager fileSystem, IOutputHandlerFactory outputHandlerFactory, string fileSymbol, string directorySymbol, string spaceSymbol)
    {
        _fileSystem = fileSystem;
        _outputHandlerFactory = outputHandlerFactory;
        _fileSymbol = fileSymbol;
        _directorySymbol = directorySymbol;
        _spaceSymbol = spaceSymbol;
    }

    protected override bool CheckCommand(string[] args)
    {
        return args.Length > 1 && string.Equals(args[0], "tree", StringComparison.OrdinalIgnoreCase) && string.Equals(args[1], "list", StringComparison.OrdinalIgnoreCase);
    }

    protected override ICommand CreateCommand(string[] args)
    {
        return new ListTreeCommand(_fileSystem, _outputHandlerFactory,  _fileSymbol, _directorySymbol, _spaceSymbol, args);
    }
}