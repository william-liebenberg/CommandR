using System;
using CommandR.Abstractions;

namespace CommandR
{
	public abstract class Event : IEvent
	{
		public Guid EventId { get; } = Guid.NewGuid();
	}
}