namespace Itmo.ObjectOrientedProgramming.Lab4.ResultTypes;

public abstract record OutputHandlerResult
{
    private OutputHandlerResult() { }

    public sealed record SuccessWithResult<T>(T Result) : OutputHandlerResult;

    public sealed record Fail : OutputHandlerResult;
}