namespace Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

public abstract record SubjectResult
{
    private SubjectResult() { }

    public sealed record Success : SubjectResult;

    public sealed record Fail : SubjectResult;
}