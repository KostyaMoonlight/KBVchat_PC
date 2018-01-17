using Autofac;
using BusinessLogic.Service;
using BusinessLogic.Service.Base;
using GoogleDriveAPI.Service;
using GoogleDriveAPI.Service.Base;
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

            builder.RegisterType(typeof(FriendService))
                .As(typeof(IFriendService))
                .InstancePerRequest();

            builder.RegisterType(typeof(AuthenticationService))
                .As(typeof(IAuthenticationService))
                .InstancePerRequest();

            builder.RegisterType(typeof(GoogleDriveApiService))
                .As(typeof(IGoogleDriveApiService))
                .InstancePerRequest();

            builder.RegisterType(typeof(BlackjackService))
                .As(typeof(IBlackjackService))
                .InstancePerRequest();

            builder.RegisterType(typeof(PokerService))
               .As(typeof(IPokerService))
               .InstancePerRequest();
        }
    }
}
