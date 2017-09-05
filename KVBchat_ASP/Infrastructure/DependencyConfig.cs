using Autofac;
using Autofac.Integration.Mvc;
using BusinessLogic;
using DataAccess;
using KVBchat_ASP.Infrastructure.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KVBchat_ASP.Infrastructure
{
    public static class DependencyConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<KVBchat_ASP_DependencyModule>();
            builder.RegisterModule<DataAccessDependencyModule>();
            builder.RegisterModule<BusinessLogicDependencyModule>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}