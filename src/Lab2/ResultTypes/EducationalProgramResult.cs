namespace Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

public abstract record EducationalProgramResult
{
    private EducationalProgramResult() { }

    public sealed record Success : EducationalProgramResult;

    public sealed record Success<T>(T Item) : EducationalProgramResult where T : IEntity;

    public sealed record Fail : EducationalProgramResult;
}