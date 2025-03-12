namespace Itmo.ObjectOrientedProgramming.Lab4.OutputHandler;

public class ConsoleOutputHandler : IOutputHandler
{
    public void Write(string content)
    {
        Console.WriteLine(content);
    }
}