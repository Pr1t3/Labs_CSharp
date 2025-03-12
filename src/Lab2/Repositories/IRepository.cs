using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories;

public interface IRepository<T> where T : IEntity
{
    RepositoryResult Add(T entity);

    RepositoryResult FindById(Guid id);

    RepositoryResult RemoveById(Guid id);

    IEnumerable<T> GetAll();
}