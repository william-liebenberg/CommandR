using CommandR.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace CommandR.UnitTests
{
	public class TestCommandWithoutResult : Command
	{
		public sealed class Handler : ICommandHandler<TestCommandWithoutResult>
		{
			public async Task<Unit> Handle(TestCommandWithoutResult command, CancellationToken cancellationToken)
			{
				return await Task.FromResult(Unit.Value);
			}
		}
	}
}