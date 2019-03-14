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
    class CollectServers : SPJobDefinition {


        const string TIMER_JOB_NAME = "MAPPS - Collect Servers";

        public CollectServers()
            : base() {
            this.Title = TIMER_JOB_NAME;
        }
        public CollectServers(string JobName, SPService service, SPServer server, SPJobLockType targetType)
            : base(JobName, service, server, targetType) {
            this.Title = TIMER_JOB_NAME;
        }
        public CollectServers(string JobName, SPWebApplication WebApplication)
            : base(JobName, WebApplication, null, SPJobLockType.ContentDatabase) {
            this.Title = TIMER_JOB_NAME;
        }

        public override void Execute(Guid targetInstanceId) {
            CollectServerData();
        }

        public void CollectServerData() {
            string summary = "Collect Server Data<br>";
            string computers = "";
            int resultsCount = 0;
            int serversAdded = 0;
            if (Setting.KeyValue("TimeJobsEnabled") == "True") { //Ensure TimeJobsEnabled is enabled.
                try {
                    string domain = "";
                    SPSecurity.RunWithElevatedPrivileges(delegate () {
                        SPFarm farm = SPFarm.Local;
                        domain = farm.Properties["MAPPS_DOMAIN"].ToString();
                    });
                    PrincipalContext ctx = new PrincipalContext(ContextType.Domain, domain);
                    ComputerPrincipal cP = new ComputerPrincipal(ctx);
                    cP.Name = "*";
                    PrincipalSearcher ps = new PrincipalSearcher();
                    ps.QueryFilter = cP;
                    PrincipalSearchResult<Principal> result = ps.FindAll();
                    foreach (ComputerPrincipal p in result) {
                        resultsCount++;
                        DirectoryEntry de = (DirectoryEntry)p.GetUnderlyingObject();
                        string os = "";

                        try { os = de.Properties["operatingSystem"].Value.ToString(); } catch { }
                        if (os.Contains("Windows Server") && !MAPPS.Server.Exists(p.Name)) {
                            computers += string.Format("{0}, {1} |", p.Name, os);
                            MAPPS.Server server = new MAPPS.Server();
                            server.Name = p.Name;
                            server.Description = p.Name;
                            server.ServerVersionID = (MAPPS.ServerVersion.Exists(os)) ? new ServerVersion(os).ID : 1;
                            if (server.Insert()) {
                                serversAdded++;
                            }
                        }
                    }
                } catch (Exception ex) {
                    Error.WriteError("CollectServers", "CollectServerData ", ex);
                }

                DateTime TimeStamp = DateTime.Now;
                Action.Write(string.Format("Collect Windows Servers Timer Job | Servers found: {0}, Servers Added: {1}, at {2}", resultsCount.ToString(), serversAdded.ToString(), TimeStamp.ToString()), "SharePoint Timer Service");


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
