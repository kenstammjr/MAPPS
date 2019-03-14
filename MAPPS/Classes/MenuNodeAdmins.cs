using System;
using System.Data;
using System.Data.SqlClient;

namespace MAPPS
{
    public class MenuNodeAdmin
    {

        #region _Private Variables_

        private int _ID;
        private int _MenuNodeID;
        private string _UserName;
        private string _DisplayName;
        private bool _IsDisabled;
        private string _CreatedBy;
        private DateTime _CreatedOn;
        private string _ModifiedBy;
        private DateTime _ModifiedOn;
        private string _ErrorMessage;

        #endregion

        #region _Public Properties_

        public int ID
        {
            get
            {
                return _ID;
            }
        }
        public int MenuNodeID
        {
            get
            {
                return _MenuNodeID;
            }
            set
            {
                _MenuNodeID = value;
            }
        }
        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
            }
        }
        public string DisplayName
        {
            get
            {
                return _DisplayName;
            }
            set
            {
                _DisplayName = value;
            }
        }
        public bool IsDisabled
        {
            get
            {
                return _IsDisabled;
            }
            set
            {
                _IsDisabled = value;
            }
        }
        public string CreatedBy
        {
            get
            {
                return _CreatedBy;
            }
            set
            {
                _CreatedBy = value;
            }
        }
        public DateTime CreatedOn
        {
            get
            {
                return _CreatedOn;
            }
        }
        public string ModifiedBy
        {
            get
            {
                return _ModifiedBy;
            }
            set
            {
                _ModifiedBy = value;
            }
        }
        public DateTime ModifiedOn
        {
            get
            {
                return _ModifiedOn;
            }
        }
        public string ErrorMessage
        {
            get
            {
                return _ErrorMessage;
            }
        }

        #endregion

        #region _Constructors_

        public MenuNodeAdmin()
        {
            _ID = 0;
            _MenuNodeID = 0;
            _UserName = string.Empty;
            _DisplayName = string.Empty;
            _IsDisabled = false;
            _CreatedBy = "System Account";
            _CreatedOn = new DateTime(1900, 1, 1);
            _ModifiedBy = "System Account";
            _ModifiedOn = new DateTime(1900, 1, 1);
            _ErrorMessage = string.Empty;
        }
        public MenuNodeAdmin(int ID)
            : this()
        {
            using (new Impersonator())
            {
                SqlConnection conn = DataSource.Conn();
                string sql = "SELECT * FROM nav.MenuNodeAdmins WHERE ID = @ID";
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
                        _ID = int.Parse(sdr["ID"].ToString());
                        _MenuNodeID = int.Parse(sdr["MenuNodeID"].ToString());
                        _UserName = sdr["UserName"].ToString();
                        _DisplayName = sdr["DisplayName"].ToString();
                        _IsDisabled = bool.Parse(sdr["IsDisabled"].ToString());
                        _CreatedBy = sdr["CreatedBy"].ToString();
                        _CreatedOn = DateTime.Parse(sdr["CreatedOn"].ToString());
                        _ModifiedBy = sdr["ModifiedBy"].ToString();
                        _ModifiedOn = DateTime.Parse(sdr["ModifiedOn"].ToString());
                        _ErrorMessage = string.Empty;
                    }
                }
                catch (SqlException sqlex)
                {
                    Error.WriteError(sqlex);
                    _ErrorMessage = sqlex.Message;
                }
                catch (Exception ex)
                {
                    Error.WriteError(ex);
                    _ErrorMessage = ex.Message;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
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
                    DateTime TimeStamp = DateTime.UtcNow;
                    string sql = @"INSERT INTO nav.MenuNodeAdmins 
			                                (MenuNodeID,
										UserName,
										DisplayName,
										IsDisabled,
										CreatedBy,
										CreatedOn,
										ModifiedBy,
										ModifiedOn)
			                            VALUES
			                                (@MenuNodeID,
										@UserName,
										@DisplayName,
										@IsDisabled,
										@CreatedBy,
										@CreatedOn,
										@ModifiedBy,
										@ModifiedOn)
			                            SELECT @ID = SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MenuNodeID", _MenuNodeID);
                    cmd.Parameters.AddWithValue("@UserName", _UserName);
                    cmd.Parameters.AddWithValue("@DisplayName", _DisplayName);
                    cmd.Parameters.AddWithValue("@IsDisabled", _IsDisabled);
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
                catch (SqlException sqlex)
                {
                    Error.WriteError(sqlex);
                    _ErrorMessage = sqlex.Message;
                }
                catch (Exception ex)
                {
                    Error.WriteError(ex);
                    _ErrorMessage = ex.Message;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed) conn.Close();
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
                    DateTime TimeStamp = DateTime.UtcNow;
                    string sql = @"UPDATE nav.MenuNodeAdmins 
			                        SET MenuNodeID = @MenuNodeID,
										UserName = @UserName,
										DisplayName = @DisplayName,
										IsDisabled = @IsDisabled,
										ModifiedBy = @ModifiedBy,
										ModifiedOn = @ModifiedOn
			                        WHERE ID = @ID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID", _ID);
                    cmd.Parameters.AddWithValue("@MenuNodeID", _MenuNodeID);
                    cmd.Parameters.AddWithValue("@UserName", _UserName);
                    cmd.Parameters.AddWithValue("@DisplayName", _DisplayName);
                    cmd.Parameters.AddWithValue("@IsDisabled", _IsDisabled);
                    cmd.Parameters.AddWithValue("@ModifiedBy", _ModifiedBy);
                    cmd.Parameters.AddWithValue("@ModifiedOn", TimeStamp);
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    cmd.ExecuteNonQuery();
                    _ModifiedOn = TimeStamp;
                    Successful = true;
                }
                catch (SqlException sqlex)
                {
                    Error.WriteError(sqlex);
                    _ErrorMessage = sqlex.Message;
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
                    string sql = "DELETE FROM nav.MenuNodeAdmins WHERE ID = @ID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID", _ID);
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    cmd.ExecuteNonQuery();
                    Successful = true;
                }
                catch (SqlException sqlex)
                {
                    Error.WriteError(sqlex);
                    _ErrorMessage = sqlex.Message;
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
                    string sql = "SELECT * FROM nav.MenuNodeAdmins";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
                catch (SqlException sqlex)
                {
                    Error.WriteError(sqlex);
                }
                catch (Exception ex)
                {
                    Error.WriteError(ex);
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed) conn.Close();
                }
            }
            return ds;
        }

        #endregion
    }
}
