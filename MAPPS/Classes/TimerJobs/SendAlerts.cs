using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace MAPPS.TimerJobs {
    class SendAlerts : SPJobDefinition {


        const string TIMER_JOB_NAME = "MAPPS - Send Alerts";

        public SendAlerts()
            : base() {
            this.Title = TIMER_JOB_NAME;
        }
        public SendAlerts(string JobName, SPService service, SPServer server, SPJobLockType targetType)
            : base(JobName, service, server, targetType) {
            this.Title = TIMER_JOB_NAME;
        }
        public SendAlerts(string JobName, SPWebApplication WebApplication)
            : base(JobName, WebApplication, null, SPJobLockType.ContentDatabase) {
            this.Title = TIMER_JOB_NAME;
        }

        public override void Execute(Guid targetInstanceId) {
            SendOwnerAlerts();
        }

        public void SendOwnerAlerts() {
            string summary = "Send Owner Alerts<br>";
            if (Setting.KeyValue("TimeJobsEnabled") == "True") { //Ensure TimeJobsEnabled is enabled.
                try {
                    // DO WORK
                } catch (Exception ex) {
                    Error.WriteError("SendAlerts", "SendOwnerAlerts ", ex);
                }

                Action.Write(summary, "SharePoint Timer Service");

            } else {
                Transaction disabled = new Transaction();
                disabled.Action = "Timer Jobs Not Enabled";
                disabled.Category = "System";
                disabled.Type = "TimerJob";
                disabled.Insert();
                summary += string.Format("SPTimerJob: {0} Skipped. [TimeJobsEnabled=false]", TIMER_JOB_NAME);


                Action.Write(summary, "SharePoint Timer Service");

            }
        }
    }
}
