using System;
using System.Collections.Generic;
using CommandR.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace CommandR.MicrosoftDependencyInjection
{
	public class MicrosoftDependencyResolver : IDependencyResolver
	{
		private readonly IServiceProvider _serviceProvider;

		public MicrosoftDependencyResolver(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public object Resolve(Type typeToResolve)
		{
			return _serviceProvider.GetService(typeToResolve);
		}

		public IEnumerable<T> ResolveAll<T>()
		{
			return _serviceProvider.GetServices<T>();
		}
	}
}