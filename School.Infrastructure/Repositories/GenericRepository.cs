
using System.Collections.Concurrent;
using School.Core.inerfaces;

namespace School.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenerecRepository<T> where T : class
    {
        private readonly ConcurrentDictionary<int, T> _data = new();
        private int _currentId = 1;

        public Task AddAsync(T entity)
        {
            var id = _currentId++;
            _data[id] = entity;
            return Task.FromResult(id);
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(_data.Values.AsEnumerable());

        }

        public Task<T> GetByIdAsync(int id)
        {
            _data.TryGetValue(id, out var entity);
            return Task.FromResult(entity);
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
