using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.UserEntity;

namespace Itmo.ObjectOrientedProgramming.Lab2.LectureMaterialsEntity;

public class LectureMaterials : IPrototype, IEntity
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public string Content { get; private set; }

    public User Author { get; }

    public Guid? BaseId { get; private set; }

    public LectureMaterials(string name, string description, string content, User author)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name can not be null, empty or contain only spaces", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Description can not be null, empty or contain only spaces", nameof(description));
        }

        if (string.IsNullOrWhiteSpace(content))
        {
            throw new ArgumentException("Content can not be null, empty or contain only spaces", nameof(content));
        }

        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Content = content;
        Author = author;
    }

    public IPrototype Clone() => new LectureMaterials(Name, Description, Content, Author);

    public LectureMaterialsResult UpdateData(string newName, string newDescription, string newContent, User user)
    {
        if (user.Id != Author.Id || string.IsNullOrWhiteSpace(newName) || string.IsNullOrWhiteSpace(newDescription) || string.IsNullOrWhiteSpace(newContent))
        {
            return new LectureMaterialsResult.Fail();
        }

        Name = newName;
        Description = newDescription;
        Content = newContent;
        return new LectureMaterialsResult.Success();
    }
}