using System;
using System.Data;
using System.Data.SqlClient;

namespace MAPPS
{
    class Utilities
    {

        // Remember to set _ErrorMessage with ex.Message as they are encountered.

        #region _Private Variables_

        private string _ErrorMessage;

        #endregion _Private Variables_

        #region _Public Properties_

        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }
        public bool HasError
        {
            get { return (!_ErrorMessage.IsNullOrWhiteSpace()); }
        }

        #endregion _Public Properties_

        #region _Constructors_

        public Utilities()
        {
            _ErrorMessage = string.Empty;
        }

        #endregion _Constructors_

        #region _Private Methods_

        #endregion _Private Methods_

        #region _Public Methods_

        /// <summary>
        /// Returns the total users in the table.
        /// </summary>
        /// <returns></returns>
        public static int GetTotalUsers()
        {
            int totalUsers = 0;
            using (new Impersonator())
            using (SqlConnection conn = DataSource.Conn())
            {
                try
                {
                    string sql = @"SELECT Count(*)
                                    FROM dbo.Users";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    totalUsers = (Int32)cmd.ExecuteScalar();
                }
                catch (SqlException sqlex)
                {
                    Error.WriteError(sqlex);
                }
                catch (Exception ex)
                {
                    Error.WriteError(ex);
                }
            }
            return totalUsers;
        }
        /// <summary>
        /// Returns the total users filtered by domain.
        /// </summary>
        /// <returns></returns>
        public static int GetTotalUsers(string domain)
        {
            int totalUsers = 0;
            using (new Impersonator())
            using (SqlConnection conn = DataSource.Conn())
            {
                try
                {
                    string sql = @"SELECT Count(*)
                                    FROM dbo.Users
                                    WHERE Domain = @domain";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@domain", domain);
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    totalUsers = (Int32)cmd.ExecuteScalar();
                }
                catch (SqlException sqlex)
                {
                    Error.WriteError(sqlex);
                }
                catch (Exception ex)
                {
                    Error.WriteError(ex);
                }
            }
            return totalUsers;
        }
 
        #endregion _Public Methods_

    }

    /// <summary>
    /// Custom implementation of .net 4.0 Tuple. (double). Consider KeyValuePair<T1,T2> if using this.
    /// </summary>
    /// <typeparam name="T1">First</typeparam>
    /// <typeparam name="T2">Second</typeparam>
    public class CustomTuple<T1, T2>
    {
        public T1 First { get; private set; }
        public T2 Second { get; private set; }
        internal CustomTuple(T1 first, T2 second)
        {
            First = first;
            Second = second;
        }
    }
    /// <summary>
    /// Custom implementation of .net 4.0 Tuple. (triple)
    /// </summary>
    /// <typeparam name="T1">First</typeparam>
    /// <typeparam name="T2">Second</typeparam>
    /// <typeparam name="T3">Third</typeparam>
    public class CustomTuple<T1, T2,T3>
    {
        //Custom implementation of .net 4.0 Tuple
        public T1 First { get; private set; }
        public T2 Second { get; private set; }
        public T3 Third { get; private set; }
        internal CustomTuple(T1 first, T2 second, T3 third)
        {
            First = first;
            Second = second;
            Third = third;
        }
    }
    /// <summary>
    /// Custom implementation of .net 4.0 Tuple. (quadrupal)
    /// </summary>
    /// <typeparam name="T1">First</typeparam>
    /// <typeparam name="T2">Second</typeparam>
    /// <typeparam name="T3">Third</typeparam>
    /// <typeparam name="T4">Fourth</typeparam>
    public class CustomTuple<T1, T2, T3, T4>
    {
        //Custom implementation of .net 4.0 Tuple
        public T1 First { get; private set; }
        public T2 Second { get; private set; }
        public T3 Third { get; private set; }
        public T4 Fourth { get; private set; }
        internal CustomTuple(T1 first, T2 second, T3 third, T4 fourth)
        {
            First = first;
            Second = second;
            Third = third;
            Fourth = fourth;
        }
    }
    /// <summary>
    /// Static class call allows for implicit conversion to support tuple comparisons.
    /// </summary>
    public static class CustomTuple
    {
        public static CustomTuple<T1, T2> New<T1, T2>(T1 first, T2 second)
        {
            var customTuple = new CustomTuple<T1, T2>(first, second);
            return customTuple;
        }
        public static CustomTuple<T1, T2, T3> New<T1, T2, T3>(T1 first, T2 second, T3 third)
        {
            var customTuple = new CustomTuple<T1, T2, T3>(first, second, third);
            return customTuple;
        }
        public static CustomTuple<T1, T2, T3, T4> New<T1, T2, T3, T4>(T1 first, T2 second, T3 third, T4 fourth)
        {
            var customTuple = new CustomTuple<T1, T2, T3, T4>(first, second, third, fourth);
            return customTuple;
        }
    }

}
