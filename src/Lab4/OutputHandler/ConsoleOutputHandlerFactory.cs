using Itmo.ObjectOrientedProgramming.Lab4.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab4.OutputHandler;

public class ConsoleOutputHandlerFactory : IOutputHandlerFactory
{
    public OutputHandlerResult CreateOutputHandler(string mode)
    {
        if (mode == "console")
        {
            return new OutputHandlerResult.SuccessWithResult<IOutputHandler>(new ConsoleOutputHandler());
        }

        return new OutputHandlerResult.Fail();
    }
}