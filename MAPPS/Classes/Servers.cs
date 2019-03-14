using System;
using System.Data;
using System.Data.SqlClient;

namespace MAPPS {
    public class Server {

        #region _Private Variables_

        private int _ID;
        private string _Name;
        private string _Description;
        private int _ServerFunctionID;
        private int _ServerTypeID;
        private int _ServerStatusID;
        private int _ServerEnvironmentID;
        private int _ServerVersionID;
        private string _IPAddress;
        private string _PrimaryPOC;
        private string _AlternatePOC;
        private string _RestartOrder;
        private string _Memory;
        private string _CPU;
        private string _Purpose;
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
        public int ServerFunctionID {
            get {
                return _ServerFunctionID;
            }
            set {
                _ServerFunctionID = value;
            }
        }
        public int ServerTypeID {
            get {
                return _ServerTypeID;
            }
            set {
                _ServerTypeID = value;
            }
        }
        public int ServerStatusID {
            get {
                return _ServerStatusID;
            }
            set {
                _ServerStatusID = value;
            }
        }
        public int ServerEnvironmentID {
            get {
                return _ServerEnvironmentID;
            }
            set {
                _ServerEnvironmentID = value;
            }
        }
        public int ServerVersionID {
            get {
                return _ServerVersionID;
            }
            set {
                _ServerVersionID = value;
            }
        }
        public string IPAddress {
            get {
                return _IPAddress;
            }
            set {
                _IPAddress = value;
            }
        }
        public string PrimaryPOC {
            get {
                return _PrimaryPOC;
            }
            set {
                _PrimaryPOC = value;
            }
        }
        public string AlternatePOC {
            get {
                return _AlternatePOC;
            }
            set {
                _AlternatePOC = value;
            }
        }
        public string RestartOrder {
            get {
                return _RestartOrder;
            }
            set {
                _RestartOrder = value;
            }
        }
        public string Memory {
            get {
                return _Memory;
            }
            set {
                _Memory = value;
            }
        }
        public string CPU {
            get {
                return _CPU;
            }
            set {
                _CPU = value;
            }
        }
        public string Purpose {
            get {
                return _Purpose;
            }
            set {
                _Purpose = value;
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

        public Server() {
            _ID = 0;
            _Name = string.Empty;
            _Description = string.Empty;
            _ServerFunctionID = 1;
            _ServerTypeID = 1;
            _ServerStatusID = 1;
            _ServerEnvironmentID = 1;
            _ServerVersionID = 1;
            _IPAddress = string.Empty;
            _PrimaryPOC = string.Empty;
            _AlternatePOC = string.Empty;
            _RestartOrder = string.Empty;
            _Memory = string.Empty;
            _CPU = string.Empty;
            _Purpose = string.Empty;
            _CreatedOn = new DateTime(1900, 1, 1);
            _CreatedBy = "System Account";
            _ModifiedOn = new DateTime(1900, 1, 1);
            _ModifiedBy = "System Account";
            _ErrorMessage = string.Empty;
        }
        public Server(int ID)
                : this() {
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                string sql = "SELECT * FROM dbo.Servers WHERE ID = @ID";
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
                        _ServerFunctionID = int.Parse(sdr["ServerFunctionID"].ToString());
                        _ServerTypeID = int.Parse(sdr["ServerTypeID"].ToString());
                        _ServerStatusID = int.Parse(sdr["ServerStatusID"].ToString());
                        _ServerEnvironmentID = int.Parse(sdr["ServerEnvironmentID"].ToString());
                        _ServerVersionID = int.Parse(sdr["ServerVersionID"].ToString());
                        _IPAddress = sdr["IPAddress"].ToString();
                        _PrimaryPOC = sdr["PrimaryPOC"].ToString();
                        _AlternatePOC = sdr["AlternatePOC"].ToString();
                        _RestartOrder = sdr["RestartOrder"].ToString();
                        _Memory = sdr["Memory"].ToString();
                        _CPU = sdr["CPU"].ToString();
                        _Purpose = sdr["Purpose"].ToString();
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
                    string sql = @"INSERT INTO dbo.Servers 
			                                (Name,
										Description,
										ServerFunctionID,
										ServerTypeID,
										ServerStatusID,
										ServerEnvironmentID,
										ServerVersionID,
										IPAddress,
										PrimaryPOC,
										AlternatePOC,
										RestartOrder,
										Memory,
										CPU,
										Purpose,
										CreatedOn,
										CreatedBy,
										ModifiedOn,
										ModifiedBy)
			                            VALUES
			                                (@Name,
										@Description,
										@ServerFunctionID,
										@ServerTypeID,
										@ServerStatusID,
										@ServerEnvironmentID,
										@ServerVersionID,
										@IPAddress,
										@PrimaryPOC,
										@AlternatePOC,
										@RestartOrder,
										@Memory,
										@CPU,
										@Purpose,
										@CreatedOn,
										@CreatedBy,
										@ModifiedOn,
										@ModifiedBy)
			                            SELECT @ID = SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Name", _Name);
                    cmd.Parameters.AddWithValue("@Description", _Description);
                    cmd.Parameters.AddWithValue("@ServerFunctionID", _ServerFunctionID);
                    cmd.Parameters.AddWithValue("@ServerTypeID", _ServerTypeID);
                    cmd.Parameters.AddWithValue("@ServerStatusID", _ServerStatusID);
                    cmd.Parameters.AddWithValue("@ServerEnvironmentID", _ServerEnvironmentID);
                    cmd.Parameters.AddWithValue("@ServerVersionID", _ServerVersionID);
                    cmd.Parameters.AddWithValue("@IPAddress", _IPAddress);
                    cmd.Parameters.AddWithValue("@PrimaryPOC", _PrimaryPOC);
                    cmd.Parameters.AddWithValue("@AlternatePOC", _AlternatePOC);
                    cmd.Parameters.AddWithValue("@RestartOrder", _RestartOrder);
                    cmd.Parameters.AddWithValue("@Memory", _Memory);
                    cmd.Parameters.AddWithValue("@CPU", _CPU);
                    cmd.Parameters.AddWithValue("@Purpose", _Purpose);
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
                    string sql = @"UPDATE dbo.Servers 
			                        SET Name = @Name,
										Description = @Description,
										ServerFunctionID = @ServerFunctionID,
										ServerTypeID = @ServerTypeID,
										ServerStatusID = @ServerStatusID,
										ServerEnvironmentID = @ServerEnvironmentID,
										ServerVersionID = @ServerVersionID,
										IPAddress = @IPAddress,
										PrimaryPOC = @PrimaryPOC,
										AlternatePOC = @AlternatePOC,
										RestartOrder = @RestartOrder,
										Memory = @Memory,
										CPU = @CPU,
										Purpose = @Purpose,
										ModifiedOn = @ModifiedOn,
										ModifiedBy = @ModifiedBy
			                        WHERE ID = @ID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID", _ID);
                    cmd.Parameters.AddWithValue("@Name", _Name);
                    cmd.Parameters.AddWithValue("@Description", _Description);
                    cmd.Parameters.AddWithValue("@ServerFunctionID", _ServerFunctionID);
                    cmd.Parameters.AddWithValue("@ServerTypeID", _ServerTypeID);
                    cmd.Parameters.AddWithValue("@ServerStatusID", _ServerStatusID);
                    cmd.Parameters.AddWithValue("@ServerEnvironmentID", _ServerEnvironmentID);
                    cmd.Parameters.AddWithValue("@ServerVersionID", _ServerVersionID);
                    cmd.Parameters.AddWithValue("@IPAddress", _IPAddress);
                    cmd.Parameters.AddWithValue("@PrimaryPOC", _PrimaryPOC);
                    cmd.Parameters.AddWithValue("@AlternatePOC", _AlternatePOC);
                    cmd.Parameters.AddWithValue("@RestartOrder", _RestartOrder);
                    cmd.Parameters.AddWithValue("@Memory", _Memory);
                    cmd.Parameters.AddWithValue("@CPU", _CPU);
                    cmd.Parameters.AddWithValue("@Purpose", _Purpose);
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
                    string sql = "DELETE FROM dbo.Servers WHERE ID = @ID";
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
                    string sql = @"SELECT Servers.ID, 
                                    Servers.Name, 
                                    Servers.Description, 
                                    Servers.ServerFunctionID, 
                                    Servers.ServerTypeID, 
                                    Servers.ServerStatusID, 
                                    Servers.ServerEnvironmentID, 
                                    Servers.ServerVersionID, 
                                    Servers.IPAddress, 
                                    Servers.PrimaryPOC,         
                                    Servers.AlternatePOC,       
                                    Servers.RestartOrder, 
                                    Servers.Memory, 
                                    Servers.CPU, 
                                    Servers.Purpose, 
                                    Servers.CreatedOn, 
                                    Servers.CreatedBy, 
                                    Servers.ModifiedOn, 
                                    Servers.ModifiedBy, 
                                    ServerFunctions.Name AS FunctionName, 
                                    ServerStatuses.Name AS StatusName, 
                                    ServerTypes.Name AS TypeName, 
                                    ServerVersions.Name AS VersionName
                                FROM Servers 
                                INNER JOIN
                                    ServerFunctions ON Servers.ServerFunctionID = ServerFunctions.ID INNER JOIN
                                    ServerStatuses ON Servers.ServerStatusID = ServerStatuses.ID INNER JOIN
                                    ServerTypes ON Servers.ServerTypeID = ServerTypes.ID INNER JOIN
                                    ServerVersions ON Servers.ServerVersionID = ServerVersions.ID
                                ORDER BY Servers.Name";

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
        public static DataSet Items(string Filter) {
            DataSet ds = new DataSet();
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    string filter = string.Empty;
                    string maxRecords = string.Format(" TOP ({0}) ", MAPPS.Configuration.AppSetting("MaxRecords"));
                    if (Filter.Length != 0)
                        filter = @" AND (Servers.Name LIKE @Filter 
                                    OR Servers.Description LIKE @Filter
                                    OR Servers.IPAddress LIKE @Filter
                                    OR Servers.PrimaryPOC LIKE @Filter
                                    OR Servers.AlternatePOC LIKE @Filter
                                    OR Servers.Purpose LIKE @Filter
                                    OR ServerFunctions.Name LIKE @Filter
                                    OR ServerStatuses.Name LIKE @Filter
                                    OR ServerTypes.Name LIKE @Filter
                                    OR ServerVersions.Name LIKE @Filter)";

                    string sql = string.Format(@"SELECT {0} 
                                        Servers.ID, 
                                        Servers.Name, 
                                        Servers.Description, 
                                        Servers.ServerFunctionID, 
                                        Servers.ServerTypeID, 
                                        Servers.ServerStatusID, 
                                        Servers.ServerEnvironmentID, 
                                        Servers.ServerVersionID, 
                                        Servers.IPAddress, 
                                        Servers.PrimaryPOC, 
                                        Servers.AlternatePOC, 
                                        Servers.RestartOrder, 
                                        Servers.Memory,
                                        Servers.CPU, 
                                        Servers.Purpose, 
                                        Servers.CreatedOn, 
                                        Servers.CreatedBy,
                                        Servers.ModifiedOn, 
                                        Servers.ModifiedBy,
                                        ServerFunctions.Name AS FunctionName,
                                        ServerStatuses.Name AS StatusName,
                                        ServerTypes.Name AS TypeName,
                                        ServerVersions.Name AS VersionName
                                    FROM Servers
                                    INNER JOIN
                                        ServerFunctions ON Servers.ServerFunctionID = ServerFunctions.ID INNER JOIN
                                        ServerStatuses ON Servers.ServerStatusID = ServerStatuses.ID INNER JOIN
                                        ServerTypes ON Servers.ServerTypeID = ServerTypes.ID INNER JOIN
                                        ServerVersions ON Servers.ServerVersionID = ServerVersions.ID 
                                    WHERE 1= 1
                                    {1}
                                    ORDER BY Servers.Name", maxRecords, filter);

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Filter", "%" + Filter + "%");
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

        public static bool Exists(string ServerName) {
            bool found = false;

            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                const string sql = @"SELECT Count(ID) AS cnt 
                                    FROM dbo.Servers 
                                    WHERE Name = @ServerName";

                SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ServerName", ServerName);
                try {
                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();

                    if (sdr.Read()) {
                        if (int.Parse(sdr["cnt"].ToString()) > 0) {
                            found = true;
                        }
                    }
                } catch (Exception ex) {
                    Error.WriteError(ex);
                } finally {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
            }
            return found;
        }
        #endregion
    }
}