using Itmo.ObjectOrientedProgramming.Lab2.LabEntity;
using Itmo.ObjectOrientedProgramming.Lab2.LectureMaterialsEntity;
using Itmo.ObjectOrientedProgramming.Lab2.Repositories;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.SubjectEntity;
using Itmo.ObjectOrientedProgramming.Lab2.UserEntity;

namespace Itmo.ObjectOrientedProgramming.Lab2.EntityCreator;

public class EntitiesFactory
{
    private readonly User _commonAuthor;
    private readonly IRepository<Lab> _labsRepo;
    private readonly IRepository<LectureMaterials> _lectureMaterialsRepo;
    private readonly IRepository<Subject> _subjectsRepo;

    public EntitiesFactory(User commonAuthor, IRepository<Lab> labsRepo, IRepository<LectureMaterials> lectureMaterialsRepo, IRepository<Subject> subjectsRepo)
    {
        _commonAuthor = commonAuthor;
        _labsRepo = labsRepo;
        _lectureMaterialsRepo = lectureMaterialsRepo;
        _subjectsRepo = subjectsRepo;
    }

    public EntitiesFactoryResult CreateLab(string name, string description, string evaluationCriteria, float points, User? author = null)
    {
        author ??= _commonAuthor;

        var newLab = new Lab(name, description, evaluationCriteria, points, author);

        RepositoryResult result = _labsRepo.Add(newLab);
        return result is RepositoryResult.Fail ? new EntitiesFactoryResult.Fail() : new EntitiesFactoryResult.Success<Lab>(newLab);
    }

    public EntitiesFactoryResult CreateLectureMaterials(string name, string description, string content, User? author = null)
    {
        author ??= _commonAuthor;

        var newLectureMaterial = new LectureMaterials(name, description, content, author);

        RepositoryResult result = _lectureMaterialsRepo.Add(newLectureMaterial);
        return result is RepositoryResult.Fail ? new EntitiesFactoryResult.Fail() : new EntitiesFactoryResult.Success<LectureMaterials>(newLectureMaterial);
    }

    public EntitiesFactoryResult CreateExamSubject(string name, IReadOnlyCollection<Lab> labs, IReadOnlyCollection<LectureMaterials> lectureMaterials, User? author, float pointsForExam)
    {
        author ??= _commonAuthor;

        var newSubject = new Subject(name, labs, lectureMaterials, author, SubjectFormat.Exam, pointsForExam: pointsForExam);

        RepositoryResult result = _subjectsRepo.Add(newSubject);
        return result is RepositoryResult.Fail ? new EntitiesFactoryResult.Fail() : new EntitiesFactoryResult.Success<Subject>(newSubject);
    }

    public EntitiesFactoryResult CreateZachetSubject(string name, IReadOnlyCollection<Lab> labs, IReadOnlyCollection<LectureMaterials> lectureMaterials, User? author, float minimumPoints)
    {
        author ??= _commonAuthor;

        var newSubject = new Subject(name, labs, lectureMaterials, author, SubjectFormat.Zachet, minimumPoints: minimumPoints);

        RepositoryResult result = _subjectsRepo.Add(newSubject);
        return result is RepositoryResult.Fail ? new EntitiesFactoryResult.Fail() : new EntitiesFactoryResult.Success<Subject>(newSubject);
    }
}