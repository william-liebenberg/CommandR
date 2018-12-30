using CommandR.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CommandR
{
	/// <summary>
	/// The CqrsAgent is responsible for sending/dispatching the Command and Query requests.
	/// </summary>
	public class CommandR : ICommander
	{
		private readonly IDependencyResolver _dependencyResolver;
		private readonly IHandlerTypeResolver _handlerResolver;

		public CommandR(IDependencyResolver dependencyResolver, IHandlerTypeResolver handlerResolver)
		{
			_dependencyResolver = dependencyResolver;
			_handlerResolver = handlerResolver;
		}

		public async Task<TResponse> Execute<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken)
		{
			Type commandHandlerType = _handlerResolver.ResolveHandlerType(command);
			dynamic handler = _dependencyResolver.Resolve(commandHandlerType);

			// check that Cancellation Token before handling the Command
			cancellationToken.ThrowIfCancellationRequested();
			
			TResponse response = await handler.Handle((dynamic)command, cancellationToken);
			return response;
		}

		public async Task<TResponse> Execute<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken)
		{
			Type queryHandlerType = _handlerResolver.ResolveHandlerType(query);
			dynamic handler = _dependencyResolver.Resolve(queryHandlerType);

			// check that Cancellation Token before handling the Command
			cancellationToken.ThrowIfCancellationRequested();

			TResponse response = await handler.Handle((dynamic)query, cancellationToken);
			return response;
		}

		public async Task Publish<TEvent>(TEvent @event, CancellationToken cancellationToken) where TEvent : IEvent
		{
			IEnumerable<IEventHandler<TEvent>> handlers = _dependencyResolver.ResolveAll<IEventHandler<TEvent>>();
			foreach (IEventHandler<TEvent> handler in handlers)
			{
				await handler.Handle(@event).ConfigureAwait(false);
			}
		}
	}
}