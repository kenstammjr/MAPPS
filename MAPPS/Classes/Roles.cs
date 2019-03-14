using System;
using System.Data;
using System.Data.SqlClient;

namespace MAPPS {
    public enum RoleType {
        Administrator,
        Manager
    }

    public class Role {

        #region _Private Variables_

        private int _ID;
        private string _Name;
        private string _Description;
        private int _DisplayIndex;
        private int _ParentRoleID;
        private string _ErrorMessage;

        #endregion

        #region _Public Properties_

        public enum RoleType {
            Administrator = 1,
            Manager = 2
        }

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
        public int DisplayIndex {
            get { return _DisplayIndex; }
            set { _DisplayIndex = value; }
        }
        public int ParentRoleID {
            get { return _ParentRoleID; }
            set { _ParentRoleID = value; }
        }
        public string ErrorMessage {
            get { return _ErrorMessage; }
        }

        #endregion

        #region _Constructors_

        public Role() {
            _ID = 0;
            _Name = string.Empty;
            _Description = string.Empty;
            _DisplayIndex = 0;
            _ParentRoleID = 0;
            _ErrorMessage = string.Empty;
        }
        public Role(int ID)
            : this() {
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                string sql = "SELECT * FROM dbo.Roles WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", ID);
                SqlDataReader sdr;
                try {
                    conn.Open();
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read()) {
                        _ID = int.Parse(sdr["ID"].ToString());
                        _Name = sdr["Role"].ToString();
                        _Description = sdr["Description"].ToString();
                        _DisplayIndex = int.Parse(sdr["DisplayIndex"].ToString());
                        _ParentRoleID = int.Parse(sdr["ParentRoleID"].ToString());
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
        public Role(string name)
            : this() {
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                const string sql = @"SELECT 
                                        ID
                                       ,Role
                                       ,Description
                                       ,DisplayIndex
                                       ,ParentRoleID
                                  FROM dbo.Roles 
                                  WHERE Role = @Role";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Role", name.Trim());
                SqlDataReader sdr;
                try {
                    conn.Open();
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read()) {
                        _ID = int.Parse(sdr["ID"].ToString());
                        _Name = sdr["Role"].ToString();
                        _Description = sdr["Description"].ToString();
                        _DisplayIndex = int.Parse(sdr["DisplayIndex"].ToString());
                        _ParentRoleID = int.Parse(sdr["ParentRoleID"].ToString());
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

        public static DataSet Items() {
            DataSet ds = new DataSet();
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    string sql = "SELECT * FROM dbo.Roles ";
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
        public static DataTable Items(bool AllItems) {
            DataTable dt = new DataTable();
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    if (!AllItems) {
                        string sql = "SELECT * FROM dbo.Roles WHERE ID !=0";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    } else {
                        string sql = "SELECT * FROM dbo.Roles";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                } catch (SqlException sqlex) {
                    Error.WriteError(sqlex);
                } catch (Exception ex) {
                    Error.WriteError(ex);
                } finally {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
            }
            return dt;
        }



        #endregion

    }
}
