using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.OutputHandler;
using Itmo.ObjectOrientedProgramming.Lab4.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class ShowFileCommand : ICommand
{
    private readonly IFileSystemManager _fileSystem;
    private readonly string _path;
    private readonly IOutputHandler _outputHandler;

    public ShowFileCommand(IFileSystemManager fileSystem, IOutputHandlerFactory outputHandlerFactory, string[] args)
    {
        if (args.Length != 3 && args.Length != 5)
        {
            throw new ArgumentException("Invalid number of arguments");
        }

        _fileSystem = fileSystem;
        _path = args[2];
        string mode = "console";
        if (args.Length == 5)
        {
            if (args[3] == "-m")
            {
                mode = args[4];
            }
            else
            {
                throw new ArgumentException("Unknown option");
            }
        }

        OutputHandlerResult result = outputHandlerFactory.CreateOutputHandler(mode);
        if (result is OutputHandlerResult.SuccessWithResult<IOutputHandler> success)
        {
            _outputHandler = success.Result;
        }
        else
        {
            throw new InvalidOperationException("Invalid output mode");
        }
    }

    public CommandResult Execute()
    {
        FileSystemResult result = _fileSystem.GetFileContent(_path);
        if (result is FileSystemResult.SuccessWithResult<string> success)
        {
            _outputHandler.Write(success.Result);
            return new CommandResult.Success();
        }

        return result is FileSystemResult.Success ? new CommandResult.Success() : new CommandResult.Fail();
    }
}

internal interface IOutputHandlerResult
{
}