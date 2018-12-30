using CommandR.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace CommandR.UnitTests
{
	public class TestQuery : Query<TestResponse>
	{
		public TestQuery(string passThroughValue)
		{
			PassThroughValue = passThroughValue;
		}

		private string PassThroughValue { get; }

		public sealed class Handler : IQueryHandler<TestQuery, TestResponse>
		{
			public async Task<TestResponse> Handle(TestQuery query, CancellationToken cancellationToken)
			{
				var response = new TestResponse()
				{
					PassThroughValue = query.PassThroughValue
				};

				return await Task.FromResult(response);
			}
		}
	}
}