using System.Threading;
using System.Threading.Tasks;
using CommandR.Abstractions;
using Xunit.Abstractions;

namespace CommandR.UnitTests
{
	public class CreateMovieCommand : Command<int>
	{
		public CreateMovieCommand(CreateMoviePayload payload)
		{
			Name = payload.Name;
		}

		public string Name { get; }

		public sealed class Handler : ICommandHandler<CreateMovieCommand, int>
		{
			private readonly ITestOutputHelper _logger;
			private readonly ICommander _commander;
			private readonly MovieRepository _repo;

			public Handler(ITestOutputHelper logger, ICommander commander, MovieRepository repo)
			{
				_logger = logger;
				_commander = commander;
				_repo = repo;
			}

			public async Task<int> Handle(CreateMovieCommand command, CancellationToken cancellationToken)
			{
				Movie newMovie = _repo.Add(
					new Movie()
					{
						Name = command.Name
					});

				_logger.WriteLine($"Created new Movie: {newMovie.Name} ({newMovie.Id})");

				await _commander.Publish(new MovieCreatedEvent(newMovie.Id, newMovie.Name), cancellationToken);

				return await Task.FromResult(newMovie.Id);
			}
		}
	}
}