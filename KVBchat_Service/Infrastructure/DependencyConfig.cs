using Autofac;
using BusinessLogic;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVBchat_Service.Infrastructure
{

    public static class DependencyConfig
    {
        public static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<DataAccessDependencyModule>();
            builder.RegisterModule<BusinessLogicDependencyModule>();

            var container = builder.Build();
            return container;
        }
    }

}
