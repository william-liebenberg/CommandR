using System.Threading.Tasks;

namespace CommandR.Abstractions
{
	/// <summary>
	/// Implementations of the <see cref="IEventStore"/> can used to persist events and to retrieve events.
	/// </summary>
	public interface IEventStore
	{
		Task Store(IEvent @event);
	}
}