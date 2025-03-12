namespace Itmo.ObjectOrientedProgramming.Lab3.MessageEntity;

public class Message
{
    public string Title { get; }

    public string Text { get; }

    public ImportanceLevel Importance { get; }

    public Message(string title, string text, ImportanceLevel importance)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title can not be null, empty or contain only spaces", nameof(title));
        }

        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Text can not be null, empty or contain only spaces", nameof(text));
        }

        Title = title;
        Text = text;
        Importance = importance;
    }
}