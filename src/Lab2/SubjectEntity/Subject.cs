using Itmo.ObjectOrientedProgramming.Lab2.LabEntity;
using Itmo.ObjectOrientedProgramming.Lab2.LectureMaterialsEntity;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.UserEntity;

namespace Itmo.ObjectOrientedProgramming.Lab2.SubjectEntity;

public class Subject : IPrototype, IEntity
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public IReadOnlyCollection<Lab> Labs { get; }

    public IReadOnlyCollection<LectureMaterials> LectureMaterials { get; private set; }

    public User Author { get; }

    public Guid? BaseId { get; private set; }

    public float? MinimumPoints { get; } = null;

    public float? PointsForExam { get; } = null;

    public SubjectFormat Format { get; }

    public Subject(string name, IReadOnlyCollection<Lab> labs, IReadOnlyCollection<LectureMaterials> lectureMaterials, User author, SubjectFormat format, float? minimumPoints = null, float? pointsForExam = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name can not be null, empty or contain only spaces", nameof(name));
        }

        Format = format;
        Id = Guid.NewGuid();
        Name = name;
        Labs = labs;
        LectureMaterials = lectureMaterials;
        Author = author;

        float totalPoints = labs.Sum(lab => lab.Points);

        if (format == SubjectFormat.Exam)
        {
            PointsForExam = pointsForExam ?? throw new ArgumentException("Points for exam can not be null", nameof(pointsForExam));
            if (PointsForExam < 0)
            {
                throw new ArgumentException("Points for exam must be a positive value", nameof(pointsForExam));
            }

            totalPoints += (float)PointsForExam;
        }
        else if (format == SubjectFormat.Zachet)
        {
            MinimumPoints = minimumPoints ?? throw new ArgumentException("Minimum points can not be null", nameof(minimumPoints));

            if (MinimumPoints < 0)
            {
                throw new ArgumentException("Minimum points must be a positive value", nameof(pointsForExam));
            }
        }
        else
        {
            throw new ArgumentException("Format is inccorect");
        }

        if (totalPoints != 100)
        {
            throw new ArgumentException("Total points for subject must be 100", nameof(labs));
        }
    }

    public IPrototype Clone() => new Subject(Name, Labs, LectureMaterials, Author, Format, MinimumPoints, PointsForExam);

    public SubjectResult UpdateData(string newName, IReadOnlyCollection<LectureMaterials> lectureMaterials, User user)
    {
        if (user.Id != Author.Id || string.IsNullOrWhiteSpace(newName))
        {
            return new SubjectResult.Fail();
        }

        Name = newName;
        LectureMaterials = lectureMaterials;
        return new SubjectResult.Success();
    }
}