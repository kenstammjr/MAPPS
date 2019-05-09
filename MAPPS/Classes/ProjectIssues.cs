using System;
using System.Data;
using System.Data.SqlClient;

namespace MAPPS {
    public class ProjectIssue {

        #region _Private Variables_

        private int _ID;
        private int _ProjectID;
        private string _Issue;
        private string _ProposedAction;
        private string _ActionTaken;
        private string _TicketNumber;
        private string _TicketUrl;
        private int _Priority;
        private bool _PacingItem;
        private DateTime _ClosedOn;
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
        public int ProjectID {
            get {
                return _ProjectID;
            }
            set {
                _ProjectID = value;
            }
        }
        public string Issue {
            get {
                return _Issue;
            }
            set {
                _Issue = value;
            }
        }
        public string ProposedAction {
            get {
                return _ProposedAction;
            }
            set {
                _ProposedAction = value;
            }
        }
        public string ActionTaken {
            get {
                return _ActionTaken;
            }
            set {
                _ActionTaken = value;
            }
        }
        public string TicketNumber {
            get {
                return _TicketNumber;
            }
            set {
                _TicketNumber = value;
            }
        }
        public string TicketUrl {
            get {
                return _TicketUrl;
            }
            set {
                _TicketUrl = value;
            }
        }
        public int Priority {
            get {
                return _Priority;
            }
            set {
                _Priority = value;
            }
        }
        public bool PacingItem {
            get {
                return _PacingItem;
            }
            set {
                _PacingItem = value;
            }
        }
        public DateTime ClosedOn {
            get {
                return _ClosedOn;
            }
            set {
                _ClosedOn = value;
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

        public ProjectIssue() {
            _ID = 0;
            _ProjectID = 0;
            _Issue = string.Empty;
            _ProposedAction = string.Empty;
            _ActionTaken = string.Empty;
            _TicketNumber = string.Empty;
            _TicketUrl = string.Empty;
            _Priority = 0;
            _PacingItem = false;
            _ClosedOn = new DateTime(1900, 1, 1);
            _CreatedBy = "System Account";
            _CreatedOn = new DateTime(1900, 1, 1);
            _ModifiedBy = "System Account";
            _ModifiedOn = new DateTime(1900, 1, 1);
            _ErrorMessage = string.Empty;
        }
        public ProjectIssue(int ID)
                : this() {
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                string sql = "SELECT * FROM dbo.ProjectIssues WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", ID);
                SqlDataReader sdr;
                try {
                    conn.Open();
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read()) {
                        _ID = int.Parse(sdr["ID"].ToString());
                        _ProjectID = int.Parse(sdr["ProjectID"].ToString());
                        _Issue = sdr["Issue"].ToString();
                        _ProposedAction = sdr["ProposedAction"].ToString();
                        _ActionTaken = sdr["ActionTaken"].ToString();
                        _TicketNumber = sdr["TicketNumber"].ToString();
                        _TicketUrl = sdr["TicketUrl"].ToString();
                        _Priority = int.Parse(sdr["Priority"].ToString());
                        _PacingItem = bool.Parse(sdr["PacingItem"].ToString());
                        _ClosedOn = DateTime.Parse(sdr["ClosedOn"].ToString());
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

        #endregion

        #region _Public Methods_

        public bool Insert() {
            bool Successful = false;
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    DateTime TimeStamp = DateTime.UtcNow;
                    string sql = @"INSERT INTO dbo.ProjectIssues 
			                                (ProjectID,
										Issue,
										ProposedAction,
										ActionTaken,
										TicketNumber,
										TicketUrl,
										Priority,
										PacingItem,
										ClosedOn,
										CreatedBy,
										CreatedOn,
										ModifiedBy,
										ModifiedOn)
			                            VALUES
			                                (@ProjectID,
										@Issue,
										@ProposedAction,
										@ActionTaken,
										@TicketNumber,
										@TicketUrl,
										@Priority,
										@PacingItem,
										@ClosedOn,
										@CreatedBy,
										@CreatedOn,
										@ModifiedBy,
										@ModifiedOn)
			                            SELECT @ID = SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ProjectID", _ProjectID);
                    cmd.Parameters.AddWithValue("@Issue", _Issue);
                    cmd.Parameters.AddWithValue("@ProposedAction", _ProposedAction);
                    cmd.Parameters.AddWithValue("@ActionTaken", _ActionTaken);
                    cmd.Parameters.AddWithValue("@TicketNumber", _TicketNumber);
                    cmd.Parameters.AddWithValue("@TicketUrl", _TicketUrl);
                    cmd.Parameters.AddWithValue("@Priority", _Priority);
                    cmd.Parameters.AddWithValue("@PacingItem", _PacingItem);
                    cmd.Parameters.AddWithValue("@ClosedOn", _ClosedOn);
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
                    string sql = @"UPDATE dbo.ProjectIssues 
			                        SET ProjectID = @ProjectID,
										Issue = @Issue,
										ProposedAction = @ProposedAction,
										ActionTaken = @ActionTaken,
										TicketNumber = @TicketNumber,
										TicketUrl = @TicketUrl,
										Priority = @Priority,
										PacingItem = @PacingItem,
										ClosedOn = @ClosedOn,
										ModifiedBy = @ModifiedBy,
										ModifiedOn = @ModifiedOn
			                        WHERE ID = @ID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID", _ID);
                    cmd.Parameters.AddWithValue("@ProjectID", _ProjectID);
                    cmd.Parameters.AddWithValue("@Issue", _Issue);
                    cmd.Parameters.AddWithValue("@ProposedAction", _ProposedAction);
                    cmd.Parameters.AddWithValue("@ActionTaken", _ActionTaken);
                    cmd.Parameters.AddWithValue("@TicketNumber", _TicketNumber);
                    cmd.Parameters.AddWithValue("@TicketUrl", _TicketUrl);
                    cmd.Parameters.AddWithValue("@Priority", _Priority);
                    cmd.Parameters.AddWithValue("@PacingItem", _PacingItem);
                    cmd.Parameters.AddWithValue("@ClosedOn", _ClosedOn);
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
                    string sql = "DELETE FROM dbo.ProjectIssues WHERE ID = @ID";
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
                    string sql = "SELECT * FROM dbo.ProjectIssues";
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
        public static DataSet Items(int ProjectID) {
            DataSet ds = new DataSet();
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    string sql = "SELECT * FROM dbo.ProjectIssues WHERE ProjectID = @ProjectID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
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
                    string sql = "SELECT * FROM dbo.ProjectIssues";
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
        #endregion
    }
}