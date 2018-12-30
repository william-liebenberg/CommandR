using System.Threading;
using System.Threading.Tasks;
using CommandR.Abstractions;
using CommandR.MicrosoftDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace CommandR.UnitTests
{

	public class EventTests
	{
		private readonly IServiceCollection _services = new ServiceCollection();
		private readonly HandlerTypeResolver _handlerTypeResolver = new HandlerTypeResolver();
		private readonly CommandR _commander;

		public EventTests(ITestOutputHelper output)
		{
			_services.AddSingleton(output);
			_services.AddSingleton<MovieRepository>();
			_services.AddCommandR(typeof(EventTests).Assembly);

			IDependencyResolver dependencyResolver = new MicrosoftDependencyResolver(_services.BuildServiceProvider());
			_commander = new CommandR(dependencyResolver, _handlerTypeResolver);
		}

		[Fact]
		public async Task TestEvents()
		{
			// Arrange
			var payload = new CreateMoviePayload()
			{
				Name = "Terminator 1"
			};

			// Act
			var cmd = new CreateMovieCommand(payload);
			int resp = await _commander.Execute(cmd, CancellationToken.None);

			// Assert
			Assert.True(resp > 0);
		}
	}
}