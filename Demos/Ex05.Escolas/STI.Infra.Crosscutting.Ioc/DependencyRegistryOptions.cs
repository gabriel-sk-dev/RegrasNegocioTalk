using System;

namespace STI.Infra.Crosscutting.Ioc
{
    public sealed class DependencyRegistryOptions
    {
        public string Name { get; set; }
        public Type From { get; set; }
        public Type To { get; set; }
        public bool Singleton { get; set; }
        public object SingletonInstance { get; set; }
        public object[] ConstructorParameters { get; set; }

        public override string ToString()
        {
            var from = From == null ? string.Empty : From.ToString();
            var to = To == null ? string.Empty : To.ToString();
            return $"Name: {Name ?? string.Empty}, From: {from}, To: {to}, Singleton: {Singleton}";
        }
    }
}
