using System;

namespace CommandR.Abstractions
{
	public interface IHandlerTypeResolver
	{
		Type ResolveHandlerType<TResponse>(ICommand<TResponse> command);

		Type ResolveHandlerType<TResponse>(IQuery<TResponse> query);
	}
}