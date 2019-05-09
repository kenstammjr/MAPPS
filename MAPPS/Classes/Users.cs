using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MAPPS {
    public class User {

        #region _Private Variables_

        private int _ID;
        private string _UserName;
        private string _Email;
        private string _ADObjectGuid;
        private string _SPObjectGuid;
        private int _UserProfileRecordID;
        private string _LastName;
        private string _FirstName;
        private string _MiddleInitial;
        private string _GenerationalQualifier;
        private string _PreferredName;
        private string _DutyTitle;
        private string _Salt;
        private bool _SeniorStaff;
        private bool _ITAdmin;
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
        public string UserName {
            get {
                return _UserName;
            }
            set {
                _UserName = value;
            }
        }
        public string Email {
            get {
                return _Email;
            }
            set {
                _Email = value;
            }
        }
        public string ADObjectGuid {
            get {
                return _ADObjectGuid;
            }
            set {
                _ADObjectGuid = value;
            }
        }
        public string SPObjectGuid {
            get {
                return _SPObjectGuid;
            }
            set {
                _SPObjectGuid = value;
            }
        }
        public int UserProfileRecordID {
            get {
                return _UserProfileRecordID;
            }
            set {
                _UserProfileRecordID = value;
            }
        }
        public string LastName {
            get {
                return _LastName;
            }
            set {
                _LastName = value;
            }
        }
        public string FirstName {
            get {
                return _FirstName;
            }
            set {
                _FirstName = value;
            }
        }
        public string MiddleInitial {
            get {
                return _MiddleInitial;
            }
            set {
                _MiddleInitial = value;
            }
        }
        public string GenerationalQualifier {
            get {
                return _GenerationalQualifier;
            }
            set {
                _GenerationalQualifier = value;
            }
        }
        public string PreferredName {
            get {
                return _PreferredName;
            }
            set {
                _PreferredName = value;
            }
        }
        public string DutyTitle {
            get {
                return _DutyTitle;
            }
            set {
                _DutyTitle = value;
            }
        }
        public string Salt {
            get {
                return _Salt;
            }
            set {
                _Salt = value;
            }
        }
        public bool SeniorStaff {
            get {
                return _SeniorStaff;
            }
            set {
                _SeniorStaff = value;
            }
        }
                public bool ITAdmin {
            get {
                return _ITAdmin;
            }
            set {
                _ITAdmin = value;
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
        public string DisplayName {
            get { return _DisplayName(); }
        }
        public string Roles {
            get { return CleanUserRoles(); }
        }

        #endregion

        #region _Constructors_

        public User() {
            _ID = 0;
            _UserName = string.Empty;
            _Email = string.Empty;
            _ADObjectGuid = string.Empty;
            _SPObjectGuid = string.Empty;
            _UserProfileRecordID = 0;
            _LastName = string.Empty;
            _FirstName = string.Empty;
            _MiddleInitial = string.Empty;
            _GenerationalQualifier = string.Empty;
            _PreferredName = string.Empty;
            _DutyTitle = string.Empty;
            _Salt = string.Empty;
            _SeniorStaff = false;
            _ITAdmin = false;
            _CreatedBy = "System Account";
            _CreatedOn = new DateTime(1900, 1, 1);
            _ModifiedBy = "System Account";
            _ModifiedOn = new DateTime(1900, 1, 1);
            _ErrorMessage = string.Empty;
        }
        public User(int ID)
            : this() {
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                string sql = "SELECT * FROM dbo.Users WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", ID);
                SqlDataReader sdr;
                try {
                    conn.Open();
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read()) {
                        //_Salt = sdr["Salt"].ToString();
                        //byte[] key = Common.RestoreKey(Common.Decrypt(_Salt));
                        _ID = int.Parse(sdr["ID"].ToString());
                        _UserName = sdr["UserName"].ToString();
                        _Email = sdr["Email"].ToString();
                        _ADObjectGuid = sdr["ADObjectGuid"].ToString();
                        _SPObjectGuid = sdr["SPObjectGuid"].ToString();
                        _UserProfileRecordID = int.Parse(sdr["UserProfileRecordID"].ToString());
                        _LastName = sdr["LastName"].ToString();
                        _FirstName = sdr["FirstName"].ToString();
                        _MiddleInitial = sdr["MiddleInitial"].ToString();
                        _GenerationalQualifier = sdr["GenerationalQualifier"].ToString();
                        _PreferredName = sdr["PreferredName"].ToString();
                        _DutyTitle = sdr["DutyTitle"].ToString();
                        _Salt = sdr["Salt"].ToString();
                        _SeniorStaff = bool.Parse(sdr["SeniorStaff"].ToString());
                        _ITAdmin = bool.Parse(sdr["ITAdmin"].ToString());
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
        public User(string Identity)
            : this() {
            string identity = Identity.Replace("i:0#.w|", "").Replace("0#.w|", "");
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                string sql = "SELECT * FROM dbo.Users WHERE UserName = @Identity";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Identity", identity);
                SqlDataReader sdr;
                try {
                    conn.Open();
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read()) {
                        //_Salt = sdr["Salt"].ToString();
                        //byte[] key = Common.RestoreKey(Common.Decrypt(_Salt));
                        _ID = int.Parse(sdr["ID"].ToString());
                        _UserName = sdr["UserName"].ToString();
                        _Email = sdr["Email"].ToString();
                        _ADObjectGuid = sdr["ADObjectGuid"].ToString();
                        _SPObjectGuid = sdr["SPObjectGuid"].ToString();
                        _UserProfileRecordID = int.Parse(sdr["UserProfileRecordID"].ToString());
                        _LastName = sdr["LastName"].ToString();
                        _FirstName = sdr["FirstName"].ToString();
                        _MiddleInitial = sdr["MiddleInitial"].ToString();
                        _GenerationalQualifier = sdr["GenerationalQualifier"].ToString();
                        _PreferredName = sdr["PreferredName"].ToString();
                        _DutyTitle = sdr["DutyTitle"].ToString();
                        _Salt = sdr["Salt"].ToString();
                        _SeniorStaff = bool.Parse(sdr["SeniorStaff"].ToString());
                        _ITAdmin = bool.Parse(sdr["ITAdmin"].ToString());
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

        private string UserRoles() {
            string roles = "User";
            DataSet dsAppRoles = SecurityGroupMembership.UserItems(ID);
            foreach (DataRow drAppRoles in dsAppRoles.Tables[0].Rows)
                roles = drAppRoles["Role"].ToString().Trim() + "|" + roles;
            return roles;
        }

        private string CleanUserRoles() {
            string roles = "App-User";
            DataSet dsAppRoles = SecurityGroupMembership.UserItems(ID);
            foreach (DataRow drAppRoles in dsAppRoles.Tables[0].Rows)
                roles = drAppRoles["AppRole"].ToString().Trim() + ", " + roles;

            return roles;

        }
        private string _DisplayName() {
            return string.Format("{0}, {1} {2} {3}", _LastName, _FirstName, _MiddleInitial, _GenerationalQualifier);
        }

        private bool HasRole(string Role) {
            string[] UserRoleCollection = UserRoles().Split('|');
            for (int n = 0; n < UserRoleCollection.Length; n++)
                if (UserRoleCollection[n] == Role)
                    return true;
            return false;
        }

        private bool UserInGroupMembership() {
            bool exists = false;
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                const string sql = @"SELECT Count(ID) AS IDCount
                                       FROM dbo.SecurityGroupMemberships 
                                      WHERE UserID = @UserID ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@UserID", ID);
                try {
                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read()) {
                        if (int.Parse(sdr["IDCount"].ToString()) > 0) {
                            exists = true;
                        }
                    }
                } catch (Exception ex) {
                    Error.WriteError(ex);
                } finally {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
                return exists;
            }
        }

        #endregion

        #region _Public Methods_

        public bool Insert() {
            bool Successful = false;
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    string salt = Common.GenerateSalt();
                    byte[] key = Common.RestoreKey(salt);

                    DateTime TimeStamp = DateTime.UtcNow;
                    string sql = @"INSERT INTO dbo.Users 
			                                (UserName,
										Email,
										ADObjectGuid,
										SPObjectGuid,
										UserProfileRecordID,
										LastName,
										FirstName,
										MiddleInitial,
										GenerationalQualifier,
										PreferredName,
										DutyTitle,
										Salt,
										SeniorStaff,
                                        ITAdmin,
										CreatedBy,
										CreatedOn,
										ModifiedBy,
										ModifiedOn)
			                            VALUES
			                                (@UserName,
										@Email,
										@ADObjectGuid,
										@SPObjectGuid,
										@UserProfileRecordID,
										@LastName,
										@FirstName,
										@MiddleInitial,
										@GenerationalQualifier,
										@PreferredName,
										@DutyTitle,
										@Salt,
										@SeniorStaff,
                                        @ITAdmin,
										@CreatedBy,
										@CreatedOn,
										@ModifiedBy,
										@ModifiedOn)
			                            SELECT @ID = SCOPE_IDENTITY()";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@UserName", _UserName);
                    cmd.Parameters.AddWithValue("@Email", _Email);
                    cmd.Parameters.AddWithValue("@ADObjectGuid", _ADObjectGuid);
                    cmd.Parameters.AddWithValue("@SPObjectGuid", _SPObjectGuid);
                    cmd.Parameters.AddWithValue("@UserProfileRecordID", _UserProfileRecordID);
                    cmd.Parameters.AddWithValue("@LastName", _LastName);
                    cmd.Parameters.AddWithValue("@FirstName", _FirstName);
                    cmd.Parameters.AddWithValue("@MiddleInitial", _MiddleInitial);
                    cmd.Parameters.AddWithValue("@GenerationalQualifier", _GenerationalQualifier);
                    cmd.Parameters.AddWithValue("@PreferredName", _PreferredName);
                    cmd.Parameters.AddWithValue("@DutyTitle", _DutyTitle);
                    cmd.Parameters.AddWithValue("@Salt", Common.Encrypt(salt));
                    cmd.Parameters.AddWithValue("@SeniorStaff", _SeniorStaff);
                    cmd.Parameters.AddWithValue("@ITAdmin", _ITAdmin);
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
                    byte[] key = Common.RestoreKey(Common.Decrypt(_Salt));
                    DateTime TimeStamp = DateTime.UtcNow;
                    string sql = @"UPDATE dbo.Users 
			                        SET UserName = @UserName,
										Email = @Email,
										ADObjectGuid = @ADObjectGuid,
										SPObjectGuid = @SPObjectGuid,
										UserProfileRecordID = @UserProfileRecordID,
										LastName = @LastName,
										FirstName = @FirstName,
										MiddleInitial = @MiddleInitial,
										GenerationalQualifier = @GenerationalQualifier,
										PreferredName = @PreferredName,
										DutyTitle = @DutyTitle,
										Salt = @Salt,
										SeniorStaff = @SeniorStaff,
										ITAdmin = @ITAdmin,
										ModifiedBy = @ModifiedBy,
										ModifiedOn = @ModifiedOn
			                        WHERE ID = @ID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID", _ID);
                    cmd.Parameters.AddWithValue("@UserName", _UserName);
                    cmd.Parameters.AddWithValue("@Email", _Email);
                    cmd.Parameters.AddWithValue("@ADObjectGuid", _ADObjectGuid);
                    cmd.Parameters.AddWithValue("@SPObjectGuid", _SPObjectGuid);
                    cmd.Parameters.AddWithValue("@UserProfileRecordID", _UserProfileRecordID);
                    cmd.Parameters.AddWithValue("@LastName", _LastName);
                    cmd.Parameters.AddWithValue("@FirstName", _FirstName);
                    cmd.Parameters.AddWithValue("@MiddleInitial", _MiddleInitial);
                    cmd.Parameters.AddWithValue("@GenerationalQualifier", _GenerationalQualifier);
                    cmd.Parameters.AddWithValue("@PreferredName", _PreferredName);
                    cmd.Parameters.AddWithValue("@DutyTitle", _DutyTitle);
                    cmd.Parameters.AddWithValue("@Salt", _Salt);
                    cmd.Parameters.AddWithValue("@SeniorStaff", _SeniorStaff);
                    cmd.Parameters.AddWithValue("@ITAdmin", _ITAdmin);
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
                    string sql = "DELETE FROM dbo.Users WHERE ID = @ID";
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
            return Items(string.Empty);
        }
        public static DataSet Items(string Filter) {
            DataSet ds = new DataSet();
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    string filter = string.Empty;
                    string maxRecords = string.Format(" TOP ({0}) ", MAPPS.Configuration.AppSetting("MaxRecords"));
                    if (Filter.Length != 0)
                        filter = @" AND (Users.UserName LIKE @Filter 
                                    OR Users.FirstName LIKE @Filter
                                    OR Users.UserName LIKE @Filter
                                    OR Users.PreferredName LIKE @Filter
                                    OR Users.DutyTitle LIKE @Filter)";

                    string sql = string.Format(@"SELECT {0} 
                                        Users.ID, 
                                        Users.UserName, 
                                        Users.Email, 
                                        Users.ADObjectGuid, 
                                        Users.SPObjectGuid, 
                                        Users.UserProfileRecordID, 
                                        Users.LastName, 
                                        Users.FirstName, 
                                        Users.MiddleInitial, 
                                        Users.GenerationalQualifier, 
                                        Users.PreferredName, 
                                        Users.DutyTitle, 
                                        Users.Salt,
                                        Users.SeniorStaff, 
                                        Users.ITAdmin, 
                                        Users.CreatedBy, 
                                        Users.CreatedOn, 
                                        Users.ModifiedBy, 
                                        Users.ModifiedOn
                                    FROM Users 
                                    WHERE 1= 1
                                    {1}
                                    ORDER BY Users.LastName", maxRecords, filter);

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

        public static DataSet Admins() {
            DataSet ds = new DataSet();
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    string sql = @"SELECT Users.ID, 
                                        Users.UserName, 
                                        Users.Email, 
                                        Users.ADObjectGuid, 
                                        Users.SPObjectGuid, 
                                        Users.UserProfileRecordID, 
                                        Users.LastName, 
                                        Users.FirstName, 
                                        Users.MiddleInitial, 
                                        Users.GenerationalQualifier, 
                                        Users.PreferredName, 
                                        Users.DutyTitle, 
                                        Users.Salt,
                                        Users.SeniorStaff, 
                                        Users.ITAdmin,
                                        Users.LastName + ', ' + Users.FirstName + ' ' + Users.MiddleInitial + ' ' +  Users.GenerationalQualifier as DisplayName, 
                                        Users.CreatedBy, 
                                        Users.CreatedOn, 
                                        Users.ModifiedBy, 
                                        Users.ModifiedOn
                                    FROM Users 
                                    WHERE ITAdmin = 1
                                    ORDER BY Users.LastName";

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


        public static bool IdentityExists(string UserIdentity) {
            bool found = false;

            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                const string sql = @"SELECT Count(ID) AS cnt 
                                    FROM dbo.Users 
                                    WHERE UserName = @UserIdentity";

                SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@UserIdentity", UserIdentity.Replace("i:0#.w|", "").Replace("0#.w|", ""));
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
        public static bool ADObjectGuidExists(string ADObjectGuid) {
            bool found = false;

            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                const string sql = @"SELECT Count(ID) AS cnt 
                                    FROM dbo.Users 
                                    WHERE ADObjectGuid = @ADObjectGuid";

                SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ADObjectGuid", ADObjectGuid);
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
        public bool InRole(string Role) {
            try {
                //If user is not in the SecurityGroupMenbership table we dont need to go any further
                if (!UserInGroupMembership())
                    return false;

                string EffectiveRoles = Role;
                Role role = new Role(Role);
                int parentID = role.ParentRoleID;
                while (parentID != 0) {
                    Role nextRole = new Role(parentID);
                    parentID = nextRole.ParentRoleID;
                    EffectiveRoles = nextRole.Name + "|" + EffectiveRoles;
                }
                string[] EffectiveRolesCollection = EffectiveRoles.Split('|');
                for (int n = 0; n < EffectiveRolesCollection.Length; n++) {
                    if (HasRole(EffectiveRolesCollection[n]))
                        return true;
                }
            } catch (Exception ex) {
                Error.WriteError(ex);
                _ErrorMessage = ex.Message;
            }
            return false;
        }

        public string EffectiveAppRoles(string Role) {
            string EffectiveRoles = Role;
            Role role = new Role(Role);
            int parentID = role.ParentRoleID;
            while (parentID != 0) {
                Role nextRole = new Role(parentID);
                parentID = nextRole.ParentRoleID;
                EffectiveRoles = nextRole.Name + "|" + EffectiveRoles;
            }
            return EffectiveRoles;
        }

        public string Administrators() {
            string administrators = string.Empty;


            return administrators;
        }

        public static int Count() {
            int count = 0;
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                const string sql = @"SELECT Count(ID) AS cnt 
                                    FROM dbo.Users";

                SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
                cmd.CommandType = CommandType.Text;
                try {
                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read()) {
                        count = int.Parse(sdr["cnt"].ToString());
                    }
                } catch (Exception ex) {
                    Error.WriteError(ex);
                } finally {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
            }
            return count;
        }


        #endregion
    }
}