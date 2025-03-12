namespace Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;

public abstract record RouteResult
{
    private RouteResult() { }

    public sealed record Success(double Time) : RouteResult;

    public sealed record ForceLimitReached : RouteResult;

    public sealed record SpeedLimitReached : RouteResult;

    public sealed record Fail : RouteResult;
}