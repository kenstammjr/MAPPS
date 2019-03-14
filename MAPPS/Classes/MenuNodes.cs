using System;
using System.Data;
using System.Data.SqlClient;

namespace MAPPS {
    public class MenuNode {

        #region _Private Variables_

        private int _ID;
        private int _ParentID;
        private int _MenuID;
        private string _Name;
        private string _Description;
        private string _URL;
        private string _Target;
        private int _DisplayIndex;
        private bool _IsVisible;
         private string _CreatedBy;
        private DateTime _CreatedOn;
        private string _ModifiedBy;
        private DateTime _ModifiedOn;
        private bool _HasChildren;
        private string _ErrorMessage;

        #endregion

        #region _Public Properties_

        public int ID {
            get { return _ID; }
        }
        public int ParentID {
            get { return _ParentID; }
            set { _ParentID = value; }
        }
        public int MenuID {
            get { return _MenuID; }
            set { _MenuID = value; }
        }
        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }
        public string Description {
            get { return _Description; }
            set { _Description = value; }
        }
        public string URL {
            get { return _URL; }
            set { _URL = value; }
        }
        public string Target {
            get { return _Target; }
            set { _Target = value; }
        }
        public int DisplayIndex {
            get { return _DisplayIndex; }
            set { _DisplayIndex = value; }
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
        public bool HasChildren {
            get { return _HasChildren; }
            set { _HasChildren = value; }
        }
        public string ErrorMessage {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; }
        }

        #endregion

        #region _Constructors_

        public MenuNode() {
            _ID = 0;
            _ParentID = 0;
            _MenuID = 0;
            _Name = string.Empty;
            _Description = string.Empty;
            _URL = string.Empty;
            _Target = "_self";
            _DisplayIndex = 0;
            _IsVisible = true;
            _CreatedOn = new DateTime(1900, 1, 1);
            _CreatedBy = "System Account";
            _ModifiedOn = new DateTime(1900, 1, 1);
            _ModifiedBy = "System Account";
            _HasChildren = false;
            _ErrorMessage = string.Empty;
        }
        public MenuNode(int ID)
            : this() {
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                string sql = "SELECT * FROM dbo.MenuNodes WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", ID);
                SqlDataReader sdr;
                try {
                    conn.Open();
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read()) {
                        _ID = int.Parse(sdr["ID"].ToString());
                        _MenuID = int.Parse(sdr["MenuID"].ToString());
                        _ParentID = int.Parse(sdr["ParentID"].ToString());
                        _Name = sdr["Name"].ToString();
                        _Description = sdr["Description"].ToString();
                        _URL = sdr["URL"].ToString();
                        _Target = sdr["Target"].ToString();
                        _DisplayIndex = int.Parse(sdr["DisplayIndex"].ToString());
                        _IsVisible = bool.Parse(sdr["IsVisible"].ToString());
                        _CreatedBy = sdr["CreatedBy"].ToString();
                        _CreatedOn = DateTime.Parse(sdr["CreatedOn"].ToString());
                        _ModifiedBy = sdr["ModifiedBy"].ToString();
                        _ModifiedOn = DateTime.Parse(sdr["ModifiedOn"].ToString());
                        _HasChildren = HasChild(_ID);
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

        private bool HasChild(int ParentID) {
            using (new Impersonator()) {
                bool hasChild = false;
                SqlConnection conn = DataSource.Conn();
                string sql = "SELECT TOP(1) ID FROM dbo.MenuNodes WHERE ParentID = @ParentID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ParentID", ParentID);
                try {
                    conn.Open();
                    object count = cmd.ExecuteScalar();
                    if (count != null) hasChild = true;
                } catch (SqlException sqlex) {
                    Error.WriteError(sqlex);
                    _ErrorMessage = sqlex.Message;
                } catch (Exception ex) {
                    Error.WriteError(ex);
                    _ErrorMessage = ex.Message;
                } finally {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
                return hasChild;
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
                    string sql = @"Insert INTO dbo.MenuNodes 
			                            (MenuID,
										ParentID,
										Name,
										Description,
										URL,
										Target,
										DisplayIndex,
										IsVisible,
                                        CreatedBy,
										CreatedOn,
										ModifiedBy,
										ModifiedOn)
			                        Values
			                            (@MenuID,
										@ParentID,
										@Name,
										@Description,
										@URL,
										@Target,
										@DisplayIndex,
										@IsVisible,
										@CreatedBy,
										@CreatedOn,
										@ModifiedBy,
										@ModifiedOn)
			                        SELECT @ID = SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MenuID", _MenuID);
                    cmd.Parameters.AddWithValue("@ParentID", _ParentID);
                    cmd.Parameters.AddWithValue("@Name", _Name);
                    cmd.Parameters.AddWithValue("@Description", _Description);
                    cmd.Parameters.AddWithValue("@URL", _URL);
                    cmd.Parameters.AddWithValue("@Target", _Target);
                    cmd.Parameters.AddWithValue("@DisplayIndex", _DisplayIndex);
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
                    string sql = @"Update dbo.MenuNodes 
			                        SET MenuID = @MenuID,
										ParentID = @ParentID,
										Name = @Name,
										Description = @Description,
										URL = @URL,
										Target = @Target,
										DisplayIndex = @DisplayIndex,
										IsVisible = @IsVisible,
										ModifiedBy = @ModifiedBy,
										ModifiedOn = @ModifiedOn
			                        WHERE ID = @ID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID", _ID);
                    cmd.Parameters.AddWithValue("@MenuID", _MenuID);
                    cmd.Parameters.AddWithValue("@ParentID", _ParentID);
                    cmd.Parameters.AddWithValue("@Name", _Name);
                    cmd.Parameters.AddWithValue("@Description", _Description);
                    cmd.Parameters.AddWithValue("@URL", _URL);
                    cmd.Parameters.AddWithValue("@Target", _Target);
                    cmd.Parameters.AddWithValue("@DisplayIndex", _DisplayIndex);
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
                    string sql = "DELETE FROM dbo.MenuNodes WHERE ID = @ID";
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

        public static bool SetPurgeFlag(int MenuID) {
            bool successful = false;

            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                const string sql = @"UPDATE dbo.MenuNodes SET Purge = 1 WHERE MenuID = @MenuID";

                SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@MenuID", MenuID);

                try {
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    cmd.ExecuteNonQuery();
                    successful = true;
                } catch (Exception ex) {
                    Error.WriteError(ex);
                } finally {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
            }
            return successful;
        }
        public static bool PurgeFlaggedRecords(int MenuID) {
            bool successful = false;

            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                const string sql = @"DELETE FROM dbo.MenuNodes WHERE Purge = 1 AND MenuID = @MenuID";

                SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@MenuID", MenuID);

                try {
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    cmd.ExecuteNonQuery();
                    successful = true;
                } catch (Exception ex) {
                    Error.WriteError(ex);
                } finally {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
            }
            return successful;
        }
    
        public static DataSet Items(int MenuID, int ParentID, bool VisibleOnly) {
            DataSet ds = new DataSet();
            try {
                using (new Impersonator()) {
                    SqlConnection conn = DataSource.Conn();
                    try {
                        string maxRecords = string.Format(" TOP ({0}) ", MAPPS.Configuration.AppSetting("MaxRecords"));

                        string sql = string.Format(@"SELECT {0}
                                                    MenuNodes.ID, 
                                                    MenuNodes.MenuID, 
                                                    MenuNodes.ParentID, 
                                                    MenuNodes.Name, 
                                                    MenuNodes.Description, 
                                                    MenuNodes.URL, 
                                                    MenuNodes.Target, 
                                                    MenuNodes.DisplayIndex, 
                                                    MenuNodes.IsVisible, 
                                                    Parents.Name AS ParentName, 
                                                    MenuNodes.CreatedOn, 
                                                    MenuNodes.CreatedBy, 
                                                    MenuNodes.ModifiedOn, 
                                                    MenuNodes.ModifiedBy
                                                FROM MenuNodes
                                                LEFT OUTER JOIN MenuNodes AS Parents ON MenuNodes.ParentID = Parents.ID
                                                WHERE MenuNodes.ParentID = @ParentID
                                                AND MenuNodes.MenuID = @MenuID
                                                ORDER BY MenuNodes.DisplayIndex, MenuNodes.Name ", maxRecords);

                        if (VisibleOnly)
                            sql = string.Format(@"SELECT {0}
                                                    MenuNodes.ID, 
                                                    MenuNodes.MenuID, 
                                                    MenuNodes.ParentID, 
                                                    MenuNodes.Name, 
                                                    MenuNodes.Description, 
                                                    MenuNodes.URL, 
                                                    MenuNodes.Target, 
                                                    MenuNodes.DisplayIndex, 
                                                    MenuNodes.IsVisible, 
                                                    Parents.Name AS ParentName, 
                                                    MenuNodes.CreatedOn, 
                                                    MenuNodes.CreatedBy, 
                                                    MenuNodes.ModifiedOn, 
                                                    MenuNodes.ModifiedBy
                                                FROM MenuNodes
                                                LEFT OUTER JOIN MenuNodes AS Parents ON MenuNodes.ParentID = Parents.ID
                                                WHERE MenuNodes.ParentID = @ParentID
                                                AND MenuNodes.MenuID = @MenuID
                                                ORDER BY MenuNodes.DisplayIndex, MenuNodes.Name ", maxRecords);

                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@MenuID", MenuID);
                        cmd.Parameters.AddWithValue("@ParentID", ParentID);
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
        public static DataSet Items(int MenuID, bool VisibleOnly) {
            DataSet ds = new DataSet();
            try {
                using (new Impersonator()) {
                    SqlConnection conn = DataSource.Conn();
                    try {
                        string sql = @"with c (Name, ID, Level, FullName)
                                        AS
                                        (select e.name, e.id, 0, cast(e.name as nvarchar(max))
                                        from dbo.menunodes e where e.parentid = 0 and isvisible = @VisibleOnly and MenuID = @MenuID
                                        union all
                                        select e.name, e.id, c.Level+1, c.FullName + '.' + e.name 
                                        from dbo.menunodes e inner join c on c.id = e.parentid where e.isvisible = @VisibleOnly and e.MenuID = @MenuID)
                                        select * from c order by FullName";
                       
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@VisibleOnly", VisibleOnly);
                        cmd.Parameters.AddWithValue("@MenuID", MenuID);
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
        public static DataSet Items(int MenuID, string Filter) {
            DataSet ds = new DataSet();
            try {
                using (new Impersonator()) {
                    SqlConnection conn = DataSource.Conn();
                    try {
                        string sql = @"with c (Name, Description, URL, ID, Level, FullName)
                                        AS
                                        (select e.name, e.description, e.url, e.id, 0, cast(e.name as nvarchar(max))
                                        from dbo.menunodes e where e.parentid = 0 and isvisible = 1 and MenuID = @MenuID
                                        union all
                                        select e.name, e.description, e.url, e.id, c.Level+1, c.FullName + '.' + e.name 
                                        from dbo.menunodes e inner join c on c.id = e.parentid where e.isvisible = 1 and e.MenuID = @MenuID)
                                        select * from c 
                                        where FullName LIKE @Filter OR Description LIKE @Filter OR Name LIKE @Filter OR URL LIKE @Filter 
                                        order by FullName";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@MenuID", MenuID);
                        cmd.Parameters.AddWithValue("@Filter", "%" + Filter + "%");

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
        public static DataSet Parents(int ChildID) {
            DataSet ds = new DataSet();
            try {
                using (new Impersonator()) {
                    SqlConnection conn = DataSource.Conn();
                    try {
                        string sql = @"with x (child, parent, level)
                                        as
                                        (select 
                                            dbo.MenuNodes.ID as child, 
                                            dbo.MenuNodes.ParentID as parent,
                                            0 
                                            from dbo.MenuNodes
                                        UNION ALL
                                        select 
                                            x.child, 
                                            dbo.MenuNodes.ParentID as parent,
                                            x.level+1
                                            from x,dbo.MenuNodes where x.parent = dbo.MenuNodes.ID)
                                        select distinct 
                                            dbo.MenuNodes.ID, 
                                            dbo.MenuNodes.Name, 
                                            dbo.MenuNodes.ParentID, 
                                            case when (y.ID IS not null) then 1 else 0 end as haschildren, 
                                            dbo.MenuNodes.DisplayIndex, 
                                            dbo.MenuNodes.URL, 
                                            dbo.MenuNodes.Description,
                                            level 
                                            from x 
                                            inner join dbo.MenuNodes on dbo.MenuNodes.ID = parent 
                                            left outer join dbo.MenuNodes as y on dbo.MenuNodes.ID = y.ParentID 
                                            where child = @ChildID
                                            order by level desc";

                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@ChildID", ChildID);
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
        public static DataSet List(int MenuID) {
            DataSet ds = new DataSet();
            try {
                using (new Impersonator()) {
                    SqlConnection conn = DataSource.Conn();
                    try {
                        string sql = string.Format(@"with c (Name, ID, Level, FullName, URL, ParentID, sort)
                                        AS
                                        (select e.name, e.id, 0, cast(e.name as nvarchar(max)), e.url, e.parentid,CONVERT(varchar(255), e.Name)
                                        from dbo.menunodes e where e.parentid = 0 and isvisible = 1 and MenuID = @MenuID
                                        union all
                                        select e.name, e.id, c.Level+1, CAST(REPLICATE('&nbsp;&nbsp;&nbsp;', c.Level) + '&nbsp;&nbsp;{0}&nbsp;' + e.name as nvarchar(max)),  e.url, e.parentid, CONVERT(varchar(255), rtrim(sort) + '|  ' + e.name) 
                                        from dbo.menunodes e inner join c on c.id = e.parentid where e.isvisible = 1 and e.MenuID = @MenuID)
                                        select * from c order by sort", "<img src=\"/_layouts/15/images/hbulletln.gif\" style=\"border-style: none\" />");

                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@MenuID", MenuID);
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

        public static DataSet Ordered() {
            DataSet ds = new DataSet();
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    string sql = @"with c (Name, Description, ID, Level, FullName)
                                        AS
                                        (select e.name, e.Description, e.id, 0, cast(e.name as nvarchar(max))
                                        from dbo.MenuNodes e where e.parentid = 0 
                                        union all
                                        select e.name, e.Description, e.id, c.Level+1, c.FullName + '.' + e.name 
                                        from dbo.MenuNodes e inner join c on c.id = e.parentid)
                                        select * from c order by FullName";
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
        public static DataSet Nested() {
            DataSet ds = new DataSet();
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    string sql = @"with c (Name, Description, ID, Level, FullName)
                                        AS
                                        (select e.name, e.Description, e.id, 0, cast(e.name as nvarchar(max))
                                        from dbo.MenuNodes e where e.parentid = 0 
                                        union all
                                        select e.name, e.Description, e.id, c.Level+1, c.FullName + ' > ' + e.name 
                                        from dbo.MenuNodes e inner join c on c.id = e.parentid)
                                        select * from c order by FullName";
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