using Autofac;
using DataAccess.Context;
using DataAccess.Repositories;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DataAccessDependencyModule
        : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<KVBchatDbContext>()
                .Named<DbContext>("DataContext")
                .InstancePerRequest();


            builder.RegisterType(typeof(GroupRepository)).As(typeof(IGroupRepository))
                .WithParameter((pi, c) => pi.Name == "context",
                   (pi, c) => (KVBchatDbContext)c.ResolveNamed<DbContext>("DataContext"))
                .InstancePerRequest();

            builder.RegisterType(typeof(MessageRepository)).As(typeof(IMessageRepository))
            .WithParameter((pi, c) => pi.Name == "context",
               (pi, c) => (KVBchatDbContext)c.ResolveNamed<DbContext>("DataContext"))
            .InstancePerRequest();

            builder.RegisterType(typeof(UserRepository)).As(typeof(IUserRepository))
                .WithParameter((pi, c) => pi.Name == "context",
                   (pi, c) => (KVBchatDbContext)c.ResolveNamed<DbContext>("DataContext"))
                .InstancePerRequest();

            builder.RegisterType(typeof(AuthenticationRepository)).As(typeof(IAuthenticationRepository))
    .WithParameter((pi, c) => pi.Name == "context",
       (pi, c) => (KVBchatDbContext)c.ResolveNamed<DbContext>("DataContext"))
    .InstancePerRequest();

        }
    }
}
