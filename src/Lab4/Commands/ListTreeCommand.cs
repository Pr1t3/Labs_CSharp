// TreeListCommand.cs (обновлено)
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem.FileSystemElements;
using Itmo.ObjectOrientedProgramming.Lab4.OutputHandler;
using Itmo.ObjectOrientedProgramming.Lab4.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class ListTreeCommand : ICommand
{
    private readonly IFileSystemManager _fileSystem;
    private readonly int _depth = 1;
    private readonly IOutputHandler _outputHandler;
    private readonly string _fileSymbol;
    private readonly string _directorySymbol;
    private readonly string _spaceSymbol;

    public ListTreeCommand(IFileSystemManager fileSystem, IOutputHandlerFactory outputHandlerFactory, string fileSymbol, string directorySymbol, string spaceSymbol, string[] args)
    {
        if (args.Length != 2 && args.Length != 4 && args.Length != 6)
        {
            throw new ArgumentException("Invalid number of arguments");
        }

        _fileSystem = fileSystem;
        string outputMode = "console";

        if (args.Length == 4)
        {
            if (args[2] == "-d" && int.TryParse(args[3], out int depth))
            {
                _depth = depth;
            }
            else if (args[2] == "-m")
            {
                outputMode = args[3];
            }
            else
            {
                throw new ArgumentException("Invalid argument");
            }
        }
        else if (args.Length == 6)
        {
            for (int i = 2; i < args.Length; i++)
            {
                if (args[i] == "-d" && int.TryParse(args[i + 1], out int depth))
                {
                    _depth = depth;
                }
                else if (args[i] == "-m")
                {
                    outputMode = args[i + 1];
                }
                else if (i % 2 == 0)
                {
                    throw new ArgumentException("Invalid argument");
                }
            }
        }

        OutputHandlerResult result = outputHandlerFactory.CreateOutputHandler(outputMode);
        if (result is OutputHandlerResult.SuccessWithResult<IOutputHandler> success)
        {
            _outputHandler = success.Result;
        }
        else
        {
            throw new InvalidOperationException("Invalid output mode");
        }

        _fileSymbol = fileSymbol;
        _directorySymbol = directorySymbol;
        _spaceSymbol = spaceSymbol;
    }

    public CommandResult Execute()
    {
        FileSystemResult result = _fileSystem.GetDirectory(_depth);
        if (result is FileSystemResult.SuccessWithResult<DirectoryElement> success)
        {
            DirectoryElement curDirectory = success.Result;
            var visitor = new TreeListVisitor(_depth, _outputHandler, _fileSymbol, _directorySymbol, _spaceSymbol);
            curDirectory.Accept(visitor);
            return new CommandResult.Success();
        }

        return result is FileSystemResult.Success ? new CommandResult.Success() : new CommandResult.Fail();
    }
}