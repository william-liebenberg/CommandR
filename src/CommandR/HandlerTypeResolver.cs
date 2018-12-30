using CommandR.Abstractions;
using System;
using System.Collections.Concurrent;

namespace CommandR
{
	public class HandlerTypeResolver : IHandlerTypeResolver
	{
		private readonly ConcurrentDictionary<Type, Type> _commandHandlerTypes = new ConcurrentDictionary<Type, Type>();
		private readonly ConcurrentDictionary<Type, Type> _queryHandlerTypes = new ConcurrentDictionary<Type, Type>();

		public Type ResolveHandlerType<TResponse>(ICommand<TResponse> command)
		{
			Type commandType = command.GetType();
			return _commandHandlerTypes.GetOrAdd(commandType, t => BuildCommandHandlerType<TResponse>(commandType));
		}

		public Type ResolveHandlerType<TResponse>(IQuery<TResponse> query)
		{
			Type queryType = query.GetType();
			return _queryHandlerTypes.GetOrAdd(queryType, t => BuildQueryHandlerType<TResponse>(queryType));
		}

		private static Type BuildCommandHandlerType<TResponse>(Type commandType)
		{
			return typeof(ICommandHandler<,>).MakeGenericType(commandType, typeof(TResponse));
		}

		private static Type BuildQueryHandlerType<TResponse>(Type queryType)
		{
			return typeof(IQueryHandler<,>).MakeGenericType(queryType, typeof(TResponse));
		}
	}
}