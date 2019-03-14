using System;
using System.Data;
using System.Data.SqlClient;

namespace MAPPS {
    public class Menu {

        #region _Private Variables_

        private int _ID;
        private string _Name;
        private string _Description;
        private bool _IsVisible;
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
        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }
        public string Description {
            get { return _Description; }
            set { _Description = value; }
        }
        public bool IsVisible {
            get { return _IsVisible; }
            set { _IsVisible = value; }
        }
        public DateTime CreatedOn {
            get { return _CreatedOn; }
        }
        public string CreatedBy {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        public DateTime ModifiedOn {
            get { return _ModifiedOn; }
        }
        public string ModifiedBy {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        public string ErrorMessage {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; }
        }

        #endregion

        #region _Constructors_

        public Menu() {
            _ID = 0;
            _Name = string.Empty;
            _Description = string.Empty;
            _IsVisible = true;
            _CreatedOn = new DateTime(1900, 1, 1);
            _CreatedBy = "System Account";
            _ModifiedOn = new DateTime(1900, 1, 1);
            _ModifiedBy = "System Account";
            _ErrorMessage = string.Empty;
        }
        public Menu(int ID)
            : this() {
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                string sql = "SELECT * FROM dbo.Menus WHERE ID = @ID";
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
                        _IsVisible = bool.Parse(sdr["IsVisible"].ToString());
                        _CreatedBy = sdr["CreatedBy"].ToString();
                        _CreatedOn = DateTime.Parse(sdr["CreatedOn"].ToString());
                        _ModifiedBy = sdr["ModifiedBy"].ToString();
                        _ModifiedOn = DateTime.Parse(sdr["ModifiedOn"].ToString());
                        _ErrorMessage = string.Empty;
                    }
                } catch (SqlException sqlex) {
                    _ErrorMessage = sqlex.Message;
                } catch (Exception ex) {
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
                    DateTime TimeStamp = DateTime.UtcNow;
                    string sql = @"Insert INTO dbo.Menus 
			                            (Name,
										Description,
										IsVisible,
										CreatedBy,
										CreatedOn,
										ModifiedBy,
										ModifiedOn)
			                        Values
			                            (@Name,
										@Description,
										@IsVisible,
										@CreatedBy,
										@CreatedOn,
										@ModifiedBy,
										@ModifiedOn)
			                        SELECT @ID = SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Name", _Name);
                    cmd.Parameters.AddWithValue("@Description", _Description);
                    cmd.Parameters.AddWithValue("@IsVisible", _IsVisible);
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
                    try {
                        MenuNode node = new MenuNode();
                        node.MenuID = _ID;
                        node.Name = "Dummy Node";
                        node.Description = "Dummy node created when menu generated";
                        node.IsVisible = true;
                        node.URL = "#";
                        node.ParentID = 0;
                        node.Insert();
                    } catch (Exception ex) {
                        Error.WriteError("Menu", "Insert(), Create Dummy Node", ex);
                        _ErrorMessage = ex.Message;
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
            return Successful;
        }
        public bool Update() {
            bool Successful = false;
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    DateTime TimeStamp = DateTime.UtcNow;
                    string sql = @"Update dbo.Menus 
			                        SET Name = @Name,
										Description = @Description,
										IsVisible = @IsVisible,
										ModifiedBy = @ModifiedBy,
										ModifiedOn = @ModifiedOn
			                        WHERE ID = @ID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID", _ID);
                    cmd.Parameters.AddWithValue("@Name", _Name);
                    cmd.Parameters.AddWithValue("@Description", _Description);
                    cmd.Parameters.AddWithValue("@IsVisible", _IsVisible);
                    cmd.Parameters.AddWithValue("@ModifiedBy", _ModifiedBy);
                    cmd.Parameters.AddWithValue("@ModifiedOn", TimeStamp);
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    cmd.ExecuteNonQuery();
                    _ModifiedOn = TimeStamp;
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
        public bool Delete() {
            bool Successful = false;
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    string sql = "DELETE FROM dbo.Menus WHERE ID = @ID";
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
            DataSet ds = new DataSet();
            try {
                using (new Impersonator()) {
                    SqlConnection conn = DataSource.Conn();
                    try {
                        string sql = "SELECT * FROM dbo.Menus ORDER BY Name";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                    } catch (Exception ex) {
                        Error.WriteError(ex);
                    } finally {
                        if (conn.State != ConnectionState.Closed) { conn.Close(); }
                    }
                }
            } catch (Exception ex) {
                Error.WriteError(ex);
            }
            return ds;
        }

        #endregion

    }
}