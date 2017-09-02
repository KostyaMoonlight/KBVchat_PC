using Autofac;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBVchat_PC.Infrastructure.IoC
{
    public class WpfDependencyModule
        :Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(cnt => AutoMapperConfig.GetConfiguration()).SingleInstance();
            builder.Register(cnt => cnt.Resolve<MapperConfiguration>().CreateMapper()).SingleInstance();
        }
    }
}
