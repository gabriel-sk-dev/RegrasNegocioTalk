using System;
using System.Collections.Generic;
using System.Text;

namespace STI.Infra.Crosscutting.Ioc
{
    public interface IDependencyRegistry
    {
        bool IsRegistered<T>(string name = null);
        bool IsRegistered(Type typeToCheck, string name = null);
        IDependencyRegistry Name(string name);
        IDependencyRegistry Singleton(object instance = null);
        IDependencyRegistry From(Type from);
        IDependencyRegistry To(Type to);
        IDependencyRegistry ConstuctorParameters(params object[] constuctorParameters);
        IDependencyRegistry Register(string name = null);
        IDependencyRegistry Register<TFrom, TTo>(string name = null);
        IDependencyRegistry Register(Type from, Type to, string name = null);
    }
}
