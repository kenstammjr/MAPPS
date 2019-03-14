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
    class CollectUsers : SPJobDefinition {


        const string TIMER_JOB_NAME = "MAPPS - Collect Users";

        public CollectUsers()
            : base() {
            this.Title = TIMER_JOB_NAME;
        }
        public CollectUsers(string JobName, SPService service, SPServer server, SPJobLockType targetType)
            : base(JobName, service, server, targetType) {
            this.Title = TIMER_JOB_NAME;
        }
        public CollectUsers(string JobName, SPWebApplication WebApplication)
            : base(JobName, WebApplication, null, SPJobLockType.ContentDatabase) {
            this.Title = TIMER_JOB_NAME;
        }

        public override void Execute(Guid targetInstanceId) {
            CollectUserData();
        }

        public void CollectUserData() {
            string summary = "Collect User Data<br>";
            string users = string.Empty;
            int resultsCount = 0;
            int usersAdded = 0;
            if (Setting.KeyValue("TimeJobsEnabled") == "True") { //Ensure TimeJobsEnabled is enabled.
                try {
                    string domain = "";
                    SPSecurity.RunWithElevatedPrivileges(delegate () {
                        SPFarm farm = SPFarm.Local;
                        domain = farm.Properties["MAPPS_DOMAIN"].ToString();
                    });
                    PrincipalContext ctx = new PrincipalContext(ContextType.Domain, domain);
                    UserPrincipal uP = new UserPrincipal(ctx);
                    uP.Name = "*";
                    PrincipalSearcher ps = new PrincipalSearcher();
                    ps.QueryFilter = uP;
                    ((DirectorySearcher)ps.GetUnderlyingSearcher()).PageSize = 500;

                    //PrincipalSearchResult<Principal> result = ps.FindAll();

                    foreach (UserPrincipal p in ps.FindAll()) {
                        resultsCount++;
                        if (!MAPPS.User.ADObjectGuidExists(p.Guid.ToString())) {
                            MAPPS.User user = new MAPPS.User();
                            user.LastName = (p.Surname == null) ? "" : p.Surname;
                            user.FirstName = (p.GivenName == null) ? "" : p.GivenName;
                            user.MiddleInitial = (p.MiddleName == null) ? "" : p.MiddleName;
                            user.UserName = string.Format("{0}\\{1}", domain.ToUpper(), p.SamAccountName.ToLower());
                            user.Email = (p.EmailAddress == null) ? "" : p.EmailAddress;
                            user.ADObjectGuid = p.Guid.ToString();
                            if (user.Insert()) {
                                usersAdded++;
                            }
                        }
                    }
                } catch (Exception ex) {
                    Error.WriteError("CollectUsers", "CollectUsers ", ex);
                }
                DateTime TimeStamp = DateTime.Now;
                Action.Write(string.Format("Collect AD Users Timer Job | Users found: {0}, Users Added: {1}, at {2}", resultsCount.ToString(), usersAdded.ToString(), TimeStamp.ToString()), "SharePoint Timer Service");

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
