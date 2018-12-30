using System.Threading.Tasks;

namespace CommandR.Abstractions
{
	public interface IEventHandler<in TEvent> where TEvent : IEvent
	{
		Task Handle(TEvent @event);
	}
}