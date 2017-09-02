using Autofac;
using BusinessLogic.Service;
using BusinessLogic.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class BusinessLogicDependencyModule
        : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(GroupService))
                .As(typeof(IGroupService))
                .InstancePerRequest();

            builder.RegisterType(typeof(MessageService))
                .As(typeof(IMessageService))
                .InstancePerRequest();

            builder.RegisterType(typeof(UserService))
                .As(typeof(IUserService))
                .InstancePerRequest();
        }
    }
}
