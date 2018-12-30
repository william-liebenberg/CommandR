using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using CommandR.Abstractions;
using Module = Autofac.Module;

namespace CommandR.Autofac
{
	/// <summary>
	/// AutoFac Module that adds CommandR and register all the Command, Query, and Event Handler types.
	/// </summary>
	public class CommandRModule : Module
	{
		private readonly Assembly[] _assemblies;

		public CommandRModule(Assembly[] assemblies)
		{
			_assemblies = assemblies;
		}

		protected override void Load(ContainerBuilder builder)
		{
			foreach (Assembly assembly in _assemblies)
			{
				List<Type> handlerTypes = assembly.GetTypes()
					.Where(x => x.GetInterfaces().Any(IsHandlerInterface))
					.ToList();

				handlerTypes.ForEach(y => AddHandler(builder, y));
			}

			builder.RegisterType<HandlerTypeResolver>().As<IHandlerTypeResolver>();
			builder.RegisterType<AutofacDependencyResolver>().As<IDependencyResolver>();
			builder.RegisterType<CommandR>().As<ICommander>();
		}

		private static void AddHandler(ContainerBuilder builder, Type type)
		{
			Type[] interfaces = type.GetInterfaces();
			foreach (Type baseInterface in interfaces)
			{
				if (IsHandlerInterface(baseInterface))
				{
					builder.RegisterType(type).As(baseInterface);
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