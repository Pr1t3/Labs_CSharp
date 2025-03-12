using Itmo.ObjectOrientedProgramming.Lab4.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab4.OutputHandler;

public interface IOutputHandlerFactory
{
    OutputHandlerResult CreateOutputHandler(string mode);
}