namespace Itmo.ObjectOrientedProgramming.Lab4.ResultTypes;

public abstract record ProgramResult
{
    private ProgramResult() { }

    public sealed record Success : ProgramResult;

    public sealed record Fail : ProgramResult;
}