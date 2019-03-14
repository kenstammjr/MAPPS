using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;

namespace MAPPS {
    public class Error {

        #region _Private Variables_

        private int _ID;
        private DateTime _DateOccurred;
        private string _Class;
        private string _Method;
        private string _RecordID;
        private string _UserName;
        private string _UserMachineIP;
        private string _ServerName;
        private string _ExceptionMessage;
        private string _ExceptionType;
        private string _ExceptionSource;
        private string _ExceptionStackTrace;
        private string _Comment;
        private string _ErrorMessage;

        #endregion

        #region _Public Properties_

        public int ID {
            get { return _ID; }
        }
        public DateTime DateOccurredLocal {
            get { return _DateOccurred.ToLocalTime(); }
        }
        public DateTime DateOccurred {
            get { return _DateOccurred; }
            set { _DateOccurred = value; }
        }
        public string Class {
            get { return _Class; }
            set { _Class = value; }
        }
        public string Method {
            get { return _Method; }
            set { _Method = value; }
        }
        public string RecordID {
            get { return _RecordID; }
            set { _RecordID = value; }
        }
        public string UserName {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public string UserMachineIP {
            get { return _UserMachineIP; }
            set { _UserMachineIP = value; }
        }
        public string ServerName {
            get { return _ServerName; }
            set { _ServerName = value; }
        }
        public string ExceptionMessage {
            get { return _ExceptionMessage; }
            set { _ExceptionMessage = value; }
        }
        public string ExceptionType {
            get { return _ExceptionType; }
            set { _ExceptionType = value; }
        }
        public string ExceptionSource {
            get { return _ExceptionSource; }
            set { _ExceptionSource = value; }
        }
        public string ExceptionStackTrace {
            get { return _ExceptionStackTrace; }
            set { _ExceptionStackTrace = value; }
        }
        public string Comment {
            get { return _Comment; }
            set { _Comment = value; }
        }
        public string ErrorMessage {
            get { return _ErrorMessage; }
        }

        #endregion

        #region _Constructors_

        public Error() {
            _ID = 0;
            _DateOccurred = new DateTime(1900, 1, 1);
            _Class = string.Empty;
            _Method = string.Empty;
            _RecordID = string.Empty;
            _UserName = string.Empty;
            _UserMachineIP = string.Empty;
            _ServerName = string.Empty;
            _ExceptionMessage = string.Empty;
            _ExceptionType = string.Empty;
            _ExceptionSource = string.Empty;
            _ExceptionStackTrace = string.Empty;
            _Comment = string.Empty;
            _ErrorMessage = string.Empty;
        }
        public Error(int ID)
            : this() {
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                string sql = "SELECT * FROM dbo.Errors WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", ID);
                SqlDataReader sdr;
                try {
                    conn.Open();
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read()) {
                        _ID = int.Parse(sdr["ID"].ToString());
                        _DateOccurred = DateTime.Parse(sdr["DateOccurred"].ToString());
                        _Class = sdr["Class"].ToString();
                        _Method = sdr["Method"].ToString();
                        _RecordID = sdr["RecordID"].ToString();
                        _UserName = sdr["UserName"].ToString();
                        _UserMachineIP = sdr["UserMachineIP"].ToString();
                        _ServerName = sdr["ServerName"].ToString();
                        _ExceptionMessage = sdr["ExceptionMessage"].ToString();
                        _ExceptionType = sdr["ExceptionType"].ToString();
                        _ExceptionSource = sdr["ExceptionSource"].ToString();
                        _ExceptionStackTrace = sdr["ExceptionStackTrace"].ToString();
                        _Comment = sdr["Comment"].ToString();
                        _ErrorMessage = string.Empty;
                    }
                } catch (SqlException sqlex) {
                    Error.WriteError(sqlex);
                    _ErrorMessage = sqlex.Message;
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

        /// <summary>
        /// Created At display: Created at mm/dd/yyy h:mm AM/PM (local) createdByUser
        /// </summary>
        /// <example></example>
        /// <param name="web"></param>
        /// <returns>string formatted version of the Created At.</returns>
        public string GetCreatedAt() {
            return "Created at " + this.DateOccurred.ToLocalTime().ToString("MM/dd/yyyy h:mm tt") + " by " + this.UserName;
        }

        public bool Insert() {
            bool Successful = false;
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    DateTime timeStamp = DateTime.Now;
                    string sql = @"Insert INTO dbo.Errors 
										(DateOccurred
										,Class
										,Method
										,RecordID
										,UserName
										,UserMachineIP
										,ServerName
										,ExceptionMessage
										,ExceptionType
										,ExceptionSource
										,ExceptionStackTrace
										,Comment)
							Values
										(@DateOccurred
										,@Class
										,@Method
										,@RecordID
										,@UserName
										,@UserMachineIP
										,@ServerName
										,@ExceptionMessage
										,@ExceptionType
										,@ExceptionSource
										,@ExceptionStackTrace
										,@Comment)
							SELECT @ID = SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@DateOccurred", timeStamp);
                    cmd.Parameters.AddWithValue("@Class", _Class);
                    cmd.Parameters.AddWithValue("@Method", _Method);
                    cmd.Parameters.AddWithValue("@RecordID", _RecordID);
                    cmd.Parameters.AddWithValue("@UserName", _UserName);
                    cmd.Parameters.AddWithValue("@UserMachineIP", _UserMachineIP);
                    cmd.Parameters.AddWithValue("@ServerName", _ServerName);
                    cmd.Parameters.AddWithValue("@ExceptionMessage", _ExceptionMessage);
                    cmd.Parameters.AddWithValue("@ExceptionType", _ExceptionType);
                    cmd.Parameters.AddWithValue("@ExceptionSource", _ExceptionSource);
                    cmd.Parameters.AddWithValue("@ExceptionStackTrace", _ExceptionStackTrace);
                    cmd.Parameters.AddWithValue("@Comment", _Comment);
                    SqlParameter prmID = cmd.Parameters.Add("@ID", SqlDbType.Int);
                    prmID.Direction = ParameterDirection.Output;
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    cmd.ExecuteNonQuery();
                    _ID = int.Parse(prmID.Value.ToString());
                    Successful = true;
                    //Microsoft.Office.Server.Diagnostics.PortalLog.LogString("MAPPS Products Error: {0}, Stack: {1} Class: {2} Method: {3}", _ExceptionMessage, _ExceptionStackTrace, _Class, _Method);
                } catch (SqlException sqlex) {
                    _ErrorMessage = sqlex.Message;
                } catch (Exception ex) {
                    _ErrorMessage = ex.Message;
                } finally {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
            }
            return Successful;
        }
        public bool Update() {
            bool Successful = false;
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    DateTime timeStamp = DateTime.UtcNow;
                    string sql = @"Update dbo.Errors 
										DateOccurred = @DateOccurred
										,Class = @Class
										,Method = @Method
										,RecordID = @RecordID
										,UserName = @UserName
										,UserMachineIP = @UserMachineIP
										,ServerName = @ServerName
										,ExceptionMessage = @ExceptionMessage
										,ExceptionType = @ExceptionType
										,ExceptionSource = @ExceptionSource
										,ExceptionStackTrace = @ExceptionStackTrace
										,Comment = @Comment																			
							WHERE ID = @ID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@DateOccurred", _DateOccurred);
                    cmd.Parameters.AddWithValue("@Class", _Class);
                    cmd.Parameters.AddWithValue("@Method", _Method);
                    cmd.Parameters.AddWithValue("@RecordID", _RecordID);
                    cmd.Parameters.AddWithValue("@UserName", _UserName);
                    cmd.Parameters.AddWithValue("@UserMachineIP", _UserMachineIP);
                    cmd.Parameters.AddWithValue("@ServerName", _ServerName);
                    cmd.Parameters.AddWithValue("@ExceptionMessage", _ExceptionMessage);
                    cmd.Parameters.AddWithValue("@ExceptionType", _ExceptionType);
                    cmd.Parameters.AddWithValue("@ExceptionSource", _ExceptionSource);
                    cmd.Parameters.AddWithValue("@ExceptionStackTrace", _ExceptionStackTrace);
                    cmd.Parameters.AddWithValue("@Comment", _Comment);
                    cmd.Parameters.AddWithValue("@ID", _ID);
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    cmd.ExecuteNonQuery();
                    Successful = true;
                } catch (SqlException sqlex) {
                    _ErrorMessage = sqlex.Message;
                } catch (Exception ex) {
                    _ErrorMessage = ex.Message;
                } finally {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
            }
            return Successful;
        }
        public bool Delete() {
            bool Successful = false;
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    string sql = "DELETE FROM dbo.Errors WHERE ID = @ID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID", _ID);
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    cmd.ExecuteNonQuery();
                    Successful = true;
                } catch (SqlException sqlex) {
                    _ErrorMessage = sqlex.Message;
                } catch (Exception ex) {
                    _ErrorMessage = ex.Message;
                } finally {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
            }
            return Successful;
        }
        public static DataSet Items() {
            DataSet ds = new DataSet();
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    string sql = "SELECT TOP " + Setting.KeyValue("MaxRecords") +" * FROM dbo.Errors ";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                } catch (Exception ex) {
                    WriteError(ex);
                } finally {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
            }
            return ds;
        
        }
        public static DataSet Items(string searchText)
        {
            DataSet ds = new DataSet();
            using (new Impersonator())
            {
                SqlConnection conn = DataSource.Conn();
                try
                {
                    string sql = @"SELECT TOP " + Setting.KeyValue("MaxRecords") + @" * 
                                   FROM dbo.Errors ";
                    if (!searchText.IsNullOrWhiteSpace())
                        sql += @" WHERE (
                                          (Class like @searchText)
                                            OR (Method like @searchText)
                                            OR (ServerName like @searchText)
                                            OR (ExceptionMessage like @searchText)
                                            OR (ExceptionType like @searchText)
                                        ) ";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    if (!searchText.IsNullOrWhiteSpace())
                        cmd.Parameters.AddWithValue("@searchText", "%"+searchText +"%");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
                catch (Exception ex)
                {
                    WriteError(ex);
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
            }
            return ds;

        }
        public static DataSet Items(DateTime Begining) {
            DataSet ds = new DataSet();
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    string sql = @"SELECT   Class
                                        ,COUNT(*) AS dbo.Errors
                                        FROM Errors 
                                        WHERE dateoccurred >= @Begining
                                        GROUP BY Class";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Begining", Begining);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                } catch {

                } finally {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
            }
            return ds;
        }

        public static bool ShowError(string ContextUserIdentityName) {
            bool show = false;
            MAPPS.User user = new MAPPS.User(ContextUserIdentityName);
            if (user.InRole(RoleType.Manager.ToString()))
                show = true;
            return show;
        }
        public static bool TruncateErrors() {
            bool bDeletedError = false;
            using (new Impersonator()) {
                string sql = string.Empty;
                SqlConnection conn = DataSource.Conn();
                try {
                    sql = @"TRUNCATE TABLE dbo.Errors";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.CommandType = CommandType.Text;

                    if (conn.State != ConnectionState.Open) {
                        conn.Open();
                    }
                    cmd.ExecuteNonQuery();
                    bDeletedError = true;
                } catch (SqlException sqlex) {
                    WriteError(sqlex);
                    bDeletedError = false;
                } catch (Exception ex) {
                    WriteError(ex);
                    bDeletedError = false;
                } finally {
                    if (conn.State != ConnectionState.Closed) {
                        conn.Close();
                    }
                }
            }
            return bDeletedError;
        }
       
        public static bool DeleteErrors() {
            bool bDeletedError = false;
            using (new Impersonator()) {
                string sql = string.Empty;
                SqlConnection conn = DataSource.Conn();
                try {
                    sql = @"DELETE FROM dbo.Errors";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.CommandType = CommandType.Text;

                    if (conn.State != ConnectionState.Open) {
                        conn.Open();
                    }
                    cmd.ExecuteNonQuery();
                    bDeletedError = true;
                } catch (SqlException sqlex) {
                    WriteError(sqlex);
                    bDeletedError = false;
                } catch (Exception ex) {
                    WriteError(ex);
                    bDeletedError = false;
                } finally {
                    if (conn.State != ConnectionState.Closed) {
                        conn.Close();
                    }
                }
            }
            return bDeletedError;
        }

        public static bool WriteError(Exception ex) {
            bool bWroteError = false;

            StackTrace st = new StackTrace();
            string sObjectClassName = st.GetFrame(1).GetMethod().DeclaringType.ToString();
            string sClassMethod = st.GetFrame(1).GetMethod().Name;

            IPHostEntry ipHost = Dns.GetHostEntry(System.Environment.MachineName);
            IPAddress ip = ipHost.AddressList[0];

            bWroteError = WriteError(sObjectClassName, sClassMethod, "System", ip.ToString(), System.Environment.MachineName, "", "", ex);

            return bWroteError;

        }
        public static bool WriteError(string sObjectClassName, string sClassMethod, Exception exLogError) {
            bool bWroteError = false;

            IPHostEntry ipHost = Dns.GetHostEntry(System.Environment.MachineName);
            IPAddress ip = ipHost.AddressList[0];

            bWroteError = WriteError(sObjectClassName, sClassMethod, "System", ip.ToString(), System.Environment.MachineName, "", "", exLogError);

            return bWroteError;
        }
        public static bool WriteError(string sObjectClassName, string sClassMethod, string sUserName, string sUserMachineIP, string sServerName, Exception exLogError) {
            bool bWroteError = false;

            bWroteError = WriteError(sObjectClassName, sClassMethod, sUserName, sUserMachineIP, sServerName, "", "", exLogError);

            return bWroteError;
        }
        public static bool WriteError(string sObjectClassName, string sClassMethod, string sUserName, string sUserMachineIP, string sServerName, string sRecordID, Exception exLogError) {
            bool bWroteError = false;

            bWroteError = WriteError(sObjectClassName, sClassMethod, sUserName, sUserMachineIP, sServerName, sRecordID, "", exLogError);

            return bWroteError;
        }
        public static bool WriteError(string sObjectClassName, string sClassMethod, string sUserName, string sUserMachineIP, string sServerName, string sRecordID, string sComment, Exception exLogError) {
            bool bWroteError = false;

            Error e = new Error();
            e.Class = sObjectClassName;
            e.Method = sClassMethod;
            e.RecordID = sRecordID;
            e.UserName = sUserName;
            e.UserMachineIP = sUserMachineIP;
            e.ServerName = sServerName;
            e.ExceptionMessage = exLogError.Message == null ? string.Empty : exLogError.Message;
            e.ExceptionType = exLogError.GetType().ToString();
            e.ExceptionSource = exLogError.Source == null ? string.Empty : exLogError.Source;
            e.ExceptionStackTrace = exLogError.StackTrace == null ? string.Empty : exLogError.StackTrace;
            e.Comment = sComment;

            bWroteError = e.Insert();

            return bWroteError;
        }

        #endregion
    }
}
