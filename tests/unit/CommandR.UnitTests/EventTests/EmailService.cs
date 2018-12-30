using System.Threading.Tasks;
using CommandR.Abstractions;
using Xunit.Abstractions;

namespace CommandR.UnitTests
{
	// TODO: Write test to verify handlers are called
	public class EmailService : IEventHandler<MovieCreatedEvent>
	{
		private readonly ITestOutputHelper _logger;

		public EmailService(ITestOutputHelper logger)
		{
			_logger = logger;
		}

		public Task Handle(MovieCreatedEvent @event)
		{
			_logger.WriteLine($"Sending Email about {@event.MovieName} (Movie ID: {@event.MovieId}, EventID: {@event.EventId})");
			return Task.CompletedTask;
		}
	}
}