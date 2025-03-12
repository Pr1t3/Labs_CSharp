namespace Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

public abstract record LectureMaterialsResult
{
    private LectureMaterialsResult() { }

    public sealed record Success : LectureMaterialsResult;

    public sealed record Fail : LectureMaterialsResult;
}