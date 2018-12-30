using CommandR.Abstractions;
using CommandR.MicrosoftDependencyInjection;
using CommandR.Testing.Library;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CommandR.UnitTests
{
	public class CommandTests
	{
		private readonly IServiceCollection _services = new ServiceCollection();
		private readonly HandlerTypeResolver _handlerTypeResolver = new HandlerTypeResolver();

		private readonly CommandR _commander;

		public CommandTests()
		{
			_services.AddCommandR(typeof(CommandTests).Assembly);
			IDependencyResolver dependencyResolver = new MicrosoftDependencyResolver(_services.BuildServiceProvider());
			_commander = new CommandR(dependencyResolver, _handlerTypeResolver);
		}

		[Fact]
		public async Task GivenTestCommandWithResult_WhenSend_ThenResultReturned()
		{
			// Arrange
			var payload = new TestCommandPayload()
			{
				Value1 = new RandomInt()
			};

			// Act
			var cmd = new TestCommandWithResult(payload);
			int resp = await _commander.Execute(cmd, CancellationToken.None);

			// Assert
			Assert.Equal(payload.Value1, resp);
		}

		[Fact]
		public async Task GivenTestCommandWithoutResult_WhenSend_ThenUnitReturned()
		{
			// Arrange

			// Act
			var cmd = new TestCommandWithoutResult();
			Unit resp = await _commander.Execute(cmd, CancellationToken.None);

			// Assert
			Assert.Equal(Unit.Value, resp);
		}
	}
}