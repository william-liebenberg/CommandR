using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommandR.Abstractions
{
	public interface IEventPublisher
	{
		Task Publish(IEvent @event);

		IReadOnlyCollection<IEvent> Events { get; }
	}
}