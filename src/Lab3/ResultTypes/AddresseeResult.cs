namespace Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

public abstract record AddresseeResult
{
    private AddresseeResult() { }

    public sealed record Success : AddresseeResult;

    public sealed record Fail : AddresseeResult;
}