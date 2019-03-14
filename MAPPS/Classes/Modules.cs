using System;
using System.Data;
using System.Data.SqlClient;

namespace MAPPS {
    public class Module {

        #region _Private Variables_

        private int _ID;
        private string _Name;
        private string _Description;
        private string _Directory;
        private string _URL;
        private string _AdminURL;
        private string _ImageURL;
        private string _DBVersion;
        private int _DisplayIndex;
        private bool _IsActive;
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
        public string Directory {
            get { return _Directory; }
            set { _Directory = value; }
        }
        public string URL {
            get { return _URL; }
            set { _URL = value; }
        }
        public string AdminURL {
            get { return _AdminURL; }
            set { _AdminURL = value; }
        }
        public string ImageURL {
            get { return _ImageURL; }
            set { _ImageURL = value; }
        }
        public string DBVersion {
            get { return _DBVersion; }
            set { _DBVersion = value; }
        }
        public int DisplayIndex {
            get { return _DisplayIndex; }
            set { _DisplayIndex = value; }
        }
        public bool IsActive {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        public string CreatedBy {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        public DateTime CreatedOn {
            get { return _CreatedOn; }
            set { _CreatedOn = value; }
        }
        public string ModifiedBy {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        public DateTime ModifiedOn {
            get { return _ModifiedOn; }
            set { _ModifiedOn = value; }
        }
        public string ErrorMessage {
            get { return _ErrorMessage; }
        }

        #endregion

        #region _Constructors_

        public Module() {
            _ID = 0;
            _Name = string.Empty;
            _Description = string.Empty;
            _Directory = string.Empty;
            _URL = string.Empty;
            _AdminURL = string.Empty;
            _ImageURL = string.Empty;
            _DBVersion = string.Empty;
            _DisplayIndex = 0;
            _IsActive = false;
            _CreatedBy = "System Account";
            _CreatedOn = new DateTime(1900, 1, 1);
            _ModifiedBy = "System Account";
            _ModifiedOn = new DateTime(1900, 1, 1);
            _ErrorMessage = string.Empty;
        }
        public Module(int ID)
            : this() {
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                string sql = "SELECT * FROM dbo.Modules WHERE ID = @ID";
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
                        _Directory = sdr["Directory"].ToString();
                        _URL = sdr["URL"].ToString();
                        _AdminURL = sdr["AdminURL"].ToString();
                        _ImageURL = sdr["ImageURL"].ToString();
                        _DBVersion = sdr["DBVersion"].ToString();
                        _DisplayIndex = int.Parse(sdr["DisplayIndex"].ToString());
                        _IsActive = bool.Parse(sdr["IsActive"].ToString());
                        _CreatedBy = sdr["CreatedBy"].ToString();
                        _CreatedOn = DateTime.Parse(sdr["CreatedOn"].ToString());
                        _ModifiedBy = sdr["ModifiedBy"].ToString();
                        _ModifiedOn = DateTime.Parse(sdr["ModifiedOn"].ToString());
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
        public Module(string module)
            : this() {
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                string sql = "SELECT * FROM dbo.Modules WHERE module = @MODULE";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@MODULE", module);
                SqlDataReader sdr;
                try {
                    conn.Open();
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read()) {
                        _ID = int.Parse(sdr["ID"].ToString());
                        _Name = sdr["Name"].ToString();
                        _Description = sdr["Description"].ToString();
                        _Directory = sdr["Directory"].ToString();
                        _URL = sdr["URL"].ToString();
                        _AdminURL = sdr["AdminURL"].ToString();
                        _ImageURL = sdr["ImageURL"].ToString();
                        _DBVersion = sdr["DBVersion"].ToString();
                        _DisplayIndex = int.Parse(sdr["DisplayIndex"].ToString());
                        _IsActive = bool.Parse(sdr["IsActive"].ToString());
                        _CreatedBy = sdr["CreatedBy"].ToString();
                        _CreatedOn = DateTime.Parse(sdr["CreatedOn"].ToString());
                        _ModifiedBy = sdr["ModifiedBy"].ToString();
                        _ModifiedOn = DateTime.Parse(sdr["ModifiedOn"].ToString());
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

        public bool Insert() {
            bool Successful = false;
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    DateTime timeStamp = DateTime.UtcNow;
                    string sql = @"Insert INTO dbo.Modules 
										(Name
										,Description
										,Directory
										,URL
										,AdminURL
										,ImageURL
										,DBVersion
										,DisplayIndex
										,IsActive
										,CreatedBy
										,CreatedOn
										,ModifiedBy
										,ModifiedOn)
							Values
										(@Name
										,@Description
										,@Directory
										,@URL
										,@AdminURL
										,@ImageURL
										,@DBVersion
										,@DisplayIndex
										,@IsActive
										,@CreatedBy
										,@CreatedOn
										,@ModifiedBy
										,@ModifiedOn)
							SELECT @ID = SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Name", _Name);
                    cmd.Parameters.AddWithValue("@Description", _Description);
                    cmd.Parameters.AddWithValue("@Directory", _Directory);
                    cmd.Parameters.AddWithValue("@URL", _URL);
                    cmd.Parameters.AddWithValue("@AdminURL", _AdminURL);
                    cmd.Parameters.AddWithValue("@ImageURL", _ImageURL);
                    cmd.Parameters.AddWithValue("@DBVersion", _DBVersion);
                    cmd.Parameters.AddWithValue("@DisplayIndex", _DisplayIndex);
                    cmd.Parameters.AddWithValue("@IsActive", _IsActive);
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
                    DateTime timeStamp = DateTime.UtcNow;
                    string sql = @"Update dbo.Modules 
										SET Name = @Name
										,Description = @Description
										,Directory = @Directory
										,URL = @URL
										,AdminURL = @AdminURL
										,ImageURL = @ImageURL
										,DBVersion = @DBVersion
										,DisplayIndex = @DisplayIndex
										,IsActive = @IsActive
										,ModifiedBy = @ModifiedBy
										,ModifiedOn = @ModifiedOn
							WHERE ID = @ID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Name", _Name);
                    cmd.Parameters.AddWithValue("@Description", _Description);
                    cmd.Parameters.AddWithValue("@Directory", _Directory);
                    cmd.Parameters.AddWithValue("@URL", _URL);
                    cmd.Parameters.AddWithValue("@AdminURL", _AdminURL);
                    cmd.Parameters.AddWithValue("@ImageURL", _ImageURL);
                    cmd.Parameters.AddWithValue("@DBVersion", _DBVersion);
                    cmd.Parameters.AddWithValue("@DisplayIndex", _DisplayIndex);
                    cmd.Parameters.AddWithValue("@IsActive", _IsActive);
                    cmd.Parameters.AddWithValue("@ModifiedBy", _ModifiedBy);
                    cmd.Parameters.AddWithValue("@ModifiedOn", _ModifiedOn);
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
        public bool Delete() {
            bool Successful = false;
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    string sql = "DELETE FROM dbo.Modules WHERE ID = @ID";
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

        public static DataTable Items() {
            DataTable dt = new DataTable();
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    string sql = "SELECT * FROM dbo.Modules ";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
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
