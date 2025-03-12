using Itmo.ObjectOrientedProgramming.Lab4.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.CommandsHandlers;

public abstract class CommandHandler
{
    protected CommandHandler? NextHandler { get; private set; }

    public void SetNext(CommandHandler handler)
    {
        NextHandler = handler;
    }

    public ICommand Handle(string[] args)
    {
        if (CheckCommand(args))
        {
            return CreateCommand(args);
        }
        else if (NextHandler != null)
        {
            return NextHandler.Handle(args);
        }

        throw new ArgumentException("Invalid command");
    }

    protected abstract bool CheckCommand(string[] args);

    protected abstract ICommand CreateCommand(string[] args);
}