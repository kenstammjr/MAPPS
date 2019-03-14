using System;
using System.Data;
using System.Data.SqlClient;

namespace MAPPS {
    public enum SecurityGroupType {
        Administrators,
        Managers,
        Members
    }
    public class SecurityGroup {


        #region _Private Variables_

        private int _ID;
        private string _Name;
        private string _Description;
        private int _RoleID;
        private int _DisplayIndex;
        private int _ParentID;
        private string _ErrorMessage;

        #endregion

        #region _Public Properties_

        public int ID {
            get { return _ID; }
        }
        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }
        public string Description {
            get { return _Description; }
            set { _Description = value; }
        }
        public int RoleID {
            get { return _RoleID; }
            set { _RoleID = value; }
        }
        public int DisplayIndex {
            get { return _DisplayIndex; }
            set { _DisplayIndex = value; }
        }
        public int ParentID {
            get { return _ParentID; }
            set { _ParentID = value; }
        }
        public string ErrorMessage {
            get { return _ErrorMessage; }
        }

        #endregion

        #region _Constructors_

        public SecurityGroup() {
            _ID = 0;
            _Name = string.Empty;
            _Description = string.Empty;
            _RoleID = 0;
            _DisplayIndex = 0;
            _ParentID = 0;
            _ErrorMessage = string.Empty;
        }
        public SecurityGroup(int ID)
            : this() {
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                string sql = "SELECT * FROM dbo.SecurityGroups WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", ID);
                SqlDataReader sdr;
                try {
                    conn.Open();
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read()) {
                        _ID = int.Parse(sdr["ID"].ToString());
                        _Name = sdr["Name"].ToString();
                        _Description = sdr["Description"].ToString();
                        _RoleID = int.Parse(sdr["RoleID"].ToString());
                        _DisplayIndex = int.Parse(sdr["DisplayIndex"].ToString());
                        _ParentID = int.Parse(sdr["ParentID"].ToString());
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
        public SecurityGroup(string Name)
            : this() {
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                string sql = "SELECT * FROM dbo.SecurityGroups WHERE Name = @Name";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", Name);
                SqlDataReader sdr;
                try {
                    conn.Open();
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read()) {
                        _ID = int.Parse(sdr["ID"].ToString());
                        _Name = sdr["Name"].ToString();
                        _Description = sdr["Description"].ToString();
                        _RoleID = int.Parse(sdr["RoleID"].ToString());
                        _DisplayIndex = int.Parse(sdr["DisplayIndex"].ToString());
                        _ParentID = int.Parse(sdr["ParentID"].ToString());
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

        public bool Insert() {
            bool Successful = false;
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    DateTime timeStamp = DateTime.UtcNow;
                    string sql = @"Insert INTO dbo.SecurityGroups 
										(Name
										,Description
										,RoleID
										,DisplayIndex
										,ParentID)
							Values
										(@Name
										,@Description
										,@RoleID
										,@DisplayIndex
										,@ParentID)
							SELECT @ID = SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Name", _Name);
                    cmd.Parameters.AddWithValue("@Description", _Description);
                    cmd.Parameters.AddWithValue("@RoleID", _RoleID);
                    cmd.Parameters.AddWithValue("@DisplayIndex", _DisplayIndex);
                    cmd.Parameters.AddWithValue("@ParentID", _ParentID);
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
            return Successful;
        }
        public bool Update() {
            bool Successful = false;
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    DateTime timeStamp = DateTime.UtcNow;
                    string sql = @"Update dbo.SecurityGroups 
										SET Name = @Name
										,Description = @Description
										,RoleID = @RoleID
										,DisplayIndex = @DisplayIndex
										,ParentID = @ParentID
							WHERE ID = @ID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Name", _Name);
                    cmd.Parameters.AddWithValue("@Description", _Description);
                    cmd.Parameters.AddWithValue("@RoleID", _RoleID);
                    cmd.Parameters.AddWithValue("@DisplayIndex", _DisplayIndex);
                    cmd.Parameters.AddWithValue("@ParentID", _ParentID);
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
                    string sql = "DELETE FROM dbo.SecurityGroups WHERE ID = @ID";
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
        public static DataSet Items() {
            DataSet ds = new DataSet();
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    string sql = "SELECT * FROM dbo.SecurityGroups ORDER BY DisplayIndex ";
                    SqlCommand cmd = new SqlCommand(sql, conn);
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
        public static int GetSecurityGroupIdByName(SecurityGroupType securityGroup) {
            int id = 0;

            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                const string sql = @"SELECT ID 
                                    FROM dbo.SecurityGroups
                                    WHERE Name = @Name";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", securityGroup.ToString());
                try {
                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();

                    if (sdr.Read())
                        int.TryParse(sdr["ID"].ToString(), out id);
                } catch (Exception ex) {
                    Error.WriteError(ex);
                } finally {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
                return id;
            }
        }

        #endregion

    }
}
