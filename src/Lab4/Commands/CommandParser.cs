using Itmo.ObjectOrientedProgramming.Lab4.CommandsHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.OutputHandler;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class CommandParser
{
    private readonly CommandHandler _firstHandler;

    public CommandParser(IFileSystemManager fileSystem, IOutputHandlerFactory outputHandlerFactory, string fileSymbol, string directorySymbol, string spaceSymbol)
    {
        var connectHandler = new ConnectCommandHandler(fileSystem);
        var disconnectHandler = new DisconnectCommandHandler(fileSystem);
        var listTreeHandler = new ListTreeCommandHandler(fileSystem, outputHandlerFactory, fileSymbol, directorySymbol, spaceSymbol);
        var gotoTreeHandler = new GotoTreeCommandHandler(fileSystem);
        var showFileHandler = new ShowFileCommandHandler(fileSystem, outputHandlerFactory);
        var moveFileHandler = new MoveFileCommandHandler(fileSystem);
        var copyFileHandler = new CopyFileCommandHandler(fileSystem);
        var deleteFileHandler = new DeleteFileCommandHandler(fileSystem);
        var renameFileHandler = new RenameFileCommandHandler(fileSystem);

        connectHandler.SetNext(disconnectHandler);
        disconnectHandler.SetNext(listTreeHandler);
        listTreeHandler.SetNext(gotoTreeHandler);
        gotoTreeHandler.SetNext(showFileHandler);
        showFileHandler.SetNext(moveFileHandler);
        moveFileHandler.SetNext(copyFileHandler);
        copyFileHandler.SetNext(deleteFileHandler);
        deleteFileHandler.SetNext(renameFileHandler);

        _firstHandler = connectHandler;
    }

    public ICommand Parse(string input)
    {
        string[] args = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (args.Length == 0)
            throw new InvalidOperationException("No command entered.");

        return _firstHandler.Handle(args);
    }
}