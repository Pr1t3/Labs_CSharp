namespace Itmo.ObjectOrientedProgramming.Lab3.DisplayEntity;

public class Display(DisplayDriver displayDriver)
{
    private readonly DisplayDriver _displayDriver = displayDriver;

    public void ShowMessage(string message, ConsoleColor color)
    {
        _displayDriver.ClearDisplay();
        _displayDriver.SetTextColor(color);
        _displayDriver.WriteText(message);
    }
}