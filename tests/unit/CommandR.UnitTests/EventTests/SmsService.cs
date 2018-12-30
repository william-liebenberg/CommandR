using System.Threading.Tasks;
using CommandR.Abstractions;
using Xunit.Abstractions;

namespace CommandR.UnitTests
{
	// TODO: Write test to verify handlers are called
	public class SmsService : IEventHandler<MovieCreatedEvent>
	{
		private readonly ITestOutputHelper _logger;

		public SmsService(ITestOutputHelper logger)
		{
			_logger = logger;
		}

		public Task Handle(MovieCreatedEvent @event)
		{
			_logger.WriteLine($"Sending SMS about {@event.MovieName} ({@event.MovieId}, EventID: {@event.EventId})");
			return Task.CompletedTask;
		}
	}
}