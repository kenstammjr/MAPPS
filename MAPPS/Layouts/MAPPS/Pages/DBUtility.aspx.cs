using System;
using Microsoft.SharePoint.WebControls;
using System.IO;
using System.Reflection;
using System.Web.UI;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint;

namespace MAPPS.Pages {
    public partial class DBUtility : PageBase {


        protected const string PAGE_TITLE = "Database Utility";
        protected const string PAGE_DESCRIPTION = "Create/Update the MAPPS Data System ";
        const string DATE_FORMAT = "MM/dd/yyyy  HH:mm tt";
        const string SQL_FILE_LOCATION = "MAPPS.SQL";
        public const string PAGE_URL = BASE_PAGES_URL + "DBUtility.aspx";
        public string _ErrorMessage = string.Empty;

        protected override void Page_Load(object sender, EventArgs e) {
            try {
                Framework myFramework = new Framework();
                DateTime utcDateTime;
                string currentVersion = myFramework.InstalledVersion;
                string dateTime = string.Empty;

                if (!IsPostBack) {

                    if (string.IsNullOrEmpty(currentVersion)) {
                        // Status
                        lblDBStatus.Text = "<b>Not Installed</b>";
                        lblDBStatusMsg.Text = "<br><br>Run the configuration utility to create a new database.";
                        // Version
                        lblDBVersion.Text = "Not Installed";
                        lblDBVersion.CssClass = "ms-formvalidation";
                        lblDBVersionMsg.Visible = false;
                        // LastUpdate
                        lblDBUpdated.Text = "Not Installed";

                        btnRunConfigurationUtility.Visible = true;

                    } else if (currentVersion != Framework.RequiredVersion) {
                        dateTime = myFramework.UpdatedOn.ToString();
                        utcDateTime = DateTime.Parse(dateTime);
                        // Status
                        lblDBStatus.Text = "<b>Update Required</b>";
                        lblDBStatusMsg.Text = string.Format("<br><br>Run the configuration utility to upgrade the database to version {0}.", Framework.RequiredVersion);
                        // Version
                        lblDBVersion.Text = currentVersion;
                        lblDBVersion.CssClass = "ms-formvalidation";
                        lblDBVersionMsg.Visible = false;
                        // LastUpdate
                        lblDBUpdated.Text = System.TimeZone.CurrentTimeZone.ToLocalTime(utcDateTime).ToString();

                        btnRunConfigurationUtility.Visible = true;

                    } else {
                        dateTime = myFramework.UpdatedOn.ToString();
                        utcDateTime = DateTime.Parse(dateTime);
                        // Status
                        lblDBStatus.Text = "<font color=green><b>Current</b></font>";
                        lblDBStatusMsg.Text = string.Empty; ;
                        // Version
                        lblDBVersion.Text = currentVersion;
                        //lblDBVersion.CssClass = "ms-formvalidation";
                        lblDBVersionMsg.Visible = false;
                        // LastUpdate
                        lblDBUpdated.Text = System.TimeZone.CurrentTimeZone.ToLocalTime(utcDateTime).ToString();

                        btnRunConfigurationUtility.Visible = false;
                    }
                }
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (SPFarm.Local.CurrentUserIsAdministrator())
                    lblErrorMessage.Visible = true;
                lblErrorMessage.Text = ex.ToString();
            }
        }

        #region _Methods_

        protected override void AddRibbonTab() {
            try {
                base.AddRibbonTab();
                Ribbon.CommandUIVisible = false;
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }
        protected override void Fill() { }
        public bool DeployDataSystem() {
            bool success = false;
            string[] TableList = new string[]
            {
                "TableVersions",
                "Activity",
                "Errors",
                "Menus",
                "MenuNodes",
                "MenuAdmins",
                "MenuNodeAdmins",
                "Messages",
                "Modules",
                "Roles",
                "SecurityGroups",
                "Users",
                "SecurityGroupMemberships",
                "Settings",
                "Tabs",
                "DROP_PROC_UpdateTabDisplayIndex",
                "CREATE_PROC_UpdateTabDisplayIndex",
                "TimeZones",
                "Transactions",
                "Applications",
                "DROP_PROC_UpdateApplicationDisplayIndex",
                "CREATE_PROC_UpdateApplicationDisplayIndex",
                "ServerEnvironments",
                "ServerFunctions",
                "ServerPorts",
                "ServerStatuses",
                "ServerTypes",
                "ServerVersions",
                "Servers"
            };
            string ErrorList = string.Empty;

            success = DataSystem.CreateDatabase();
            lblErrorMessage.Text += "DataSystem.CreateDatabase(): " + success.ToString();
            if (success) {
                foreach (string tbl in TableList) {
                    lblErrorMessage.Text += "tbl in TableList: " + tbl;
                    if (!ExecSqlFile(string.Format("{0}.{1}.sql", SQL_FILE_LOCATION, tbl))) {
                        success = false;
                        ErrorList += string.Format("\\t{0}\\n", tbl);
                    }
                }
                if (success) {
                    if (!Framework.UpdateVersion()) {
                        success = false;
                        ErrorList += "\\Framework Version\\n";
                    }
                }
            }
            if (!success) {
                int Errors = ErrorList.Replace("\\n", "|").Split('|').Length;
                _ErrorMessage = string.Format("Database creation/update failed {0}: \\n\\n{1}\\nDelete any existing constraints for {2}\\nand run the configuration utility again.", Errors > 2 ? "tables" : "table", ErrorList, Errors > 2 ? "these tables" : "this table");
            } else {
                try {
                    //   SaveCurrentUser();
                } catch (Exception ex) {
                    MAPPS.Error.WriteError(ex);
                    if (SPFarm.Local.CurrentUserIsAdministrator())
                        lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = ex.ToString();
                }
            }
            return success;
        }

        protected string GetSQLResource(string Name) {
            string sql = string.Empty;
            try {
                Assembly asm = Assembly.GetExecutingAssembly();
                Stream stream = asm.GetManifestResourceStream(Name);
                StreamReader reader = new StreamReader(stream);
                sql = reader.ReadToEnd();
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (SPFarm.Local.CurrentUserIsAdministrator())
                    lblErrorMessage.Visible = true;
                lblErrorMessage.Text = ex.ToString();
            }
            return sql;
        }
        protected bool ExecSqlFile(string tbl) {
            string TableName = tbl.Replace("MAPPS.SQL.", string.Empty);
            bool success = false;
            try {
                string sql = GetSQLResource(tbl);
                success = DataSystem.ExecSql(sql);
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
            return success;
        }

        #endregion

        #region _Events_

        protected void btnCloseBottom_Click(object sender, EventArgs e) {
            Response.Redirect(string.Format("{0}/{1}", SPContext.Current.Web.Url, Pages.Administration.PAGE_URL, ItemID), false);
        }
        protected void btnRunConfigurationUtility_Click(object sender, EventArgs e) {
            lblErrorMessage.Visible = true;
            lblErrorMessage.Text = string.Empty;
            DateTime utcDateTime;
            string dateTime = string.Empty;
            if (DeployDataSystem()) {
                Framework myFramework = new Framework();
                dateTime = myFramework.UpdatedOn.ToString();
                utcDateTime = DateTime.Parse(dateTime);
                dateTime = myFramework.UpdatedOn.ToString();
                utcDateTime = DateTime.Parse(dateTime);
                // Status
                lblDBStatus.Text = "<font color=green><b>Current</b></font>";
                lblDBStatusMsg.Text = string.Empty; ;
                // Version
                lblDBVersion.Text = myFramework.InstalledVersion;
                lblDBVersion.CssClass = "ms-vb";
                lblDBVersionMsg.Visible = false;
                // LastUpdate
                lblDBUpdated.Text = System.TimeZone.CurrentTimeZone.ToLocalTime(utcDateTime).ToString();

                btnRunConfigurationUtility.Visible = false;

            } else {

                // Status
                lblDBStatus.Text = "<font color=red><b>Update Failed</b></font>";
                lblDBStatusMsg.Text = string.Format("<br><br><font color=red><b>Resolve the error and run the configuration utility to upgrade the database to version {0}.</b></font>", Framework.RequiredVersion);
            }
        }

        #endregion
    }
}

