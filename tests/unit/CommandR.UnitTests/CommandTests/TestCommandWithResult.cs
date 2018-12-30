using CommandR.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace CommandR.UnitTests
{
	public class TestCommandWithResult : Command<int>
	{
		public TestCommandWithResult(TestCommandPayload payload)
		{
			Value = payload.Value1;
		}

		public int Value { get; }

		public sealed class Handler : ICommandHandler<TestCommandWithResult, int>
		{
			public async Task<int> Handle(TestCommandWithResult command, CancellationToken cancellationToken)
			{
				return await Task.FromResult(command.Value);
			}
		}
	}
}