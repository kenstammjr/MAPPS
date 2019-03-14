using System;
using System.Data;
using System.Data.SqlClient;

namespace MAPPS {
    public class Framework {

        private string _InstalledVersion;
        private DateTime _UpdatedOn;
        private const string VERSION = "1.1.0";

        #region _Public Properties_

        public string InstalledVersion {
            get {
                return _InstalledVersion;
            }
        }
        public bool IsDatabaseCurrent {
            get {
                //return true;
                return (VERSION == _InstalledVersion);
            }
        }
        public DateTime UpdatedOn {
            get {
                return _UpdatedOn;
            }
        }

        public Framework() {
            _InstalledVersion = string.Empty;
            _UpdatedOn = new DateTime(1900, 1, 1);
            using (new Impersonator()) {
                using (SqlConnection conn = DataSource.Conn()) {
                    string sql = "SELECT TOP 1 * FROM dbo.TableVersions WHERE Name = 'FrameworkVersion'";
                    using (SqlCommand cmd = new SqlCommand(sql, conn)) {
                        cmd.CommandType = CommandType.Text;

                        try {
                            conn.Open();
                            using (SqlDataReader sdr = cmd.ExecuteReader()) {
                                if (sdr.Read()) {
                                    _InstalledVersion = sdr["Version"].ToString();
                                    _UpdatedOn = DateTime.Parse(sdr["UpdatedOn"].ToString());
                                }
                            }
                        } catch (Exception ex) {
                            Error.WriteError(ex);
                        }
                    }
                }
            }
        }

        public static string RequiredVersion {
            get {
                return VERSION;
            }
        }

        public static bool UpdateVersion() {
            bool Successful = false;
            using (new Impersonator()) {
                using (SqlConnection conn = DataSource.Conn()) {
                    try {
                        string sql = @"IF NOT EXISTS (SELECT * FROM dbo.TableVersions WHERE Name = @Name)
                                        INSERT INTO dbo.TableVersions (Name, Version) VALUES (@Name,  @Version)
                                    IF NOT EXISTS (SELECT * FROM dbo.TableVersions WHERE Name = @Name AND Version = @Version)
                                        UPDATE dbo.TableVersions SET Version = @Version, UpdatedOn = getutcdate() WHERE Name = @Name
                                    
                                    IF EXISTS (SELECT Version FROM dbo.TableVersions WHERE Version = @Version AND Name = @Name)
                                        SELECT 1
                                   ELSE
                                        SELECT 0";
                        using (SqlCommand cmd = new SqlCommand(sql, conn)) {
                            cmd.Parameters.AddWithValue("@Version", VERSION);
                            cmd.Parameters.AddWithValue("@Name", "FrameworkVersion");
                            if (conn.State != ConnectionState.Open) {
                                conn.Open();
                            }
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
        #endregion
    }
}