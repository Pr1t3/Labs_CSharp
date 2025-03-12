using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.OutputHandler;
using Itmo.ObjectOrientedProgramming.Lab4.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab4;

public class ProgramManager
{
    private readonly CommandParser _commandParser;

    public ProgramManager(IFileSystemManager fileSystem, IOutputHandlerFactory outputHandlerFactory, string fileSymbol, string directorySymbol, string spaceSymbol)
    {
        _commandParser = new CommandParser(fileSystem, outputHandlerFactory, fileSymbol, directorySymbol, spaceSymbol);
    }

    public ProgramResult Run()
    {
        while (true)
        {
            string? input = Console.ReadLine();
            if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
            {
                return new ProgramResult.Success();
            }

            if (string.IsNullOrWhiteSpace(input))
            {
                continue;
            }

            ICommand command = _commandParser.Parse(input);
            CommandResult result = command.Execute();
            if (result is CommandResult.Fail)
            {
                return new ProgramResult.Fail();
            }
        }
    }
}