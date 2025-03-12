namespace Itmo.ObjectOrientedProgramming.Lab4.ResultTypes;

public abstract record CommandResult
{
    private CommandResult() { }

    public sealed record Success : CommandResult;

    public sealed record Fail : CommandResult;
}