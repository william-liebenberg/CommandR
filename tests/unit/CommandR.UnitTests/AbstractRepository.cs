using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace CommandR.UnitTests
{
	/// <summary>
	/// Abstract In-Memory repository to help with unit testing the TestCommands and TestQueries
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	public abstract class AbstractRepository<TEntity> where TEntity : Entity
	{
		private int _autoId = 0;
		private int AutoId => ++_autoId;

		private readonly ConcurrentDictionary<int, TEntity> _entities = new ConcurrentDictionary<int, TEntity>();

		public TEntity Add(TEntity ax)
		{
			ax.Id = AutoId;
			_entities.GetOrAdd(ax.Id, ax);

			return ax;
		}

		public TEntity Update(TEntity ax)
		{
			if (Get(ax.Id) == null)
			{
				throw new Exception($"{typeof(TEntity).Name} with Id {ax.Id} not found!");
			}

			_entities.AddOrUpdate(ax.Id, (id) => ax, (id, existing) => ax);

			return ax;
		}

		public TEntity Remove(TEntity c)
		{
			return Remove(c.Id);
		}

		public TEntity Remove(int id)
		{
			return _entities.Remove(id, out TEntity val) ? val : null;
		}

		public TEntity Get(int id)
		{
			return _entities.TryGetValue(id, out TEntity val) ? val : default(TEntity);
		}

		public IReadOnlyCollection<TEntity> GetAll()
		{
			return _entities.Values.ToReadOnly();
		}
	}
}