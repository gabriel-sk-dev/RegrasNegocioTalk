using System;
using System.Collections.Generic;
using System.Text;

namespace STI.Infra.Crosscutting.Ioc
{
    public interface IDependencyContainer
    {
        IDependencyRegistry Registry { get; set; }
        IDependencyResolver Resolver { get; set; }
        object Container { get; }
    }
}
