using System;
using System.Data;
using System.Data.SqlClient;

namespace MAPPS {
    public class SecurityGroupMembership {

        #region _Private Variables_

        private int _ID;
        private int _SecurityGroupID;
        private int _UserID;
        private string _CreatedBy;
        private DateTime _CreatedOn;
        private string _ModifiedBy;
        private DateTime _ModifiedOn;
        private string _ErrorMessage;

        #endregion

        #region _Public Properties_

        public int ID {
            get { return _ID; }
        }
        public int SecurityGroupID {
            get { return _SecurityGroupID; }
            set { _SecurityGroupID = value; }
        }
        public int UserID {
            get { return _UserID; }
            set { _UserID = value; }
        }
        public string ErrorMessage {
            get { return _ErrorMessage; }
        }
        public string CreatedBy {
            get { return _CreatedBy; }
            set { _CreatedBy = value.Trim(); }
        }
        public DateTime CreatedOn {
            get { return _CreatedOn; }
            set { _CreatedOn = value; }
        }
        public string ModifiedBy {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value.Trim(); }
        }
        public DateTime ModifiedOn {
            get { return _ModifiedOn; }
            set { _ModifiedOn = value; }
        }
        #endregion

        #region _Constructors_

        public SecurityGroupMembership() {
            _ID = 0;
            _SecurityGroupID = 0;
            _UserID = 0;
            _CreatedBy = "System Account";
            _CreatedOn = new DateTime(1900, 1, 1);
            _ModifiedBy = "System Account";
            _ModifiedOn = new DateTime(1900, 1, 1);
            _ErrorMessage = string.Empty;
        }
        public SecurityGroupMembership(int ID)
            : this() {
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                string sql = "SELECT * FROM dbo.SecurityGroupMemberships WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", ID);
                SqlDataReader sdr;
                try {
                    conn.Open();
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read()) {
                        _ID = int.Parse(sdr["ID"].ToString());
                        _SecurityGroupID = int.Parse(sdr["SecurityGroupID"].ToString());
                        _UserID = int.Parse(sdr["UserID"].ToString());
                        _CreatedBy = sdr["CreatedBy"].ToString();
                        _CreatedOn = DateTime.Parse(sdr["CreatedOn"].ToString());
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
        public SecurityGroupMembership(int SecurityGroupID, int UserID)
            : this() {
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                string sql = @"SELECT * FROM dbo.SecurityGroupMemberships 
                            WHERE SecurityGroupID = @SecurityGroupID
                            AND UserID = @UserID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@SecurityGroupID", SecurityGroupID);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                SqlDataReader sdr;
                try {
                    conn.Open();
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read()) {
                        _ID = int.Parse(sdr["ID"].ToString());
                        _SecurityGroupID = int.Parse(sdr["SecurityGroupID"].ToString());
                        _UserID = int.Parse(sdr["UserID"].ToString());
                        _CreatedBy = sdr["CreatedBy"].ToString();
                        _CreatedOn = DateTime.Parse(sdr["CreatedOn"].ToString());
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

        private bool _IsGroupMember() {
            bool IsMember = false;
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    string sql = @"SELECT COUNT(ID) AS cnt
                                FROM dbo.SecurityGroupMemberships
                                WHERE SecurityGroupID = @SecurityGroupID
                                AND UserID = @UserID";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@SecurityGroupID", _SecurityGroupID);
                    cmd.Parameters.AddWithValue("@UserID", _UserID);

                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    int RecCnt = int.Parse(cmd.ExecuteScalar().ToString());
                    if (RecCnt > 0)
                        IsMember = true;
                } catch (Exception ex) {
                    Error.WriteError(ex);
                } finally {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
            }

            return IsMember;
        }


        #endregion

        #region _Public Methods_

        public bool Insert() {
            bool Successful = false;
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                if (_IsGroupMember())
                    Successful = true;
                else {
                    try {
                        DateTime timeStamp = DateTime.UtcNow;
                        string sql = @"Insert INTO dbo.SecurityGroupMemberships 
										(SecurityGroupID
                                        ,UserID
                                        ,CreatedBy
										,CreatedOn
										,ModifiedBy
										,ModifiedOn )
							Values
										(@SecurityGroupID
                                        ,@UserID
                                        ,@CreatedBy
										,@CreatedOn
										,@ModifiedBy
										,@ModifiedOn)
							SELECT @ID = SCOPE_IDENTITY()";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@SecurityGroupID", _SecurityGroupID);
                        cmd.Parameters.AddWithValue("@UserID", _UserID);
                        cmd.Parameters.AddWithValue("@CreatedBy", _CreatedBy);
                        cmd.Parameters.AddWithValue("@CreatedOn", _CreatedOn);
                        cmd.Parameters.AddWithValue("@ModifiedBy", _ModifiedBy);
                        cmd.Parameters.AddWithValue("@ModifiedOn", _ModifiedOn);
                        SqlParameter prmID = cmd.Parameters.Add("@ID", SqlDbType.Int);
                        prmID.Direction = ParameterDirection.Output;
                        if (conn.State != ConnectionState.Open) { conn.Open(); }
                        cmd.ExecuteNonQuery();
                        _ID = int.Parse(prmID.Value.ToString());
                        Successful = true;
                    } catch (Exception ex) {
                        Error.WriteError(ex);
                        _ErrorMessage = ex.Message;
                    } finally {
                        if (conn.State != ConnectionState.Closed) { conn.Close(); }
                    }
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
                    string sql = @"Update dbo.SecurityGroupMemberships 
										SET SecurityGroupID = @SecurityGroupID
										,UserID = @UserID
										,ModifiedBy = @ModifiedBy
										,ModifiedOn = @ModifiedOn
							WHERE ID = @ID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@SecurityGroupID", _SecurityGroupID);
                    cmd.Parameters.AddWithValue("@UserID", _UserID);
                    cmd.Parameters.AddWithValue("@ModifiedBy", _ModifiedBy);
                    cmd.Parameters.AddWithValue("@ModifiedOn", _ModifiedOn);
                    cmd.Parameters.AddWithValue("@ID", _ID);
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    cmd.ExecuteNonQuery();
                    Successful = true;
                } catch (Exception ex) {
                    Error.WriteError(ex);
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
                    string sql = "DELETE FROM dbo.SecurityGroupMemberships WHERE ID = @ID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID", _ID);
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    cmd.ExecuteNonQuery();
                    Successful = true;
                } catch (Exception ex) {
                    Error.WriteError(ex);
                    _ErrorMessage = ex.Message;
                } finally {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
            }
            return Successful;
        }
        public static DataSet Items(int SecurityGroupID) {
            DataSet ds = new DataSet();
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    string sql = @"SELECT SecurityGroupMemberships.ID, 
                                    SecurityGroupMemberships.UserID, 
                                    Users.UserName 
                                FROM dbo.SecurityGroupMemberships 
                                LEFT OUTER JOIN dbo.Users ON SecurityGroupMemberships.UserID = Users.ID
                                WHERE SecurityGroupMemberships.SecurityGroupID = @SecurityGroupID";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@SecurityGroupID", SecurityGroupID);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                } catch (Exception ex) {
                    Error.WriteError(ex);
                } finally {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
            }
            return ds;
        }
        public static DataSet UserItems(int UserID) {
            DataSet ds = new DataSet();
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    string sql = @"SELECT SecurityGroupMemberships.ID, 
                                    SecurityGroupMemberships.SecurityGroupID, 
                                    SecurityGroupMemberships.UserID, 
                                    Roles.Role,
                                    'App-' + Roles.Role as AppRole,
                                    SecurityGroups.Name
                                FROM dbo.Roles 
                                INNER JOIN dbo.SecurityGroups ON Roles.ID = SecurityGroups.RoleID 
                                RIGHT OUTER JOIN dbo.SecurityGroupMemberships 
                                LEFT OUTER JOIN dbo.Users ON SecurityGroupMemberships.UserID = Users.ID ON SecurityGroups.ID = SecurityGroupMemberships.SecurityGroupID
                                WHERE SecurityGroupMemberships.UserID = @UserID
                                ORDER BY Roles.Role DESC";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                } catch (Exception ex) {
                    Error.WriteError(ex);
                } finally {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
            }
            return ds;
        }

        public static int SecurityGroupMembershipActiveCount(string Role) {
            int cnt = new int();
            cnt = 0;
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    string sql = @"SELECT COUNT(Roles.ID) AS cnt
                                    FROM dbo.Roles 
                                    INNER JOIN dbo.SecurityGroups ON Roles.ID = SecurityGroups.RoleID 
                                    INNER JOIN dbo.SecurityGroupMemberships ON SecurityGroups.ID = SecurityGroupMemberships.SecurityGroupID 
                                    INNER JOIN dbo.Users ON SecurityGroupMemberships.UserID = Users.ID
                                    WHERE (Roles.Role = @Role)";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Role", Role); SqlDataAdapter da = new SqlDataAdapter(cmd);
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    cnt = Int32.Parse(cmd.ExecuteScalar().ToString());
                } catch (Exception ex) {
                    Error.WriteError(ex);
                } finally {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
            }
            return cnt;
        }


        #endregion

    }
}

