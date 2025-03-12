namespace Lab5.Domain.ResultTypes;

public abstract record UserResult
{
    private UserResult() { }

    public sealed record Success : UserResult;

    public sealed record Fail : UserResult;
}