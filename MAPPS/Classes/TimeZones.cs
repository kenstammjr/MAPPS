using System;
using System.Data;
using System.Data.SqlClient;

namespace MAPPS {
    public class TimeZone {

        #region _Private Variables_

        private int _ID;
        private Guid _WebID;
        private string _ZoneID;
        private string _DisplayName;
        private int _DisplayIndex;
        private bool _ShowTimeZoneLetter;
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
        public Guid WebID {
            get {
                return _WebID;
            }
            set {
                _WebID = value;
            }
        }
        public string ZoneID {
            get {
                return _ZoneID;
            }
            set {
                _ZoneID = value;
            }
        }
        public string DisplayName {
            get {
                return _DisplayName.ToUpper();
            }
            set {
                _DisplayName = value.ToUpper();
            }
        }
        public int DisplayIndex {
            get {
                return _DisplayIndex;
            }
            set {
                _DisplayIndex = value;
            }
        }
        public bool ShowTimeZoneLetter {
            get {
                return _ShowTimeZoneLetter;
            }
            set {
                _ShowTimeZoneLetter = value;
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

        public TimeZone() {
            _ID = 0;
            _WebID = new Guid();
            _ZoneID = string.Empty;
            _DisplayName = string.Empty;
            _DisplayIndex = 0;
            _ShowTimeZoneLetter = false;
            _CreatedBy = "System Account";
            _CreatedOn = new DateTime(1900, 1, 1);
            _ModifiedBy = "System Account";
            _ModifiedOn = new DateTime(1900, 1, 1);
            _ErrorMessage = string.Empty;
        }
        public TimeZone(int ID)
            : this() {
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                string sql = "SELECT * FROM dbo.TimeZones WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", ID);
                SqlDataReader sdr;
                try {
                    conn.Open();
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read()) {
                        _ID = int.Parse(sdr["ID"].ToString());
                        _WebID = new Guid(sdr["WebID"].ToString());
                        _ZoneID = sdr["ZoneID"].ToString();
                        _DisplayName = sdr["DisplayName"].ToString();
                        _DisplayIndex = int.Parse(sdr["DisplayIndex"].ToString());
                        _ShowTimeZoneLetter = bool.Parse(sdr["ShowTimeZoneLetter"].ToString());
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
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                }
            }
        }

        #endregion

        #region _Private Methods_

        private static void AddColumns(DataTable DataTable) {
            DataTable.Columns.Add("TimeInZone", typeof(DateTime));
            DataTable.Columns.Add("TimeInZoneFormat", typeof(string));
            DataTable.Columns.Add("Class", typeof(string));
            DataTable.Columns.Add("ZoneOffset", typeof(string));
            int i = 0;
            foreach (DataRow dr in DataTable.Rows) {
                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(dr["ZoneID"].ToString());
                DateTime timeInZone = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi);
                dr["TimeInZone"] = timeInZone;
                dr["TimeInZoneFormat"] = timeInZone.ToString("HH:mm");
                
                TimeZoneOffset tzo = new TimeZoneOffset(tzi.BaseUtcOffset.ToString());
                dr["ZoneOffset"] = tzi.GetUtcOffset(timeInZone).TotalHours;
                
                if (bool.Parse(dr["ShowTimeZoneLetter"].ToString()))
                    dr["DisplayName"] += string.Format(" [{0}]", tzo.Letter);
                
                if (++i == DataTable.Rows.Count)
                    dr["Class"] = "ms-socialNotif-text timeZoneWrap timeZoneWrapEnd";
                else
                    dr["Class"] = "ms-socialNotif-text timeZoneWrap";
            }
        }

        #endregion

        #region _Public Methods_

        public bool Insert() {
            bool Successful = false;
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    DateTime TimeStamp = DateTime.UtcNow;
                    string sql = @"INSERT INTO dbo.TimeZones 
			                                (WebID,
										ZoneID,
										DisplayName,
										DisplayIndex,
										ShowTimeZoneLetter,
										CreatedBy,
										CreatedOn,
										ModifiedBy,
										ModifiedOn)
			                            VALUES
			                                (@WebID,
										@ZoneID,
										@DisplayName,
										@DisplayIndex,
										@ShowTimeZoneLetter,
										@CreatedBy,
										@CreatedOn,
										@ModifiedBy,
										@ModifiedOn)
			                            SELECT @ID = SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@WebID", _WebID);
                    cmd.Parameters.AddWithValue("@ZoneID", _ZoneID);
                    cmd.Parameters.AddWithValue("@DisplayName", _DisplayName);
                    cmd.Parameters.AddWithValue("@DisplayIndex", _DisplayIndex);
                    cmd.Parameters.AddWithValue("@ShowTimeZoneLetter", _ShowTimeZoneLetter);
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
                    string sql = @"UPDATE dbo.TimeZones 
			                        SET WebID = @WebID,
										ZoneID = @ZoneID,
										DisplayName = @DisplayName,
										DisplayIndex = @DisplayIndex,
										ShowTimeZoneLetter = @ShowTimeZoneLetter,
										ModifiedBy = @ModifiedBy,
										ModifiedOn = @ModifiedOn
			                        WHERE ID = @ID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID", _ID);
                    cmd.Parameters.AddWithValue("@WebID", _WebID);
                    cmd.Parameters.AddWithValue("@ZoneID", _ZoneID);
                    cmd.Parameters.AddWithValue("@DisplayName", _DisplayName);
                    cmd.Parameters.AddWithValue("@DisplayIndex", _DisplayIndex);
                    cmd.Parameters.AddWithValue("@ShowTimeZoneLetter", _ShowTimeZoneLetter);
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
                    string sql = "DELETE FROM dbo.TimeZones WHERE ID = @ID";
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
        public static bool ResetItems(Guid WebID) {
            bool Successful = false;
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    string sql = "DELETE FROM dbo.TimeZones WHERE WebID = @WebID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@WebID", WebID);
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    cmd.ExecuteNonQuery();
                    Successful = true;
                } catch (SqlException sqlex) {
                    Error.WriteError(sqlex);
                } catch (Exception ex) {
                    Error.WriteError(ex);
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
                    string sql = "SELECT * FROM dbo.TimeZones ORDER BY DisplayIndex";
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
        public static DataSet Items(Guid WebID) {
            DataSet ds = new DataSet();
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    string sql = "SELECT * FROM dbo.TimeZones WHERE WebID = @WebID ORDER BY DisplayIndex";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@WebID", WebID);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    AddColumns(ds.Tables[0]);
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

        public static DataTable DefaultTimeZones() {
            DataTable dt = new DataTable();
            dt.TableName = "TimeZones";

            dt.Columns.Add("ZoneID", typeof(string));
            dt.Columns.Add("DisplayName", typeof(string));
            dt.Columns.Add("DisplayIndex", typeof(int));
            dt.Columns.Add("ShowTimeZoneLetter", typeof(bool));

            dt.Rows.Add(new Object[] { "Eastern Standard Time", "SOCOM", 1, true });
            dt.Rows.Add(new Object[] { "UTC", "UTC", 2, true });
            dt.Rows.Add(new Object[] { "Hawaiian Standard Time", "PACOM", 3, true });
            dt.Rows.Add(new Object[] { "Pacific Standard Time", "NSW", 4, true });
            dt.Rows.Add(new Object[] { "W. Europe Standard Time", "EUCOM", 5, true });
            dt.Rows.Add(new Object[] { "Arabic Standard Time", "IRAQ", 6, true });
            dt.Rows.Add(new Object[] { "Afghanistan Standard Time", "AFG", 7, true });
            dt.Rows.Add(new Object[] { "Korea Standard Time", "KOREA", 8, true });

            AddColumns(dt);

            return dt;
        }

        #endregion

    }
}