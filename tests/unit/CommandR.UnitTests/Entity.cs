using System;

namespace CommandR.UnitTests
{
	public interface IEntity<TId>
	{
		TId Id { get; set; }
	}

	public abstract class Entity : IEntity<int>
	{
		public int Id { get; set; }
		public string ModifiedBy { get; set; }
		public DateTimeOffset ModifiedOn { get; } = DateTimeOffset.Now;
	}
}