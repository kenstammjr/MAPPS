using System;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace MAPPS
{
    class CommonApp
    {
        public enum PageMode
        {
            New,
            Edit,
            View,
            List,
            Extension
        }

        public const string PAXTRAX_TIMER_JOB_NAME = "MAPPS Notification Job";
        //MAPPS Pages

        //Admin Pages
        public const string URL_USERMESSAGE = "/_layouts/mapps/pages/usermessage.aspx";
        public const string URL_DBUTILITY = "/_layouts/mapps/pages/databaseutility.aspx";
        public const string URL_ADMINISTRATION = "/_layouts/mapps/pages/administration.aspx";

        // SharePoint Images
        public const string URL_ASC_SORT_IMAGE = "/_layouts/15/images/mapps/sort.gif";
        public const string URL_DESC_SORT_IMAGE = "/_layouts/15/images/mapps/rsort.gif";
        public const string URL_PROGRESS_IMAGE = "/_layouts/15/images/mapps/gears_an.gif";
        public const string URL_UNCHECKED_IMAGE = "/_layouts/15/images/mapps/unchecka.gif";
        public const string URL_CHECKED_IMAGE = "/_layouts/15/images/mapps/checkall.gif";

        public const int TEXT_TRUNCATE_SHORT = 35;
        public const int TEXT_TRUNCATE_LONG = 100;

        private const int GRID_VIEW_PAGESIZE = 50;
        private const int LIST_PAGE_SIZE = 10;

        //Constants that are not in our Settings Table
        public const int ACCOUNT_NAME_MAX_LENGTH = 20;
        public const string ACCOUNT_EXTENSION = ".sp";

        private const bool SHOW_DEBUG = false;


        #region Settings Values
        /// <summary>
        /// Returns Grid view page size for webpart or listview. 
        /// Boolean optional parameter that if true returns webpart 
        /// page size.
        /// Checks for application setting DefaultPageSizeForListView or 
        /// DefaultPageSizeForWebPart if value does not exist then a default
        /// page size is used.
        /// </summary>
        public static int GridViewPageSize(bool webpartsize)
        {

            int size;
            size = 0;
            if (webpartsize)
            {
                size = int.Parse(Setting.KeyValue("DefaultPageSizeForWebPart"));
                if (size <= 0)
                {
                    size = LIST_PAGE_SIZE;
                }
            }
            else
            {
                size = int.Parse(Setting.KeyValue("DefaultPageSizeForListView"));
                if (size <= 0)
                {
                    size = GRID_VIEW_PAGESIZE;
                }
            }
            return size;
        }

        /// <summary>
        /// Determine if the Debug Texts should be displayed to the user.
        /// </summary>
        /// <param name="Identity"></param>
        /// <returns></returns>
        public static bool DisplayDebugText(string identityName)
        {
            bool showDebug = false;
            bool.TryParse(Setting.KeyValue("DebugEnabled"), out showDebug);
            return showDebug ? new MAPPS.User(identityName).InRole(RoleType.Administrator.ToString()) : false;
        }

        #endregion

        #region Extensions

        public enum ReferenceTable
        {
            GatewaySlot = 1
        }

        //Insert the item to the dropdown box based on the reference table
        /// <summary>
        /// Insert the item in the dropdown box
        /// </summary>
        /// <param name="referenceTable">Refernece Table to get record</param>
        /// <param name="id">id of record</param>
        /// <param name="position">position in the dropdown list to put the new item</param>
        public static bool InsertItemTopDDL(DropDownList ctrl, ReferenceTable referenceTable, int id)
        {
            int position = 0;
            //Get the entity items record..
            DataTable dt = GetRecordByEntity(referenceTable, id);

            //No row as returned to insert 
            if (dt.Rows.Count == 0)
                return false;

            if (ctrl.Items[0].Text.IsNullOrWhiteSpace())
                position = 1;

            ctrl.Items.Insert(position, new ListItem(dt.Rows[0][1].ToString(), id.ToString()));

            ctrl.SelectedIndex = position;

            return true;
        }

        private static DataTable GetRecordByEntity(ReferenceTable referenceTable, int id)
        {
            DataTable dt = new DataTable();
            switch (referenceTable)
            {
                case ReferenceTable.GatewaySlot:
                    {
                        // dt = GatewaySlot.GetGatewaySlot(id);
                        break;
                    }
            }

            return dt;
        }
        /// <summary>
        /// Validate this is a Valid IP Address format.
        /// </summary>
        /// <param name="ipAddress">IP Address to validate</param>
        /// <returns>True = Valid IP, False = Invalid IP</returns>
        public static bool IsValidIP(string ipAddress)
        {
            if (ipAddress.Trim().Length == 0)
                return false;
            IPAddress ip;
            return IPAddress.TryParse(ipAddress, out ip);
        }
        /// <summary>
        /// Validate a Phone Number
        /// </summary>
        /// <param name="phoneNumber">Phone Number to Validate</param>
        /// <returns>True = Valid Phone Number, False = Invalid Phone Number</returns>
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Trim().Length == 0)
                return false;

            string phonePattern = @"^\(?[0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
            bool valid = false;
            Regex checkPhone = new Regex(phonePattern);
            valid = checkPhone.IsMatch(phoneNumber);
            return valid;
        }
        #endregion
    }
    public static class CommonExtensions
    {
        #region Extensions
        /// <summary>
        /// Get the Enum Description Value if on is set
        /// If not get the string value of the enum
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : source.ToString();
        }

        /// <summary>
        ///  Compare a DateTime object to a string date/Time
        /// </summary>
        /// <param name="source">Dont Pass</param>
        /// <param name="datetime2">string datetime value to compare</param>
        /// <returns>True = same
        ///          False = different
        /// </returns>
        /// <exception cref="ArgumentException">dateTime2 is not a valid DateTime format</exception>
        public static bool Compare(this DateTime source, string dateTime2)
        {
            bool result = false;
            DateTime newDateTime2 = new DateTime();

            //Determine if string passed in is a datetime
            if (dateTime2.IsNullOrWhiteSpace() && !DateTime.TryParse(dateTime2, out newDateTime2))
            {
                throw new ArgumentException("dateTime2: " + dateTime2 + "  passed in is not a valid DateTime format");
            }

            if (DateTime.Compare(source, newDateTime2) == 0)
            {
                result = true;
            }
            return result;
        }


        #region String Extensions

        /// <summary>
        /// UpperCase the First Character of the string.
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static string UpperCaseFirstChar(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return String.Empty;
            }
            return char.ToUpper(source[0]) + source.Substring(1);
        }
        /// <summary>
        /// Truncates specified string to specified length.
        /// This will perform a Trim on the string
        /// Useful for preventing users from over-riding a text field's 'max-length' with copy & paste
        /// </summary>
        /// <param name="length">Max length allowed</param>
        /// <param name="trim">Perform a trim on the string</param>
        /// <returns></returns>
        public static string Truncate(this string source, int length, bool trim)
        {
            string newString = trim ? source.Trim() : source;

            if (!string.IsNullOrEmpty(newString) && newString.Length > length)
                newString = newString.Substring(0, length);
            return newString;
        }

        /// <summary>
        /// Extens the String.Contains Method
        /// </summary>
        /// <example> if(CallingPage.Contains("Administration",StringComparison.CurrentCultureIgnoreCase))</example>
        /// <param name="source">Dont Pass in</param>
        /// <param name="toCheck">String to Compare</param>
        /// /// <param name="compareMethod">Compare Method to use</param>
        /// <returns></returns>
        public static bool Contains(this string source, string toCheck, StringComparison compareMethod)
        {
            return source.IndexOf(toCheck, compareMethod) >= 0;
        }
        #endregion String Extensions

        #endregion Extensions
    }
}

