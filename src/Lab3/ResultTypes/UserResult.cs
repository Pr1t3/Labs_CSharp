namespace Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

public abstract record UserResult
{
    private UserResult() { }

    public sealed record Success : UserResult;

    public sealed record MarkFail : UserResult;

    public sealed record Fail : UserResult;

    public sealed record GetStatusSuccess(bool Status) : UserResult;
}