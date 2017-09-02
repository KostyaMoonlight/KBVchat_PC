using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace KVBchat_Service
{
    public partial class KVBchatService : ServiceBase
    {

        public KVBchatService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            AddLog("Start");
        }

        protected override void OnStop()
        {
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
