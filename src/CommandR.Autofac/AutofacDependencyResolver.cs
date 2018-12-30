using Autofac;
using CommandR.Abstractions;
using System;
using System.Collections.Generic;

namespace CommandR.Autofac
{
	// TODO: Write tests for autofac resolver
	public class AutofacDependencyResolver : IDependencyResolver
	{
		private readonly ILifetimeScope _scope;

		public AutofacDependencyResolver(ILifetimeScope scope)
		{
			_scope = scope;
		}

		public object Resolve(Type typeToResolve)
		{
			return _scope.Resolve(typeToResolve);
		}

		public IEnumerable<T> ResolveAll<T>()
		{
			return _scope.Resolve<IEnumerable<T>>();
		}
	}
}