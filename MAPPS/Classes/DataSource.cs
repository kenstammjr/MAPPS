using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System.Configuration;
using System.Data.SqlClient;

namespace MAPPS {
    /// <summary>
    /// Provides a connection to the database
    /// </summary>
    public class DataSource {

        #region _Constants_

        public const string APPLICATION_NAME = "MAPPS";
        private const string DB_SERVER = "MAPPS_DB_Server";
        public const string DB_CATALOG = "MAPPS";

        #endregion

        #region _Private Variables_
        private static string _ServerName;
        private static string _DatabaseName;

        #endregion

        #region _Constructors_

        static DataSource() {
            _ServerName = DetermineServerName();
            _DatabaseName = DetermineDatabaseName();
        }

        #endregion

        #region _Private Methods_

        private static string DetermineServerName() {
            string dbServer = "";
            SPSecurity.RunWithElevatedPrivileges(delegate () {
                SPFarm farm = SPFarm.Local;
                dbServer = farm.Properties["MAPPS_SQL_SERVER"].ToString();
            });
            if (dbServer == null) { // if still null, use the same database server as the farm
                SPSecurity.RunWithElevatedPrivileges(delegate () {
                    SPWebApplicationBuilder w = new SPWebApplicationBuilder(SPFarm.Local);
                    dbServer = w.DatabaseServer;
                });
            }
            return dbServer;
        }
        private static string DetermineDatabaseName() {
            string dbName = "";
            SPSecurity.RunWithElevatedPrivileges(delegate () {
                SPFarm farm = SPFarm.Local;
                dbName = farm.Properties["MAPPS_SQL_DATABASE"].ToString();
            });
            if (dbName == null) {
                SPSecurity.RunWithElevatedPrivileges(delegate () {
                    dbName = DB_CATALOG;
                });
            }
            return dbName;
        }

        #endregion

        #region _Public Methods_

        /// <summary>
        /// Main DB Connection
        /// </summary>
        /// <returns></returns>
        public static SqlConnection Conn() {
            SqlConnection myConn = new SqlConnection();
            myConn.ConnectionString = string.Format("initial catalog={0};server={1};Integrated Security=SSPI;Persist Security Info=False;Application Name={2}", _DatabaseName, _ServerName, APPLICATION_NAME);
            return myConn;
        }
        public static SqlConnection ConnMaster() {
            SqlConnection myConn = new SqlConnection();
            myConn.ConnectionString = string.Format("initial catalog={0};server={1};Integrated Security=SSPI;Persist Security Info=False;Application Name={2}", "master", _ServerName, APPLICATION_NAME);
            return myConn;
        }

        #endregion

    }

}
