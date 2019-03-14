using System;
using System.Data;
using System.Data.SqlClient;

namespace MAPPS
{
    public class Setting
    {

        #region _Private Variables_

        private int _ID;
        private string _Key;
        private string _Value;
        private string _Salt;
        private string _Description;
        private bool _IsPassword;
        private bool _IsMultiline;
        private string _ModifiedBy;
        private DateTime _ModifiedOn;
        private string _ErrorMessage;

        #endregion

        #region _Public Properties_

        public int ID
        {
            get { return _ID; }
        }
        public string Key
        {
            get { return _Key; }
            set { _Key = value; }
        }
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
        public string Salt
        {
            get { return _Salt; }
            set { _Salt = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public bool IsPassword
        {
            get { return _IsPassword; }
            set { _IsPassword = value; }
        }
        public bool IsMultiline
        {
            get { return _IsMultiline; }
            set { _IsMultiline = value; }
        }
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        public DateTime ModifiedOn
        {
            get { return _ModifiedOn; }
            set { _ModifiedOn = value; }
        }
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        #endregion

        #region _Constructors_

        public Setting()
        {
            _ID = 0;
            _Key = string.Empty;
            _Value = string.Empty;
            _Salt = string.Empty;
            _Description = string.Empty;
            _IsPassword = false;
            _IsMultiline = false;
            _ModifiedBy = "System Account";
            _ModifiedOn = new DateTime(1900, 1, 1);
            _ErrorMessage = string.Empty;
        }
        public Setting(int ID)
            : this()
        {
            using (new Impersonator())
            {
                SqlConnection conn = DataSource.Conn();
                string sql = @"SELECT * FROM dbo.Settings WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", ID);
                SqlDataReader sdr;
                try
                {
                    conn.Open();
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        _Salt = sdr["Salt"].ToString();
                        byte[] key = Common.RestoreKey(Common.Decrypt(_Salt));
                        _ID = int.Parse(sdr["ID"].ToString());
                        _Key = sdr["Key"].ToString();
                        _Value = Common.Decrypt(sdr["Value"].ToString(), key);
                        _Description = sdr["Description"].ToString();
                        _IsPassword = bool.Parse(sdr["IsPassword"].ToString());
                        _IsMultiline = bool.Parse(sdr["IsMultiline"].ToString());
                        _ModifiedBy = sdr["ModifiedBy"].ToString();
                        _ModifiedOn = DateTime.Parse(sdr["ModifiedOn"].ToString());
                        _ErrorMessage = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    Error.WriteError(ex);
                    _ErrorMessage = ex.Message;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
            }
        }
        public Setting(string Key)
            : this() {
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                string sql = @"SELECT * FROM dbo.Settings WHERE [Key] = @Key";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Key", Key);
                SqlDataReader sdr;
                try {
                    conn.Open();
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read()) {
                        _Salt = sdr["Salt"].ToString();
                        byte[] key = Common.RestoreKey(Common.Decrypt(_Salt));
                        _ID = int.Parse(sdr["ID"].ToString());
                        _Key = sdr["Key"].ToString();
                        _Value = Common.Decrypt(sdr["Value"].ToString(), key);
                        _Description = sdr["Description"].ToString();
                        _IsPassword = bool.Parse(sdr["IsPassword"].ToString());
                        _IsMultiline = bool.Parse(sdr["IsMultiline"].ToString());
                        _ModifiedBy = sdr["ModifiedBy"].ToString();
                        _ModifiedOn = DateTime.Parse(sdr["ModifiedOn"].ToString());
                        _ErrorMessage = string.Empty;
                    }
                } catch (Exception ex) {
                    Error.WriteError(ex);
                    _ErrorMessage = ex.Message;
                } finally {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
            }
        }

        #endregion

        #region _Private Methods_

        #endregion

        #region _Public Methods_

        public bool Insert()
        {
            bool Successful = false;
            using (new Impersonator())
            {
                SqlConnection conn = DataSource.Conn();
                try
                {
                    string salt = Common.GenerateSalt();
                    byte[] key = Common.RestoreKey(salt);

                    DateTime timeStamp = DateTime.UtcNow;
                    string sql = @"Insert INTO dbo.Settings 
										([Key]
										,Value
                                        ,Salt
                                        ,Description
                                        ,IsPassword
                                        ,IsMultiline
										,ModifiedBy
										,ModifiedOn)
							Values
										(@Key
										,@Value
                                        ,@Salt
                                        ,@Description
                                        ,@IsPassword
                                        ,@IsMultiline
										,@ModifiedBy
										,@ModifiedOn)
							SELECT @ID = SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Key", _Key);
                    cmd.Parameters.AddWithValue("@Value", Common.Encrypt(_Value.ToString(), key));
                    cmd.Parameters.AddWithValue("@Salt", Common.Encrypt(salt));
                    cmd.Parameters.AddWithValue("@Description", _Description);
                    cmd.Parameters.AddWithValue("@IsPassword", _IsPassword);
                    cmd.Parameters.AddWithValue("@IsMultiline", _IsMultiline);
                    cmd.Parameters.AddWithValue("@ModifiedBy", _ModifiedBy);
                    cmd.Parameters.AddWithValue("@ModifiedOn", timeStamp);
                    SqlParameter prmID = cmd.Parameters.Add("@ID", SqlDbType.Int);
                    prmID.Direction = ParameterDirection.Output;
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    cmd.ExecuteNonQuery();
                    _ID = int.Parse(prmID.Value.ToString());
                    Successful = true;
                }
                catch (Exception ex)
                {
                    Error.WriteError(ex);
                    _ErrorMessage = ex.Message;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
            }
            return Successful;
        }
        public bool Update()
        {
            bool Successful = false;
            using (new Impersonator())
            {
                SqlConnection conn = DataSource.Conn();
                try
                {
                    byte[] key = Common.RestoreKey(Common.Decrypt(_Salt));
                    DateTime timeStamp = DateTime.UtcNow;
                    string sql = @"Update dbo.Settings 
										SET [Key] = @Key
										,Value = @Value
                                        ,Description = @Description
                                        ,IsPassword = @IsPassword
                                        ,IsMultiline = @IsMultiline
										,ModifiedBy = @ModifiedBy
										,ModifiedOn = @ModifiedOn
							WHERE ID = @ID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID", _ID);
                    cmd.Parameters.AddWithValue("@Key", _Key);
                    cmd.Parameters.AddWithValue("@Value", Common.Encrypt(_Value.ToString(), key));
                    cmd.Parameters.AddWithValue("@Description", _Description);
                    cmd.Parameters.AddWithValue("@IsPassword", _IsPassword);
                    cmd.Parameters.AddWithValue("@IsMultiline", _IsMultiline);
                    cmd.Parameters.AddWithValue("@ModifiedBy", _ModifiedBy);
                    cmd.Parameters.AddWithValue("@ModifiedOn", timeStamp);
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    cmd.ExecuteNonQuery();
                    Successful = true;
                }
                catch (Exception ex)
                {
                    Error.WriteError(ex);
                    _ErrorMessage = ex.Message;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
            }
            return Successful;
        }
        public bool Delete()
        {
            bool Successful = false;
            using (new Impersonator())
            {
                SqlConnection conn = DataSource.Conn();
                try
                {
                    string sql = "DELETE FROM dbo.Settings WHERE ID = @ID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID", _ID);
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    cmd.ExecuteNonQuery();
                    Successful = true;
                }
                catch (Exception ex)
                {
                    Error.WriteError(ex);
                    _ErrorMessage = ex.Message;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
            }
            return Successful;
        }
        public static DataSet Items()
        {
            DataSet ds = new DataSet();
            using (new Impersonator())
            {
                SqlConnection conn = DataSource.Conn();
                try
                {
                    string sql = "SELECT * FROM dbo.Settings ";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (!bool.Parse(dr["IsPassword"].ToString()))
                            dr["Value"] = Common.Decrypt(dr["Value"].ToString(), Common.RestoreKey(Common.Decrypt(dr["Salt"].ToString())));
                        else
                            dr["Value"] = "********************";
                    }
                }
                catch (Exception ex)
                {
                    Error.WriteError(ex);
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
            }
            return ds;
        }
        public static DataTable DataTable()
        {
            DataTable dt = new DataTable();
            using (new Impersonator())
            {
                SqlConnection conn = DataSource.Conn();
                try
                {
                    string sql = "SELECT * FROM dbo.Settings ";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (!bool.Parse(dr["IsPassword"].ToString()))
                            dr["Value"] = Common.Decrypt(dr["Value"].ToString(), Common.RestoreKey(Common.Decrypt(dr["Salt"].ToString())));
                    }
                }
                catch (Exception ex)
                {
                    Error.WriteError(ex);
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
            }
            return dt;
        }
        public static string KeyValue(string Key)
        {
            string value = string.Empty;
            using (new Impersonator())
            {
                SqlConnection conn = DataSource.Conn();
                try
                {
                    string sql = @"SELECT Value, Salt FROM dbo.Settings WHERE [Key] = @Key";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Key", Key);
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    SqlDataReader sdr;
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        value = Common.Decrypt(sdr["Value"].ToString(), Common.RestoreKey(Common.Decrypt(sdr["Salt"].ToString())));
                    }
                }
                catch (Exception ex)
                {
                    Error.WriteError(ex);
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
            }
            return value;
        }

        /// <summary>
        /// Check that the record does not exist (it is not unique)
        /// </summary>
        /// <param name="name">Key of Settings to check</param>
        /// <param name="currentID">ID of the current record so we exclude that in the check.
        /// This would be an issue on an edit check.</param>
        /// <returns>True = record already exists, False = Does not exists</returns>
        /// 
        public static bool Exists(string key, int currentID)
        {
            bool exists = false;

            using (new Impersonator())
            {
                SqlConnection conn = DataSource.Conn();
                const string sql = @"SELECT Count( ID) AS IDCount 
                                    FROM dbo.Settings 
                                    WHERE ID != @ID and [key] = @key";

                SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@key", key.Trim());
                cmd.Parameters.AddWithValue("@ID", currentID);

                try
                {
                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();

                    if (sdr.Read())
                    {
                        if (int.Parse(sdr["IDCount"].ToString()) > 0)
                        {
                            exists = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Error.WriteError(ex);
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
                return exists;
            }
        }
        #endregion

    }
}
