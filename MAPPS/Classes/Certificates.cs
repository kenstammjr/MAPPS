using System;
using System.Data;
using System.Data.SqlClient;

namespace MAPPS {
    public class Certificate {

        #region _Private Variables_

        private int _ID;
        private string _Name;
        private string _Description;
        private DateTime _Expiration;
        private DateTime _CreatedOn;
        private string _CreatedBy;
        private DateTime _ModifiedOn;
        private string _ModifiedBy;
        private string _ErrorMessage;

        #endregion

        #region _Public Properties_

        public int ID {
            get {
                return _ID;
            }
        }
        public string Name {
            get {
                return _Name;
            }
            set {
                _Name = value;
            }
        }
        public string Description {
            get {
                return _Description;
            }
            set {
                _Description = value;
            }
        }
        public DateTime Expiration {
            get {
                return _Expiration;
            }
            set {
                _Expiration = value;
            }
        }
        public DateTime CreatedOn {
            get {
                return _CreatedOn;
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
        public DateTime ModifiedOn {
            get {
                return _ModifiedOn;
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
        public string ErrorMessage {
            get {
                return _ErrorMessage;
            }
        }

        #endregion

        #region _Constructors_

        public Certificate() {
            _ID = 0;
            _Name = string.Empty;
            _Description = string.Empty;
            _Expiration = new DateTime(1900, 1, 1);
            _CreatedOn = new DateTime(1900, 1, 1);
            _CreatedBy = "System Account";
            _ModifiedOn = new DateTime(1900, 1, 1);
            _ModifiedBy = "System Account";
            _ErrorMessage = string.Empty;
        }
        public Certificate(int ID)
                : this() {
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                string sql = "SELECT * FROM dbo.Certificates WHERE ID = @ID";
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
                        _Expiration = DateTime.Parse(sdr["Expiration"].ToString());
                        _CreatedOn = DateTime.Parse(sdr["CreatedOn"].ToString());
                        _CreatedBy = sdr["CreatedBy"].ToString();
                        _ModifiedOn = DateTime.Parse(sdr["ModifiedOn"].ToString());
                        _ModifiedBy = sdr["ModifiedBy"].ToString();
                        _ErrorMessage = string.Empty;
                    }
                } catch (SqlException sqlex) {
                    Error.WriteError(sqlex);
                    _ErrorMessage = sqlex.Message;
                } catch (Exception ex) {
                    Error.WriteError(ex);
                    _ErrorMessage = ex.Message;
                } finally {
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
                    string sql = @"INSERT INTO dbo.Certificates 
			                                (Name,
										Description,
										Expiration,
										CreatedOn,
										CreatedBy,
										ModifiedOn,
										ModifiedBy)
			                            VALUES
			                                (@Name,
										@Description,
										@Expiration,
										@CreatedOn,
										@CreatedBy,
										@ModifiedOn,
										@ModifiedBy)
			                            SELECT @ID = SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Name", _Name);
                    cmd.Parameters.AddWithValue("@Description", _Description);
                    cmd.Parameters.AddWithValue("@Expiration", _Expiration);
                    cmd.Parameters.AddWithValue("@CreatedOn", TimeStamp);
                    cmd.Parameters.AddWithValue("@CreatedBy", _CreatedBy);
                    cmd.Parameters.AddWithValue("@ModifiedOn", TimeStamp);
                    cmd.Parameters.AddWithValue("@ModifiedBy", _ModifiedBy);
                    SqlParameter prmID = cmd.Parameters.Add("@ID", SqlDbType.Int);
                    prmID.Direction = ParameterDirection.Output;
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    cmd.ExecuteNonQuery();
                    _ID = int.Parse(prmID.Value.ToString());
                    _CreatedOn = TimeStamp;
                    _ModifiedOn = TimeStamp;
                    Successful = true;
                } catch (SqlException sqlex) {
                    Error.WriteError(sqlex);
                    _ErrorMessage = sqlex.Message;
                } catch (Exception ex) {
                    Error.WriteError(ex);
                    _ErrorMessage = ex.Message;
                } finally {
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
                    string sql = @"UPDATE dbo.Certificates 
			                        SET Name = @Name,
										Description = @Description,
										Expiration = @Expiration,
										ModifiedOn = @ModifiedOn,
										ModifiedBy = @ModifiedBy
			                        WHERE ID = @ID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID", _ID);
                    cmd.Parameters.AddWithValue("@Name", _Name);
                    cmd.Parameters.AddWithValue("@Description", _Description);
                    cmd.Parameters.AddWithValue("@Expiration", _Expiration);
                    cmd.Parameters.AddWithValue("@ModifiedOn", TimeStamp);
                    cmd.Parameters.AddWithValue("@ModifiedBy", _ModifiedBy);
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
                    string sql = "DELETE FROM dbo.Certificates WHERE ID = @ID";
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
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    string sql = "SELECT * FROM dbo.Certificates ORDER BY Name";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                } catch (SqlException sqlex) {
                    Error.WriteError(sqlex);
                } catch (Exception ex) {
                    Error.WriteError(ex);
                } finally {
                    if (conn.State != ConnectionState.Closed) conn.Close();
                }
            }
            return ds;
        }
        public static DataSet Items(string Keyword) {
            DataSet ds = new DataSet();
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    string sql = "SELECT * FROM dbo.Certificates ORDER BY Name";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                } catch (SqlException sqlex) {
                    Error.WriteError(sqlex);
                } catch (Exception ex) {
                    Error.WriteError(ex);
                } finally {
                    if (conn.State != ConnectionState.Closed) conn.Close();
                }
            }
            return ds;
        }
        public static DataSet Items(int ServerID) {
            DataSet ds = new DataSet();
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    string sql = @"SELECT ServerCertificates.ID, ServerCertificates.ServerID, ServerCertificates.CertificateID, 
                                        Certificates.Name, Certificates.Description, Certificates.Expiration
                                    FROM Certificates 
                                    INNER JOIN ServerCertificates ON Certificates.ID = ServerCertificates.CertificateID
                                    WHERE ServerCertificates.ServerID = @ServerID
                                    ORDER BY Certificates.Name";

                   
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ServerID", ServerID);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                } catch (SqlException sqlex) {
                    Error.WriteError(sqlex);
                } catch (Exception ex) {
                    Error.WriteError(ex);
                } finally {
                    if (conn.State != ConnectionState.Closed) conn.Close();
                }
            }
            return ds;
        }


        #endregion
    }
}