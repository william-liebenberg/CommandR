using CommandR.Abstractions;
using CommandR.MicrosoftDependencyInjection;
using CommandR.Testing.Library;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CommandR.UnitTests
{
	public class QueryTests
	{
		private readonly IServiceCollection _services = new ServiceCollection();
		private readonly HandlerTypeResolver _handlerTypeResolver = new HandlerTypeResolver();

		private readonly CommandR _commander;

		public QueryTests()
		{
			_services.AddCommandR(typeof(QueryTests).Assembly);
			IDependencyResolver dependencyResolver = new MicrosoftDependencyResolver(_services.BuildServiceProvider());
			_commander = new CommandR(dependencyResolver, _handlerTypeResolver);
		}

		[Fact]
		public async Task GivenTestQueryWithPassThroughValue_WhenSend_ThenPassThroughValueReturned()
		{
			// Arrange
			string passthroughValue = new RandomString();

			// Act
			TestQuery query = new TestQuery(passthroughValue);
			TestResponse resp = await _commander.Execute(query, CancellationToken.None);

			// Assert
			Assert.Equal(passthroughValue, resp.PassThroughValue);
		}
	}
}