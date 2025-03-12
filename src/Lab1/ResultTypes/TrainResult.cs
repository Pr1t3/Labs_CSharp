namespace Itmo.ObjectOrientedProgramming.Lab1.ResultTypes;

public abstract record TrainResult
{
    private TrainResult() { }

    public sealed record SuccessPass(double Time) : TrainResult;

    public sealed record SuccessApplyingForce : TrainResult;

    public sealed record ForceLimitReached : TrainResult;

    public sealed record FailedPass : TrainResult;
}