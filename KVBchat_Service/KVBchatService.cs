using Autofac;
using BusinessLogic.Service.Base;
using KVBchat_Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KVBchat_Service
{
    public partial class KVBchatService : ServiceBase
    {
        CancellationTokenSource cancelTokenSource = null;
        CancellationToken cancellationToken; 

        IContainer container = null;
        IUserService userService = null;
        IMessageService messageService = null;

        public KVBchatService()
        {
            InitializeComponent();

            container = DependencyConfig.GetContainer();
            var scope = container.BeginLifetimeScope();
            userService = scope.Resolve<IUserService>();
            messageService = scope.Resolve<IMessageService>();
            cancelTokenSource = new CancellationTokenSource();
            cancellationToken = cancelTokenSource.Token;

        }

        protected override void OnStart(string[] args)
        {
            AddLog("Start");

            Task task = new Task(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var messages = messageService.GetUnreadMessages();
                    userService.UserNotification(messages);
                    Thread.Sleep(1000);
                }
            });
        }

        protected override void OnStop()
        {
            cancelTokenSource.Cancel();
            AddLog("Stop");
        }

        public void AddLog(string log)
        {
            try
            {
                if (!EventLog.SourceExists("KVBchatService"))
                {
                    EventLog.CreateEventSource("KVBchatService", $"KVBchatService{log}");
                }
                eventLog1.Source = "KVBchatService";
                eventLog1.WriteEntry(log);
            }
            catch { }
        }
    }
}
