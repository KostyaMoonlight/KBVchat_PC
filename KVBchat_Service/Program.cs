using DataAccess.Initializers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace KVBchat_Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Database.SetInitializer(new KVBchatDbInitializer());

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new KVBchatService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
