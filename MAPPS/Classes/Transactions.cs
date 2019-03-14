using System;
using System.Data;
using System.Data.SqlClient;

namespace MAPPS
{
    public partial class Transaction
    {
        //Transaction Categories
        public const string TYPE_DELETE = "Delete";
        public const string TYPE_FAILURE = "Failure";
        public const string TYPE_SUCCESS = "Successful";
        public const string TYPE_TIMERJOB = "TimerJob";
         

        public enum TransactionType
        {
            Delete = 1,
            Failure = 2,
            Successful = 3,
            TimerJob = 4
        }

        #region _Private Variables_

        private int _ID;
        private string _Category;
        private string _Type;
        private string _Action;
        private string _ResourceID;
        private string _CreatedBy;
        private DateTime _CreatedOn;
        private string _ErrorMessage;

        #endregion

        #region _Public Properties_

        public int ID
        {
            get { return _ID; }
        }

        public string Category
        {
            get { return _Category; }
            set { _Category = value; }
        }

        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public string Action
        {
            get { return _Action; }
            set { _Action = value; }
        }

        public string ResourceID
        {
            get { return _ResourceID; }
            set { _ResourceID = value; }
        }

        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        public DateTime CreatedOn
        {
            get { return _CreatedOn; }
            private set { _CreatedOn = value; }
        }

        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        #endregion

        #region _Constructors_

        /// <summary>
        /// Creates an empty Transaction object
        /// </summary>
        public Transaction()
        {
            _ID = 0;
            _Category = string.Empty;
            _Type = string.Empty;
            _Action = string.Empty;
            _ResourceID = string.Empty;
            _CreatedBy = "System Account";
            _CreatedOn = new DateTime(1900, 1, 1);
            _ErrorMessage = string.Empty;
        }

        /// <summary>
        /// Creates a populated Transaction object based on the supplied parameters
        /// </summary>
        /// <param name="category"></param>
        /// <param name="type"></param>
        /// <param name="action"></param>
        /// <param name="resourceID"></param>
        /// <param name="createdBy"></param>
        public Transaction(string category, TransactionType type, string action, string resourceID, string createdBy)
        {
            _ID = 0;
            _Category = category;
            _Type = type.ToString();
            _Action = action;
            _ResourceID = resourceID;
            _CreatedBy = createdBy;
            _CreatedOn = new DateTime(1900, 1, 1);
            _ErrorMessage = string.Empty;
        }

        /// <summary>
        /// Creates a populated Transaction object based on the supplied ID
        /// </summary>
        /// <param name="ID"></param>
        public Transaction(int ID)
            : this()
        {
            using (new Impersonator())
            {
                SqlConnection conn = DataSource.Conn();
                string sql = "SELECT * FROM dbo.Transactions WHERE ID = @ID";
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
                        _Category = sdr["Category"].ToString();
                        _Type = sdr["Type"].ToString();
                        _Action = sdr["Action"].ToString();
                        _ResourceID = sdr["ResourceID"].ToString();
                        _CreatedBy = sdr["CreatedBy"].ToString();
                        _CreatedOn = DateTime.Parse(sdr["CreatedOn"].ToString());
                        _ErrorMessage = string.Empty;
                    }
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
        }

        #endregion

        #region _Private Methods_

        /// <summary>
        /// Returns the Transaction Type based on the supplied enum value.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string GetType(TransactionType type)
        {
            string strType = string.Empty;
            switch (type)
            {
                case TransactionType.Delete:
                    strType = TYPE_DELETE;
                    break;
                case TransactionType.Failure:
                    strType = TYPE_FAILURE;
                    break;
                case TransactionType.Successful:
                    strType = TYPE_SUCCESS;
                    break;
                case TransactionType.TimerJob:
                    strType = TYPE_TIMERJOB;
                    break;
            }
            return strType.Trim();
        }
        #endregion

        #region _Public Methods_

        /// <summary>
        /// Inseerts a Transaction record
        /// </summary>
        /// <returns></returns>
        public bool Insert()
        {
            bool Successful = false;
            using (new Impersonator())
            {
                using (SqlConnection conn = DataSource.Conn())
                {

                    try
                    {
                        DateTime timeStamp = DateTime.UtcNow;
                        CreatedOn = timeStamp;
                        string sql = @"Insert INTO dbo.Transactions 
										(Category
										,Type
										,Action
										,ResourceID
										,CreatedBy
										,CreatedOn)
							Values
										(@Category
										,@Type
										,@Action
										,@ResourceID
										,@CreatedBy
										,@CreatedOn)
							SELECT @ID = SCOPE_IDENTITY()";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@Category", _Category);
                        cmd.Parameters.AddWithValue("@Type", _Type);
                        cmd.Parameters.AddWithValue("@Action", _Action);
                        cmd.Parameters.AddWithValue("@ResourceID", _ResourceID);
                        cmd.Parameters.AddWithValue("@CreatedBy", _CreatedBy);
                        cmd.Parameters.AddWithValue("@CreatedOn", CreatedOn);
                        SqlParameter prmID = cmd.Parameters.Add("@ID", SqlDbType.Int);
                        prmID.Direction = ParameterDirection.Output;
                        if (conn.State != ConnectionState.Open) { conn.Open(); }
                        cmd.ExecuteNonQuery();
                        _ID = int.Parse(prmID.Value.ToString());
                        Successful = true;
                    }
                    catch (Exception ex)
                    {
                        Error.WriteError(ex);
                        _ErrorMessage = ex.Message;
                    }
                }
            }
            return Successful;
        }

        /// <summary>
        /// Updates a Transaction record
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            bool Successful = false;
            using (new Impersonator())
            {
                using (SqlConnection conn = DataSource.Conn())
                {
                    try
                    {
                        DateTime timeStamp = DateTime.UtcNow;
                        string sql = @"Update dbo.Transactions 
										SET Category = @Category
										,Type = @Type
										,Action = @Action
										,ResourceID = @ResourceID
							WHERE ID = @ID";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@Category", _Category);
                        cmd.Parameters.AddWithValue("@Type", _Type);
                        cmd.Parameters.AddWithValue("@Action", _Action);
                        cmd.Parameters.AddWithValue("@ResourceID", _ResourceID);
                        cmd.Parameters.AddWithValue("@ID", _ID);
                        if (conn.State != ConnectionState.Open) { conn.Open(); }
                        cmd.ExecuteNonQuery();
                        Successful = true;
                    }
                    catch (Exception ex)
                    {
                        Error.WriteError(ex);
                        _ErrorMessage = ex.Message;
                    }
                }
            }
            return Successful;
        }

        /// <summary>
        /// Deletes a Transaction record.
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            bool Successful = false;
            using (new Impersonator())
            {
                using (SqlConnection conn = DataSource.Conn())
                {
                    try
                    {
                        string sql = "DELETE FROM dbo.Transactions WHERE ID = @ID";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@ID", _ID);
                        if (conn.State != ConnectionState.Open) { conn.Open(); }
                        cmd.ExecuteNonQuery();
                        Successful = true;
                    }
                    catch (Exception ex)
                    {
                        Error.WriteError(ex);
                        _ErrorMessage = ex.Message;
                    }
                }
            }
            return Successful;
        }

        /// <summary>
        /// Returns a list of Transaction records regardless of MaxRecords value. 
        /// </summary>
        /// <returns></returns>
        public static DataSet Items()
        {
            DataSet ds = new DataSet();
            using (new Impersonator())
            {
                using (SqlConnection conn = DataSource.Conn())
                {
                    try
                    {
                        string sql = "SELECT * FROM dbo.Transactions ";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                    }
                    catch (Exception ex)
                    {
                        Error.WriteError(ex);
                    }
                }
            }
            return ds;
        }


        /// <summary>
        /// Returns a list of Transactions based on the Transaction type enum and search text provided
        /// </summary>
        /// <param name="type">Transaction Type Enum</param>
        /// <param name="searchText"></param>
        /// <returns>DataTable:Id,Category,Type,Action,CreatedBy,CreatedOn(Local Time)</returns>
        public static DataTable TransactionsList(string transactionType, string searchText)
        {
            DataTable dt = new DataTable();
            string transType = transactionType;
            using (new Impersonator())
            {
                using (SqlConnection conn = DataSource.Conn())
                {
                    try
                    {
                        string sql = @"SELECT TOP " + Setting.KeyValue("MaxRecords") +
                                            @"Id,
                                        Category,
                                        Type,
                                         Action,
                                        CreatedBy,
                                        CreatedOn 
                                 FROM dbo.Transactions 
                                 WHERE Type = @Type";
                    if (!searchText.IsNullOrWhiteSpace())
                    {
                         sql += @" AND (
                                           (Category like @searchText)
                                           OR (Action like @searchText)
                                           OR (CreatedBy like @searchText)
                                           OR (CreatedOn like @searchText)
                                        ) ";// "like" searches don't have awesome performance however cannot depend on FullTextSearch. Build indexes as required to boost speed.
                    }
                        sql += " ORDER BY CreatedOn Desc";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@Type", transType);
                        if (!searchText.IsNullOrWhiteSpace())
                            cmd.Parameters.AddWithValue("@searchText", "%" + @searchText + "%");
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        foreach (DataRow dr in dt.Rows)
                        {
                            dr["CreatedOn"] = DateTime.Parse(dr["CreatedOn"].ToString()).ToLocalTime();
                        }
                    }
                    catch (Exception ex)
                    {
                        Error.WriteError(ex);
                    }
                }
            }
            return dt;
        }

         /// <summary>
        /// Get a List of ALL Transactions
        /// </summary>
        /// <returns>DataTable:Id,Category,Type,Action,CreatedBy,CreatedOn(Local Time)</returns>
        public static DataTable TransactionsList(string searchText)
        {
            DataTable dt = new DataTable();
            using (new Impersonator())
            {
                using (SqlConnection conn = DataSource.Conn())
                {

                    try
                    {
                        string sql = @"SELECT TOP " + Setting.KeyValue("MaxRecords") +
                                                @"Id,
                                            Category,
                                            Type,
                                            Action,
                                            CreatedBy,
                                            CreatedOn 
                                 FROM dbo.Transactions 
                                 WHERE 1=1 ";
                         if (!searchText.IsNullOrWhiteSpace())
                    {
                         sql += @" AND (
                                           (Category like @searchText)
                                           OR (Action like @searchText)
                                           OR (CreatedBy like @searchText)
                                           OR (CreatedOn like @searchText)
                                        ) ";// "like" searches don't have awesome performance however cannot depend on FullTextSearch. Build indexes as required to boost speed.
                    }
                        sql += " ORDER BY CreatedOn Desc";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        if (!searchText.IsNullOrWhiteSpace())
                            cmd.Parameters.AddWithValue("@searchText", "%" + @searchText + "%");
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        foreach (DataRow dr in dt.Rows)
                        {
                            dr["CreatedOn"] = DateTime.Parse(dr["CreatedOn"].ToString()).ToLocalTime();
                        }


                    }
                    catch (Exception ex)
                    {
                        Error.WriteError(ex);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// Get a list of transactions for a category and record.
        /// </summary>
        /// <param name="Category">Use the Transaction Constants to ensure consitency</param>
        /// <param name="ResourceID">Record ID of the entity</param>
        /// <returns>DataTable: Action,CreatedBy,CreatedOn(GMT)</returns>
        /// <example>Transaction.TransactionList(Transaction.CATEGORY_REQUEST,1)</example>
        public static DataTable TransactionsList(string Category, int ResourceID)
        {
            DataTable dt = new DataTable();
            using (new Impersonator())
            {
                using (SqlConnection conn = DataSource.Conn())
                {
                    try
                    {
                        const string sql = @"SELECT Id,Action,CreatedBy, CreatedOn
                                        FROM dbo.Transactions 
                                        WHERE Category = @Category AND ResourceID = @ResourceID 
                                        Order by CreatedOn Desc";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@Category", Category);
                        cmd.Parameters.AddWithValue("@ResourceID", ResourceID);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        Error.WriteError(ex);
                    }
                }
            }
            return dt;
        }

        

        /// <summary>
        /// Purge Transactions for that transaction type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool PurgeTransactions(TransactionType type)
        {
            bool bSuccess = true;
            string transType = GetType(type);
            using (new Impersonator())
            {
                using (SqlConnection conn = DataSource.Conn())
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        const string sql = @"DELETE FROM dbo.Transactions 
                                        WHERE Type = @Type";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@Type", transType);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                    }
                    catch (Exception ex)
                    {
                        bSuccess = false;
                        Error.WriteError(ex);
                    }
                }
            }
            return bSuccess;
        }

        /// <summary>
        /// Puges Transaction records 
        /// </summary>
        /// <returns></returns>
        public static bool PurgeTransactions()
        {
            bool bSuccess = true;
            using (new Impersonator())
            {
                using (SqlConnection conn = DataSource.Conn())
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        string sql = "TRUNCATE TABLE dbo.Transactions";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                    }
                    catch (Exception ex)
                    {
                        bSuccess = false;
                        Error.WriteError(ex);
                    }
                }
            }
            return bSuccess;
        }

        #endregion

    }
}
