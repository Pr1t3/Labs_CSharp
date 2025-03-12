namespace Itmo.ObjectOrientedProgramming.Lab4.ResultTypes;

public abstract record FileSystemResult
{
    private FileSystemResult() { }

    public sealed record Success : FileSystemResult;

    public sealed record SuccessWithResult<T>(T Result) : FileSystemResult;

    public sealed record Fail : FileSystemResult;
}