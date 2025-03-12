namespace Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

public abstract record EntitiesFactoryResult
{
    private EntitiesFactoryResult() { }

    public sealed record Success : EntitiesFactoryResult;

    public sealed record Success<T>(T Item) : EntitiesFactoryResult where T : IEntity;

    public sealed record Fail : EntitiesFactoryResult;
}