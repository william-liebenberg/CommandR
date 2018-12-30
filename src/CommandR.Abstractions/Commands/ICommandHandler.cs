using System.Threading;
using System.Threading.Tasks;

namespace CommandR.Abstractions
{
	/// <summary>
	/// Interface for command handler that returns a response of type <see cref="TResponse"/>.
	/// </summary>
	/// <typeparam name="TCommand"></typeparam>
	/// <typeparam name="TResponse"></typeparam>
	public interface ICommandHandler<in TCommand, TResponse> where TCommand : ICommand<TResponse>
	{
		Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken);
	}

	/// <summary>
	/// Interface for command handler that returns a void (<see cref="Unit"/>) response.
	/// </summary>
	/// <typeparam name="TCommand"></typeparam>
	public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Unit> where TCommand : ICommand<Unit>
	{
	}
}