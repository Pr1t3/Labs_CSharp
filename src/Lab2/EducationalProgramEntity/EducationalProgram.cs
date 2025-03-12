// Класс образовательной программы
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.SubjectEntity;
using Itmo.ObjectOrientedProgramming.Lab2.UserEntity;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationalProgramEntity;

public class EducationalProgram : IEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Name { get; private set; }

    public Dictionary<int, List<Subject>> SubjectsPerTerm { get; private set; }

    public User? ProgramManager { get; private set; }

    private EducationalProgram(string name, Dictionary<int, List<Subject>> subjectsPerTerm, User programManager)
    {
        Name = name;
        SubjectsPerTerm = subjectsPerTerm;
        ProgramManager = programManager;
    }

    public class EducationalProgramBuilder
    {
        public string Name { get; private set; } = string.Empty;

        public Dictionary<int, List<Subject>> SubjectsPerTerm { get; private set; } = [];

        public User? ProgramManager { get; private set; }

        public EducationalProgramBuilder WithName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name can not be null, empty or contain only spaces", nameof(name));
            }

            Name = name;
            return this;
        }

        public EducationalProgramBuilder WithProgramManager(User programManager)
        {
            ProgramManager = programManager;
            return this;
        }

        public EducationalProgramBuilder WithSubject(int term, Subject subject)
        {
            if (term < 0)
            {
                throw new ArgumentException("Term must be non-negative", nameof(term));
            }

            if (!SubjectsPerTerm.ContainsKey(term))
                SubjectsPerTerm[term] = [];
            SubjectsPerTerm[term].Add(subject);
            return this;
        }

        public EducationalProgramResult Build()
        {
            if (ProgramManager == null || string.IsNullOrWhiteSpace(Name) || SubjectsPerTerm.Count == 0)
            {
                return new EducationalProgramResult.Fail();
            }

            return new EducationalProgramResult.Success<EducationalProgram>(new EducationalProgram(Name, SubjectsPerTerm, ProgramManager));
        }
    }
}