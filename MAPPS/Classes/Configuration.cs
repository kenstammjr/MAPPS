using System;
using System.Data;
using System.Data.SqlClient;

namespace MAPPS{
    public class Configuration {
        public static string AppSetting(string Key) {
            string setting = string.Empty;
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    string sql = @"SELECT value, salt FROM dbo.Settings WHERE [key] = @Key";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Key", Key);
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    SqlDataReader sdr;
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read()) {
                        setting = Common.Decrypt(sdr["Value"].ToString(), Common.RestoreKey(Common.Decrypt(sdr["Salt"].ToString())));
                    }
                } catch (SqlException sqlex) {
                    Error.WriteError(sqlex);
                } catch (Exception ex) {
                    Error.WriteError(ex);
                } finally {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
            }
            return setting;
        }
        public static string AppMessage(string Code) {
            string setting = string.Empty;
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    string sql = @"SELECT Message FROM dbo.Messages WHERE Number = @Number";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Number", Code);
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    SqlDataReader sdr;
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read()) {
                        setting = sdr["Message"].ToString();
                    }
                } catch (SqlException sqlex) {
                    Error.WriteError(sqlex);
                } catch (Exception ex) {
                    Error.WriteError(ex);
                } finally {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
            }
            return setting;
        }
    }
}
