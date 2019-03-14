using System;
using System.Data;
using System.Data.SqlClient;

namespace MAPPS {
    public class Action{
        #region _Private Variables_

        private int _ID;
        private string _DisplayName;
        private string _Event;
        private string _Audience;
        private string _CreatedBy;
        private DateTime _CreatedOn;
        private string _ModifiedBy;
        private DateTime _ModifiedOn;
        private string _ErrorMessage;

        #endregion

        #region _Public Properties_

        public int ID {
            get {
                return _ID;
            }
        }
        public string DisplayName {
            get {
                return _DisplayName;
            }
            set {
                _DisplayName = value;
            }
        }
        public string Event {
            get {
                return _Event;
            }
            set {
                _Event = value;
            }
        }
        public string Audience {
            get {
                return _Audience;
            }
            set {
                _Audience = value;
            }
        }
        public string CreatedBy {
            get {
                return _CreatedBy;
            }
            set {
                _CreatedBy = value;
            }
        }
        public DateTime CreatedOn {
            get {
                return _CreatedOn;
            }
        }
        public string ModifiedBy {
            get {
                return _ModifiedBy;
            }
            set {
                _ModifiedBy = value;
            }
        }
        public DateTime ModifiedOn {
            get {
                return _ModifiedOn;
            }
        }
        public string ErrorMessage {
            get {
                return _ErrorMessage;
            }
        }

        #endregion

        #region _Constructors_

        public Action() {
            _ID = 0;
            _DisplayName = string.Empty;
            _Event = string.Empty;
            _Audience = string.Empty;
            _CreatedBy = "System Account";
            _CreatedOn = new DateTime(1900, 1, 1);
            _ModifiedBy = "System Account";
            _ModifiedOn = new DateTime(1900, 1, 1);
            _ErrorMessage = string.Empty;
        }
        public Action(int ID)
				: this() {
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                string sql = "SELECT * FROM dbo.Activity WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", ID);
                SqlDataReader sdr;
                try {
                    conn.Open();
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read()) {
                        _ID = int.Parse(sdr["ID"].ToString());
                        _DisplayName = sdr["DisplayName"].ToString();
                        _Event = sdr["Action"].ToString();
                        _Audience = sdr["Audience"].ToString();
                        _CreatedBy = sdr["CreatedBy"].ToString();
                        _CreatedOn = DateTime.Parse(sdr["CreatedOn"].ToString());
                        _ModifiedBy = sdr["ModifiedBy"].ToString();
                        _ModifiedOn = DateTime.Parse(sdr["ModifiedOn"].ToString());
                        _ErrorMessage = string.Empty;
                    }
                }
                catch (SqlException sqlex) {
                   Error.WriteError(sqlex);
                    _ErrorMessage = sqlex.Message;
                }
                catch (Exception ex) {
                   Error.WriteError(ex);
                    _ErrorMessage = ex.Message;
                }
                finally {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                }
            }
        }

        #endregion

        #region _Private Methods_

        #endregion

        #region _Public Methods_

        public bool Insert() {
            bool Successful = false;
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    DateTime TimeStamp = DateTime.UtcNow;
                    string sql = @"INSERT INTO dbo.Activity 
			                                (DisplayName,
										Action,
										Audience,
										CreatedBy,
										CreatedOn,
										ModifiedBy,
										ModifiedOn)
			                            VALUES
			                                (@DisplayName,
										@Action,
										@Audience,
										@CreatedBy,
										@CreatedOn,
										@ModifiedBy,
										@ModifiedOn)
			                            SELECT @ID = SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@DisplayName", _DisplayName);
                    cmd.Parameters.AddWithValue("@Action", _Event);
                    cmd.Parameters.AddWithValue("@Audience", _Audience);
                    cmd.Parameters.AddWithValue("@CreatedBy", _CreatedBy);
                    cmd.Parameters.AddWithValue("@CreatedOn", TimeStamp);
                    cmd.Parameters.AddWithValue("@ModifiedBy", _ModifiedBy);
                    cmd.Parameters.AddWithValue("@ModifiedOn", TimeStamp);
                    SqlParameter prmID = cmd.Parameters.Add("@ID", SqlDbType.Int);
                    prmID.Direction = ParameterDirection.Output;
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    cmd.ExecuteNonQuery();
                    _ID = int.Parse(prmID.Value.ToString());
                    _CreatedOn = TimeStamp;
                    _ModifiedOn = TimeStamp;
                    Successful = true;
                }
                catch (SqlException sqlex) {
                   Error.WriteError(sqlex);
                    _ErrorMessage = sqlex.Message;
                }
                catch (Exception ex) {
                   Error.WriteError(ex);
                    _ErrorMessage = ex.Message;
                }
                finally {
                    if (conn.State != ConnectionState.Closed) conn.Close();
                }
            }
            return Successful;
        }
        public bool Update() {
            bool Successful = false;
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    DateTime TimeStamp = DateTime.UtcNow;
                    string sql = @"UPDATE dbo.Activity 
			                        SET DisplayName = @DisplayName,
										Action = @Action,
										Audience = @Audience,
										ModifiedBy = @ModifiedBy,
										ModifiedOn = @ModifiedOn
			                        WHERE ID = @ID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID", _ID);
                    cmd.Parameters.AddWithValue("@DisplayName", _DisplayName);
                    cmd.Parameters.AddWithValue("@Action", _Event);
                    cmd.Parameters.AddWithValue("@Audience", _Audience);
                    cmd.Parameters.AddWithValue("@ModifiedBy", _ModifiedBy);
                    cmd.Parameters.AddWithValue("@ModifiedOn", TimeStamp);
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    cmd.ExecuteNonQuery();
                    _ModifiedOn = TimeStamp;
                    Successful = true;
                }
                catch (SqlException sqlex) {
                   Error.WriteError(sqlex);
                    _ErrorMessage = sqlex.Message;
                }
                catch (Exception ex) {
                   Error.WriteError(ex);
                    _ErrorMessage = ex.Message;
                }
                finally {
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
                    string sql = "DELETE FROM dbo.Activity WHERE ID = @ID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID", _ID);
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    cmd.ExecuteNonQuery();
                    Successful = true;
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
            return Successful;
        }
        public static DataSet Items() {
            return Items(string.Empty);
        }
        public static DataSet Items(string Audience) {
            DataSet ds = new DataSet();
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    string sql = "SELECT * FROM dbo.Activity WHERE Audience = 'Everyone' ORDER BY CreatedOn DESC";

                    if (Audience != string.Empty)
                        sql = "SELECT * FROM dbo.Activity ORDER BY CreatedOn DESC";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Audience", Audience);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
                catch (SqlException sqlex) {
                    Error.WriteError(sqlex);
                }
                catch (Exception ex) {
                    Error.WriteError(ex);
                }
                finally {
                    if (conn.State != ConnectionState.Closed) conn.Close();
                }
            }
            return ds;
        }


        public static bool Write(string Transaction, string Identity) {
            return Write(Transaction, Identity, "Everyone");
        }
        public static bool Write(string Transaction, string Identity, string Audience) {
            bool Successful = false;

            try {
                Action item = new Action();
                item.DisplayName = Identity;
                item.Event = Transaction;
                item.Audience = Audience;
                item.CreatedBy = Identity;
                item.ModifiedBy = Identity;
                Successful = item.Insert();
            }
            catch { }
            return Successful;
        }
        #endregion
    }
}