using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace MAPPS.Features.Farm {

    [Guid("04f920c2-c51f-4b50-ac76-3f36b974c4d3")]
    public class FarmEventReceiver : SPFeatureReceiver {
        const string SEND_ALERTS_TIMER_JOB = "MAPPS - Send Alerts";
        const string COLLECT_SERVERS_TIMER_JOB = "MAPPS - Collect Servers";
        const string COLLECT_USERS_TIMER_JOB = "MAPPS - Collect Users";

        public override void FeatureActivated(SPFeatureReceiverProperties properties) {
            SPService svc = (SPService)properties.Feature.Parent;
            SPJobDefinitionCollection defs = svc.JobDefinitions;
            foreach (SPJobDefinition job in defs) {
                try {
                    if (job.Name == SEND_ALERTS_TIMER_JOB) {
                        job.Delete();
                    }
                    if (job.Name == COLLECT_SERVERS_TIMER_JOB) {
                        job.Delete();
                    }
                    if (job.Name == COLLECT_USERS_TIMER_JOB) {
                        job.Delete();
                    }

                } catch (Exception ex) {
                    Error.WriteError("MAPPS.TimerJobs", "FeatureActivated: Delete " + job.Name, ex);
                }
            }
            try {
                MAPPS.TimerJobs.SendAlerts sendAlerts = new MAPPS.TimerJobs.SendAlerts(SEND_ALERTS_TIMER_JOB, svc, null, SPJobLockType.Job);
                //sendAlerts.Schedule = SPSchedule.FromString("daily at 01:00:00");
                sendAlerts.Schedule = SPSchedule.FromString("Every 5 minutes between 0 and 59");
                sendAlerts.Update();
                defs.Add(sendAlerts);
                svc.Update();

                MAPPS.TimerJobs.CollectServers collectServers = new MAPPS.TimerJobs.CollectServers(COLLECT_SERVERS_TIMER_JOB, svc, null, SPJobLockType.Job);
                //collectServers.Schedule = SPSchedule.FromString("daily at 01:00:00");
                collectServers.Schedule = SPSchedule.FromString("Every 5 minutes between 0 and 59");
                collectServers.Update();
                defs.Add(collectServers);
                svc.Update();

                MAPPS.TimerJobs.CollectUsers collectUsers = new MAPPS.TimerJobs.CollectUsers(COLLECT_USERS_TIMER_JOB, svc, null, SPJobLockType.Job);
                //collectUsers.Schedule = SPSchedule.FromString("daily at 01:00:00");
                collectUsers.Schedule = SPSchedule.FromString("Every 5 minutes between 0 and 59");
                collectUsers.Update();
                defs.Add(collectUsers);
                svc.Update();


            } catch (Exception ex) {
                Error.WriteError("MAPPS.TimerJobs", "FeatureActivated", ex);
            }
        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties) {
            SPService svc = (SPService)properties.Feature.Parent;
            foreach (SPJobDefinition job in svc.JobDefinitions) {
                try {
                    if (job.Name == SEND_ALERTS_TIMER_JOB) {
                        job.Delete();
                    }
                    if (job.Name == COLLECT_SERVERS_TIMER_JOB) {
                        job.Delete();
                    }
                    if (job.Name == COLLECT_USERS_TIMER_JOB) {
                        job.Delete();
                    }


                } catch (Exception ex) {
                    Error.WriteError("MAPPS.FeatureReceiver", "FeatureDeactivating: Delete " + job.Name, ex);
                }
            }
        }


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
