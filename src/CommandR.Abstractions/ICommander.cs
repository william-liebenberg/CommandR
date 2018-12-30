using System.Threading;
using System.Threading.Tasks;

namespace CommandR.Abstractions
{
	public interface ICommander
	{
		Task<TResponse> Execute<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken);

		Task<TResponse> Execute<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken);

		Task Publish<TEvent>(TEvent @event, CancellationToken cancellationToken) where TEvent : IEvent;
	}
}