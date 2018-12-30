using System;
using CommandR.Abstractions;

namespace CommandR
{
	public abstract class Command<TResponse> : ICommand<TResponse>
	{
	}

	/// <summary>
	/// Command that has no arguments and no response.
	/// </summary>
	public abstract class Command : Command<Unit>
	{
	}
}