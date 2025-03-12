namespace Lab5.Application.ResultTypes;

public abstract record ResultT
{
    private ResultT() { }

    public sealed record Success : ResultT;

    public sealed record SuccessWithData<T>(T Data) : ResultT;

    public sealed record Fail : ResultT;
}