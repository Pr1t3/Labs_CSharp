using Itmo.ObjectOrientedProgramming.Lab2.EntityCreator;
using Itmo.ObjectOrientedProgramming.Lab2.LabEntity;
using Itmo.ObjectOrientedProgramming.Lab2.LectureMaterialsEntity;
using Itmo.ObjectOrientedProgramming.Lab2.Repositories;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.SubjectEntity;
using Itmo.ObjectOrientedProgramming.Lab2.UserEntity;
using Xunit;

namespace Lab1.Tests;

public class Lab2Test
{
    [Fact]
    public void Scenario1()
    {
        // Arrange
        var labsRepo = new Repository<Lab>();
        var lectureMaterialsRepo = new Repository<LectureMaterials>();
        var subjectsRepo = new Repository<Subject>();
        var person1 = new User("Person 1");
        var person2 = new User("Person 2");
        var factory = new EntitiesFactory(person1, labsRepo, lectureMaterialsRepo, subjectsRepo);

        EntitiesFactoryResult result = factory.CreateLab("Lab 1", "first lab", "10", 100);

        // Act
        if (result is EntitiesFactoryResult.Success<Lab> success)
        {
            LabResult labResult = success.Item.UpdateData("Lab 2", "second", "22", person2);

            // Assert
            Assert.IsType<LabResult.Fail>(labResult);
        }
    }

    [Fact]
    public void Scenario2()
    {
        // Arrange
        var labsRepo = new Repository<Lab>();
        var lectureMaterialsRepo = new Repository<LectureMaterials>();
        var subjectsRepo = new Repository<Subject>();
        var person1 = new User("Person 1");
        var person2 = new User("Person 2");
        var factory = new EntitiesFactory(person1, labsRepo, lectureMaterialsRepo, subjectsRepo);

        EntitiesFactoryResult factoryResult = factory.CreateLab("Lab 1", "first lab", "10", 100);

        if (factoryResult is EntitiesFactoryResult.Success<Lab> success)
        {
            var lab2 = (Lab)success.Item.Clone();

            // Act
            bool result = lab2.BaseId.Equals(success.Item.Id);

            // Assert
            Assert.True(result);
        }
    }

    [Fact]
    public void Scenario3()
    {
        // Arrange
        var labsRepo = new Repository<Lab>();
        var lectureMaterialsRepo = new Repository<LectureMaterials>();
        var subjectsRepo = new Repository<Subject>();
        var person1 = new User("Person 1");
        var person2 = new User("Person 2");
        var factory = new EntitiesFactory(person1, labsRepo, lectureMaterialsRepo, subjectsRepo);

        EntitiesFactoryResult result1 = factory.CreateLab("Lab 1", "first", "10", 15, person2);
        EntitiesFactoryResult result2 = factory.CreateLab("Lab 2", "second", "10", 20, person2);
        EntitiesFactoryResult result3 = factory.CreateLectureMaterials("Lecture materials 1", "first", "10", person2);

        if (result1 is EntitiesFactoryResult.Success<Lab> success1 && result2 is EntitiesFactoryResult.Success<Lab> success2 && result3 is EntitiesFactoryResult.Success<LectureMaterials> success3)
        {
            // Act and Assert
            Assert.ThrowsAny<Exception>(() => factory.CreateExamSubject("Exam 1", [success1.Item, success2.Item], [success3.Item], person2, 30));
        }
    }
}
