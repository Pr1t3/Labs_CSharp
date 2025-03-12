namespace Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

public abstract record LabResult
{
    private LabResult() { }

    public sealed record Success : LabResult;

    public sealed record Fail : LabResult;
}