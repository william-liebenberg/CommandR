using System;
using System.Collections.Generic;

namespace CommandR.Abstractions
{
	public interface IDependencyResolver
	{
		object Resolve(Type typeToResolve);
		IEnumerable<T> ResolveAll<T>();
	}
}