using System;
using System.Collections.Generic;
using System.Text;

namespace STI.Infra.Crosscutting.Ioc
{
    public interface IDependencyResolver
    {
        TFrom Resolve<TFrom>();
        TFrom Resolve<TFrom>(string name = null, IDictionary<string, object> parameterOverrides = null, IDictionary<string, object> propertyOverrides = null);
        object Resolve(Type type, string name = null, IDictionary<string, object> parameterOverrides = null, IDictionary<string, object> propertyOverrides = null);
        IEnumerable<T> ResolveAll<T>();
        IEnumerable<object> ResolveAll(Type type);
    }
}
