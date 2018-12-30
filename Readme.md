# CommandR

I wrote this library from scratch in an attempt to really understand the basics of the CQRS (**C**ommand **Q**uery **R**esponsibility **S**egregation) Pattern.

## Implemeting a Command

```csharp
public class CreateMovieCommand : Command<int>
{
	public CreateMovieCommand(string movieName)
	{
		Name = movieName;
	}
	
	public string MovieName { get; }

	// Sealed & Nested class that implements the Command Handler for the CreateMovieCommand and returns the new movie ID (as an int)
	public sealed class Handler : ICommandHandler<CreateMovieCommand, int>
	{
		private readonly ICommander _commander;
		private readonly MovieRepository _repo;
		public Handler(ICommander commander, MovieRepository repo)
		{
			_commander = commander;
			_repo = repo;
		}

		public async Task<int> Handle(CreateMovieCommand command, CancellationToken cancellationToken)
		{
			Movie newMovie = await _repo.AddAsync(
				new Movie()
				{
					Name = command.Name
				});

			await _commander.Publish(new MovieCreatedEvent(newMovie.Id, newMovie.Name), cancellationToken);

			return newMovie.Id;
		}
	}
}
```

## Executing a Command

You can `execute` your command via an instance of the `ICommander`. The `ICommander` will take care of resolving the correct Command Handler for you along with injecting all the required dependencies that the handler requires.

```csharp

public class MoviesController : Controller
{
	private readonly ICommander _commander;

	public MoviesController(ICommander commander)
	{
		_commander = commander;
	}

	[HttpPost]
	public async Task<IActionResult> Post([FromBody]string movieName)
	{
		// instantiate your command with the required arguments
		var cmd = new CreateMovieCommand(movieName);

		// execute the command via the ICommander
		int newMovieId = await _commander.Execute(cmd);

		// return Ok with the new id
		return Ok(newMovieId);
	}
}
```

## TODO's

This is a short todo list of things that I want and are going to be adding to this library.

- [ ] Command/Query processing pielines to deal with cross-cutting concerns (authorization, logging, instrumentation, caching)
- [ ] Documentation
- [x] Tests
- [ ] Better tests
- [ ] Azure Functions Wrapper
- [ ] Nugetize