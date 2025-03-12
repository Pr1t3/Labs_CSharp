using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.UserEntity;

namespace Itmo.ObjectOrientedProgramming.Lab2.LabEntity;

public class Lab : IPrototype, IEntity
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public string EvaluationCriteria { get; private set; }

    public float Points { get; }

    public User Author { get; }

    public Guid BaseId { get; private set; }

    public Lab(string name, string description, string evaluationCriteria, float points, User author)
    {
        if (points > 100)
        {
            throw new ArgumentException("Points should be less than 100");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name can not be null, empty or contain only spaces", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Description can not be null, empty or contain only spaces", nameof(description));
        }

        if (string.IsNullOrWhiteSpace(evaluationCriteria))
        {
            throw new ArgumentException("Evaluation criteria can not be null, empty or contain only spaces", nameof(evaluationCriteria));
        }

        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        EvaluationCriteria = evaluationCriteria;
        Points = points;
        Author = author;
    }

    private Lab(string name, string description, string evaluationCriteria, float points, User author, Guid baseId)
    {
        if (points > 100)
        {
            throw new ArgumentException("Points should be less than 100");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name can not be null, empty or contain only spaces", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Description can not be null, empty or contain only spaces", nameof(description));
        }

        if (string.IsNullOrWhiteSpace(evaluationCriteria))
        {
            throw new ArgumentException("Evaluation criteria can not be null, empty or contain only spaces", nameof(evaluationCriteria));
        }

        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        EvaluationCriteria = evaluationCriteria;
        Points = points;
        Author = author;
        BaseId = baseId;
    }

    public IPrototype Clone() => new Lab(Name, Description, EvaluationCriteria, Points, Author, Id);

    public LabResult UpdateData(string newName, string newDescription, string newEvaluationCriteria, User user)
    {
        if (user.Id != Author.Id || string.IsNullOrWhiteSpace(newName) || string.IsNullOrWhiteSpace(newDescription) || string.IsNullOrWhiteSpace(newEvaluationCriteria))
        {
            return new LabResult.Fail();
        }

        Name = newName;
        Description = newDescription;
        EvaluationCriteria = newEvaluationCriteria;
        return new LabResult.Success();
    }
}