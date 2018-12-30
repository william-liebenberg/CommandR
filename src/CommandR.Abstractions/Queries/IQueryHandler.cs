using System.Threading;
using System.Threading.Tasks;

namespace CommandR.Abstractions
{
	public interface IQueryHandler<in TQuery, TResponse> where TQuery : IQuery<TResponse>
	{
		Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken);
	}
}