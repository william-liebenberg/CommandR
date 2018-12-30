using System;

namespace CommandR.Abstractions
{
	public interface IBaseCommand
	{
	}

	public interface ICommand<out TResponse> : IBaseCommand { }

	/// <summary>
	/// Interface for commands that returns a void (<see cref="Unit"/>) response.
	/// </summary>
	public interface ICommand : ICommand<Unit> { }
}