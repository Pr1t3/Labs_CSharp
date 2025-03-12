using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;

namespace Itmo.ObjectOrientedProgramming.Lab2.Repositories;

public class Repository<T> : IRepository<T> where T : IEntity
{
    private readonly Dictionary<Guid, T> _items = [];

    public RepositoryResult Add(T? entity)
    {
        if (entity == null)
        {
            return new RepositoryResult.Fail();
        }

        return _items.TryAdd(entity.Id, entity) ? new RepositoryResult.Success() : new RepositoryResult.Fail();
    }

    public RepositoryResult FindById(Guid id)
    {
        return _items.TryGetValue(id, out T? item) ? new RepositoryResult.Success<T>(item) : new RepositoryResult.Fail();
    }

    public RepositoryResult RemoveById(Guid id)
    {
        return _items.Remove(id) ? new RepositoryResult.Success() : new RepositoryResult.Fail();
    }

    public IEnumerable<T> GetAll()
    {
        return _items.Values;
    }
}