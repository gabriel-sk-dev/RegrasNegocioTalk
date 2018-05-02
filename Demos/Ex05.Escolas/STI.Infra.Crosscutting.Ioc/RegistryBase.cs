using System;
using System.Collections.Generic;
using System.Text;

namespace STI.Infra.Crosscutting.Ioc
{
    public abstract class RegistryBase : IDependencyRegistry
    {
        protected DependencyRegistryOptions _options { get; set; }

        protected RegistryBase()
        {
            _options = new DependencyRegistryOptions();
        }

        public bool IsRegistered<T>(string name = null)
        {
            return IsRegistered(typeof(T), name);
        }

        public abstract bool IsRegistered(Type typeToCheck, string name = null);

        public IDependencyRegistry Name(string name)
        {
            _options.Name = name;
            return this;
        }

        public IDependencyRegistry Singleton(object instance = null)
        {
            _options.Singleton = true;
            _options.SingletonInstance = instance;
            return this;
        }

        public IDependencyRegistry From(Type from)
        {
            _options.From = from;
            return this;
        }

        public IDependencyRegistry To(Type to)
        {
            _options.To = to;
            return this;
        }

        public IDependencyRegistry ConstuctorParameters(params object[] constuctorParameters)
        {
            _options.ConstructorParameters = constuctorParameters;
            return this;
        }

        public abstract IDependencyRegistry Register(string name = null);

        public IDependencyRegistry Register<TFrom, TTo>(string name = null)
        {
            return Register(typeof(TFrom), typeof(TTo), name);
        }

        public abstract IDependencyRegistry Register(Type from, Type to, string name = null);


        protected void SetName(string name)
        {
            if (!string.IsNullOrEmpty(name))
                _options.Name = name;
        }

        protected void ResetRegisterOptions()
        {
            _options = new DependencyRegistryOptions();
        }
    }
}
