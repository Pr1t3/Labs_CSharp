namespace Itmo.ObjectOrientedProgramming.Lab2.UserEntity;

public class User : IEntity
{
    public Guid Id { get; }

    public string Name { get; }

    public User(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name can not be null, empty or contain only spaces", nameof(name));
        }

        Name = name;
        Id = Guid.NewGuid();
    }
}