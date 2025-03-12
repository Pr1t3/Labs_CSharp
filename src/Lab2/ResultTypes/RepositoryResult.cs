namespace Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

public abstract record RepositoryResult
{
    private RepositoryResult() { }

    public sealed record Success : RepositoryResult;

    public sealed record Success<T>(T Item) : RepositoryResult where T : IEntity;

    public sealed record Fail : RepositoryResult;
}