using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBVchat_PC.Infrastructure
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration GetConfiguration()
        {
            var config = new MapperConfiguration(cnf =>
            {
                var autoMapperProfileTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes().Where(p => typeof(Profile).IsAssignableFrom(p) && p.IsPublic && !p.IsAbstract && p.GetConstructor(Type.EmptyTypes) != null));
                var autoMapperProfiles = autoMapperProfileTypes.Select(p => (Profile)Activator.CreateInstance(p));
                foreach (var profile in autoMapperProfiles)
                {
                    cnf.AddProfile(profile);
                }
            });
            return config;
        }

    }
}
