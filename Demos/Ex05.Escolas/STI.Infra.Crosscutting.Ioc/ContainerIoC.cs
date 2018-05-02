using System;
using System.Collections.Generic;

namespace STI.Infra.Crosscutting.Ioc
{
    public static class ContainerIoC
    {
        public static IDependencyContainer Container { get; private set; }

        public static void SetContainer(IDependencyContainer container)
        {
            Container = container;
        }

        public static void RegisterAsSingleton<T1, T2>(T2 instance = null) where T2 : class
        {
            Container.Registry.Singleton(instance).Register<T1, T2>();
        }

        public static T Resolve<T>()
        {
            return Container.Resolver.Resolve<T>();
        }

        public static void Register<T1, T2>(string name = null) where T2 : class
        {
            Container.Registry.Register(typeof(T1), typeof(T2), name);
        }

        public static void Register(Type from, Type to, string name = null)
        {
            Container.Registry.Register(from, to, name);
        }

        public static object Resolve(Type from, string name = null, Dictionary<string, object> parameterOverrides = null)
        {
            return Container.Resolver.Resolve(from, name, parameterOverrides);
        }

        public static T Resolve<T>(string name = null, Dictionary<string, object> parameterOverrides = null)
        {
            return Container.Resolver.Resolve<T>(name, parameterOverrides);
        }

        public static IEnumerable<T> ResolveAll<T>()
        {
            return Container.Resolver.ResolveAll<T>();
        }

        public static IEnumerable<object> ResolveAll(Type t)
        {
            return Container.Resolver.ResolveAll(t);
        }

        public static void RegisterInstance<T1>(T1 instance)
        {
            Container.Registry.Singleton(instance).Register<T1, T1>();
        }
    }
}
