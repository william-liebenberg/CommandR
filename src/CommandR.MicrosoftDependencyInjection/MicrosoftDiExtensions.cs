using CommandR.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommandR.MicrosoftDependencyInjection
{
	public static class MicrosoftDiExtensions
	{
		public static IServiceCollection AddCommandR(this IServiceCollection services, Assembly[] yourAssemblies)
		{
			foreach (Assembly assembly in yourAssemblies)
			{
				RegisterHandlersForAssembly(services, assembly);
			}

			ConfigureCommandR(services);

			return services;
		}

		public static IServiceCollection AddCommandR(this IServiceCollection services, Assembly yourAssembly)
		{
			RegisterHandlersForAssembly(services, yourAssembly);

			ConfigureCommandR(services);

			return services;
		}

		private static void RegisterHandlersForAssembly(IServiceCollection services, Assembly assembly)
		{
			// register all the command and query handlers
			List<Type> handlerTypes = assembly.GetTypes()
				.Where(x => x.GetInterfaces().Any(IsHandlerInterface))
				.ToList();

			handlerTypes.ForEach(y => AddHandler(services, y));
		}

		private static void ConfigureCommandR(IServiceCollection services)
		{
			services.AddScoped<IHandlerTypeResolver, HandlerTypeResolver>();
			services.AddScoped<IDependencyResolver, MicrosoftDependencyResolver>(s => new MicrosoftDependencyResolver(s));
			services.AddScoped<ICommander, CommandR>();
		}

		private static void AddHandler(IServiceCollection services, Type type)
		{
			Type[] interfaces = type.GetInterfaces();
			foreach (Type baseInterface in interfaces)
			{
				if (IsHandlerInterface(baseInterface))
				{
					services.AddTransient(baseInterface, type);
				}
			}
		}

		private static readonly Type[] _handlerTypes = {
			typeof(ICommandHandler<>),
			typeof(ICommandHandler<,>),
			typeof(IQueryHandler<,>),
			typeof(IEventHandler<>)
		};

		private static bool IsHandlerInterface(Type type)
		{
			if (!type.IsGenericType)
			{
				return false;
			}

			Type typeDefinition = type.GetGenericTypeDefinition();
			return _handlerTypes.Any(t => typeDefinition == t);
		}
	}
}