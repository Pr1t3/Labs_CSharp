namespace Itmo.ObjectOrientedProgramming.Lab3.DisplayEntity;

public class DisplayDriver
{
    public void ClearDisplay()
    {
        Console.Clear();
    }

    public void SetTextColor(ConsoleColor color)
    {
        Console.ForegroundColor = color;
    }

    public void WriteText(string text)
    {
        Console.WriteLine(text);
    }
}