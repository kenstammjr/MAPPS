using System;
using System.Data;
using System.Data.SqlClient;

namespace MAPPS {
    public class Message {
        public const string URL_USERMESSAGE = "/_layouts/mapps/pages/usermessage.aspx";
        public const string URL_DBUTILITY = "/_layouts/mapps/pages/databaseutility.aspx";
        public const string URL_ADMINISTRATION = "/_layouts/mapps/pages/administration.aspx";

        // CRUD Messages
        public const string CANNOT_DELETE_ASSOCIATED_ITEMS = "Delete is not allowed when an item is associated with one or more items.";
        public const string CANNOT_DELETE_DEFAULT_RECORD = "Can not delete the default record. <br/> Please select another item as the default and try again.";
        public const string CANNOT_DISABLE_LAST_ADMINISTRATOR = "<br />Last Administrator User Account cannot be set to disabled.";
        public const string CANNOT_DISABLE_LAST_MANAGER = "<br />Last Manager User Account cannot be set to disabled.";
        public const string ITEM_ALREADY_EXISTS = "<br />The item alrealy exists in the database.";
        //Form Validation Messages
        public const string REQUIRED_FIELD = "<br />This is a required field.";
        public const string REQUIRED_FIELD_NUMBER = "<br />The value of this field is not a valid number.";
        public const string REQUIRED_FIELD_NUMBER_EXCEEDED = "<br />The value of this field exceeded maximum allowable number.";
        public const string REQUIRED_FIELD_DATE = "<br />You must specify a valid date.";
        public const string REQUIRED_FIELD_DATE_NOTBEFORE = "<br />Date can not be earlier than today's date.";
        public const string REQUIRED_FIELD_DATE_MAXIMUM_EXCEEDED = "<br />Date can not be greater than maximum allowable date.";
        public const string CHECKED_FIELD = "<br />A checked value is locked and may not be changed.";
        public const string EMAIL_ADDRESS_INVALID_FORMAT = "<br />Email address improperly formatted.";
        public const string IPADDRESS_INVALID_FORMAT = "<br />IP address improperly formatted.";
        //List Messages
        public const string EMPTY_LIST = "&nbsp;&nbsp;There are no items to show for this list.";
        public const string CONTACT_EXISTS = "The contact already exists.";
        public const string EMPTY_LIST_NO_NEW = "&nbsp;&nbsp;There are no items to show for this list.";
        public const string EMPTY_LIST_SEARCHED = "<br>No results matching your search were found.<br><br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1.&nbsp;&nbsp;Check your spelling. Are the words in your query spelled correctly?<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.&nbsp;&nbsp;Try using synonyms. Maybe what you're looking for uses slightly different words.<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.&nbsp;&nbsp;Make your search more general. Try more general terms in place of specific ones.<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.&nbsp;&nbsp;Try your search in a different scope. Different scopes can have different results.";
        //Form Messages
        public const string DELETE_CONFIRM = "Are you sure you want to delete this item? ";
        //Contact and User Picker Messages
        public const string EMPTY_PICKER_SEARCH_STRING = "<br>    Please enter a search string <br><br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1.&nbsp;&nbsp;Search Options: Display Name, LastName, First Name or Office<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.&nbsp;&nbsp;* to find all active contacts.";
        public const string SEARCHED_NO_PICKER_RESULTS = "<br>    No results matching your search were found.<br><br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1.&nbsp;&nbsp;Check your spelling. Are the words in your query spelled correctly?<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.&nbsp;&nbsp;Try searching on Display Name, Last Name, First Name or Office.<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.&nbsp;&nbsp;Make your search more general. Try more general terms in place of specific ones.<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.&nbsp;&nbsp;Try your search in a different scope. * will return all active contacts.";
        //WebPart Messages
        public const string PROPRIETARY_WEBPART = "Webpart cannot be rendered on this site.<br>This is a proprietary webpart designed for use on an SOF Request site.";
        //Request Form
        public const string SUBMIT_CONFIRM = "I understand that upon clicking the OK button will submit this Gateway Access Request and no further changes can be made to this Request.";

        public enum Code {
            SecGrpMembershipReq = 101,
            AdminAccessReq = 102,
            MngrAccessReq = 103,
            MemberAccessReq = 104,
            PersonAdminAccessReq = 105,
            PersonViewAccessReq = 106,
            ActiveAccountReq = 107,
            AppConigError = 201,
            Error = 202
        }

        #region _Private Variables_

        private int _ID;
        private int _Number;
        private string _Message;
        private string _ErrorMessage;

        #endregion

        #region _Public Properties_

        public int ID {
            get { return _ID; }
        }
        public int Number {
            get { return _Number; }
            set { _Number = value; }
        }
        public string Text {
            get { return _Message; }
            set { _Message = value; }
        }
        public string ErrorMessage {
            get { return _ErrorMessage; }
        }

        #endregion

        #region _Constructors_

        public Message() {
            _ID = 0;
            _Number = 0;
            _Message = string.Empty;

            _ErrorMessage = string.Empty;
        }
        public Message(int ID)
            : this() {
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                string sql = "SELECT * FROM dbo.Messages WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", ID);
                SqlDataReader sdr;
                try {
                    conn.Open();
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read()) {
                        _ID = int.Parse(sdr["ID"].ToString());
                        _Number = int.Parse(sdr["Number"].ToString());
                        _Message = sdr["Message"].ToString();

                        _ErrorMessage = string.Empty;
                    }
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

        public static string GetAppMessage(string Code) {
            string setting = string.Empty;
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();
                try {
                    string sql = @"SELECT Message FROM dbo.Messages WHERE Number = @Number";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Number", Code);
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    SqlDataReader sdr;
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read()) {
                        setting = sdr["Message"].ToString();
                    }
                } catch (Exception ex) {
                    Error.WriteError(ex);
                } finally {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
            }
            return setting;
        }
        public bool Insert() {
            bool Successful = false;
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();

                try {
                    DateTime timeStamp = DateTime.UtcNow;
                    string sql = @"Insert INTO dbo.Messages 
										(Number
										,Message)
							Values
										(@Number
										,@Message)
							SELECT @ID = SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Number", _Number);
                    cmd.Parameters.AddWithValue("@Message", _Message);
                    SqlParameter prmID = cmd.Parameters.Add("@ID", SqlDbType.Int);
                    prmID.Direction = ParameterDirection.Output;
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    cmd.ExecuteNonQuery();
                    _ID = int.Parse(prmID.Value.ToString());
                    Successful = true;
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
                    string sql = @"Update dbo.Messages 
										SET Number = @Number
										,Message = @Message
							WHERE ID = @ID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Number", _Number);
                    cmd.Parameters.AddWithValue("@Message", _Message);
                    cmd.Parameters.AddWithValue("@ID", _ID);
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    cmd.ExecuteNonQuery();
                    Successful = true;
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
                    string sql = "DELETE FROM dbo.Messages WHERE ID = @ID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID", _ID);
                    if (conn.State != ConnectionState.Open) { conn.Open(); }
                    cmd.ExecuteNonQuery();
                    Successful = true;
                } catch (Exception ex) {
                    Error.WriteError(ex);
                    _ErrorMessage = ex.Message;
                } finally {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
            }
            return Successful;
        }
        public static DataSet AppMessagesDS() {
            DataSet ds = new DataSet();
            using (new Impersonator()) {
                SqlConnection conn = DataSource.Conn();

                try {
                    string sql = "SELECT * FROM dbo.Messages ";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                } catch (Exception ex) {
                    Error.WriteError(ex);
                } finally {
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                }
            }
            return ds;
        }

        #endregion



    }
}
