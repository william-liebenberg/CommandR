using System;

namespace CommandR.Abstractions
{
	public interface IEvent
	{
		Guid EventId { get; }
	}
}