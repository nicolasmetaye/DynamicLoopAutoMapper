using System;
using System.Collections.Generic;
using System.Linq;
using DynamicLoopAutoMapper.Data.Entities;
using DynamicLoopAutoMapper.Extensions;

namespace DynamicLoopAutoMapper.Data.Repositories
{
    public class Repository<T> where T : Entity, new()
    {
        private static List<T> _dataCollection = new List<T>();
        private readonly object _lock = new object();

        public T Insert(T entity)
        {
            lock (_lock)
            {
                var incrementedId = 1;
                if (_dataCollection.Any())
                    incrementedId = _dataCollection.Max(arg => arg.Id) + 1;
                var clone = entity.Clone();
                clone.Id = incrementedId;
                _dataCollection.Add(clone);
                return clone.Clone();
            }
        }

        public void Save(T entity)
        {
            lock (_lock)
            {
                if (_dataCollection.Any(obj => obj.Id == entity.Id))
                {
                    _dataCollection.RemoveAll(obj => obj.Id == entity.Id);
                    _dataCollection.Add(entity.Clone());
                }
            }
        }

        public void Delete(int id)
        {
            lock (_lock)
            {
                _dataCollection.RemoveAll(obj => obj.Id == id);
            }
        }

        public IEnumerable<T> SearchFor(Func<T, bool> predicate)
        {
            lock (_lock)
            {
                return _dataCollection.Where(predicate).Select(arg => arg.Clone()).OrderBy(arg => arg.Id).ToList();
            }
        }

        public bool Any(Func<T, bool> predicate)
        {
            lock (_lock)
            {
                return _dataCollection.Any(predicate);
            }
        }

        public IEnumerable<T> GetAll()
        {
            lock (_lock)
            {
                return _dataCollection.Select(arg => arg.Clone()).OrderBy(arg => arg.Id).ToList();
            }
        }

        public void DeleteAll()
        {
            lock (_lock)
            {
                _dataCollection.Clear();
            }
        }

        public T GetById(int id)
        {
            lock (_lock)
            {
                var entity = _dataCollection.SingleOrDefault(e => e.Id.Equals(id));
                if (entity == null)
                    return null;
                return entity.Clone();
            }
        }
    }
}