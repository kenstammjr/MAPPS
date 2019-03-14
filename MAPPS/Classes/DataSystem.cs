using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace MAPPS {
    public class DataSystem {

        private const string SQL_FILE_LOCATION = "MAPPS.SQL";
        private static string _ErrorMessage;

        /// <summary>
        /// Executes a SQL statement against the configured AMS_DB_Server
        /// </summary>
        /// <param name="SQL">Command Text</param>
        /// <returns></returns>
        public static bool ExecSql(string sql) {
            bool Successful = false;
            if (string.IsNullOrEmpty(sql.Trim())) {
                return Successful;//send them packing it is invalid
            }
            using (new Impersonator())
            using (SqlConnection conn = DataSource.Conn()) {
                try {
                    using (SqlCommand cmd = new SqlCommand(sql, conn)) {
                        if (conn.State != ConnectionState.Open)
                            conn.Open();

                        cmd.ExecuteNonQuery();
                        Successful = true;
                    }
                } catch (Exception ex) {
                    Error.WriteError(ex);

                }
            }
            return Successful;
        }
        public static bool CreateDatabase() {
            bool Successful = false;
            using (new Impersonator()) {
                using (SqlConnection conn = DataSource.ConnMaster()) {
                    try {
                        //string X = conn.ConnectionString;
                        
                        //conn.ConnectionString = X.Replace("catalog=" + DataSource.DB_CATALOG, "catalog=MASTER");

                        string sql = string.Empty;
                        sql = string.Format("IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'{0}') CREATE DATABASE [{0}]", DataSource.DB_CATALOG);
                        using (SqlCommand cmd = new SqlCommand(sql, conn)) {
                            if (conn.State != ConnectionState.Open)
                                conn.Open();
                            cmd.ExecuteNonQuery();

                            DateTime delay = DateTime.Now.AddSeconds(20);
                            //delay for database to be ready (up to 20 seconds)
                            do {
                            } while (!DatabaseExists() && delay > DateTime.Now);
                            Successful = DatabaseExists();
                        }
                    } catch (Exception ex) {
                        Error.WriteError(ex);
                    }
                }
            }
            return Successful;
        }
        public static bool DatabaseExists() {
            bool Successful = false;
            using (new Impersonator()) {
                using (SqlConnection conn = DataSource.Conn()) {
                    try {
                        string sql = string.Format("IF EXISTS (SELECT name FROM sys.databases WHERE name = N'{0}') SELECT 1 ELSE SELECT 0", DataSource.DB_CATALOG);
                        using (SqlCommand cmd = new SqlCommand(sql, conn)) {
                            if (conn.State != ConnectionState.Open)
                                conn.Open();
                            int RecCnt = int.Parse(cmd.ExecuteScalar().ToString());
                            if (RecCnt == 1)
                                Successful = true;
                        }
                    } catch (Exception ex) {
                        Error.WriteError(ex);
                    }
                }
            }
            return Successful;
        }
        public static bool Deploy() {
            bool success = false;
            string[] TableList = new string[] 
            {
                //Tables
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
            if (success) {
                foreach (string tbl in TableList) {
                    if (!ExecSqlFile(string.Format("{0}.{1}.sql", SQL_FILE_LOCATION, tbl))) {
                        success = false;
                        ErrorList += string.Format("\\tt_{0}\\n", tbl);
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
                _ErrorMessage = string.Format("Failed to update database {0}: \\n\\n{1}\\nDelete any existing constraints for {2}\\nand run the configuration utility again.", Errors > 2 ? "tables" : "table", ErrorList, Errors > 2 ? "these tables" : "this table");
            }
            return success;
        }

        protected static string GetSQLResource(string Name) {
            string sql = string.Empty;
            try {
                Assembly asm = Assembly.GetExecutingAssembly();
                Stream stream = asm.GetManifestResourceStream(Name);
                StreamReader reader = new StreamReader(stream);
                sql = reader.ReadToEnd();
            } catch (Exception ex) {
                Error.WriteError(ex);
            }
            return sql;
        }
        protected static bool ExecSqlFile(string tbl) {
            string TableName = tbl.Replace(SQL_FILE_LOCATION, string.Empty);
            bool success = false;
            try {
                string sql = GetSQLResource(tbl);
                success = DataSystem.ExecSql(sql);
            } catch (Exception ex) {
                Error.WriteError(ex);
            }
            return success;
        }
    }
}
