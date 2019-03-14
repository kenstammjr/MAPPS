using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace MAPPS {
    public static class Common {
        public const string URL_ASC_SORT_IMAGE = "/_layouts/15/images/mapps/sort.gif";
        public const string URL_DESC_SORT_IMAGE = "/_layouts/15/images/mapps/rsort.gif";
        public const int TEXT_TRUNCATE_SHORT = 35;
        public const int TEXT_TRUNCATE_LONG = 100;
        private const int GRID_VIEW_PAGESIZE = 50;
        private const int LIST_PAGE_SIZE = 10;
        public const int ACCOUNT_NAME_MAX_LENGTH = 20;
        public const string ACCOUNT_EXTENSION = ".sp";

        public enum PageMode {
            New,
            Edit,
            View,
            List,
            Extension
        }

        public static int GridViewPageSize() {
            return 25;
        }

        /// <summary>
        /// Like the IsNullOrEmpty but it does a Trim.
        /// </summary>
        /// <param name="source">Dont Pass</param>
        /// <returns>True = String is Null or Empty</returns>
        public static bool IsNullOrWhiteSpace(this string source) {
            bool result = false;

            if (source == null || source.Trim().Length < 1)
                result = true;

            return result;
        }

        /// <summary>
        /// Trims the 'i:0#.w|' or '0#.w|' from the Claims string.
        /// </summary>
        /// <param name="claim"></param>
        /// <returns>Stripped string</returns>
        public static string StripClaim(this string claimText) {
            if (string.IsNullOrEmpty(claimText))
                return claimText;

            string strClaim;
            if (claimText.IndexOf("|") != -1) {
                strClaim = claimText.Substring((claimText.IndexOf('|') + 1));
            } else
                strClaim = claimText;
            return strClaim;
        }

        public static DateTime ConvertUTCToWebLocalTime(SPWeb Web, DateTime UTCDateToConvert) {
            return Web.RegionalSettings.TimeZone.UTCToLocalTime(UTCDateToConvert);
        }

        /// <summary>
        /// Builds the settings table.
        /// </summary>
        /// <param name="SettingsTable">The settings table.</param>
        /// <param name="filename">The xml filename.</param>
        public static void BuildSettingsTable(ref Table SettingsTable, string filename) {
            DataSet ds = MenuGroupsDS(filename);
            DataView dv = new DataView(ds.Tables[0]);
            dv.Sort = "Index ASC";

            Int32 GroupCount = 0;
            foreach (DataRow dr in dv.ToTable().Rows) {
                if ((GroupCount % 2) == 0) {
                    TableRow trNextRow = new TableRow();
                    SettingsTable.Rows.Add(trNextRow);
                }

                TableRow tr = SettingsTable.Rows[SettingsTable.Rows.Count - 1];
                TableCell tdGroup = new TableCell();
                tdGroup.VerticalAlign = VerticalAlign.Top;
                tdGroup.Style.Add("padding-bottom", "15px");
                tdGroup.Style.Add("padding-left", "0px");
                tdGroup.Style.Add("padding-right", "0px");
                tdGroup.Style.Add("padding-top", "0px");
                tdGroup.Width = Unit.Percentage(25.0);
                tdGroup.Controls.Add(CreateSettingGroup(dr["ID"].ToString(), dr["Name"].ToString(), dr["Icon"].ToString(), filename));

                TableCell tdSpacer = new TableCell();
                tdSpacer.Width = Unit.Pixel(15);

                tr.Cells.Add(tdGroup);
                tr.Cells.Add(tdSpacer);
                GroupCount++;
            }
        }
        private static DataSet MenuGroupsDS(string filename) {
            DataSet ds = new DataSet();
            string path = string.Empty;
            try {
                path = HttpContext.Current.Request.MapPath(filename);
            } catch { }
            XmlDocument xmlDoc = new XmlDocument();
            bool xmlLoad = false;
            try {
                xmlDoc.Load(path);
                xmlLoad = true;
            } catch { }
            if (xmlLoad) {
                XmlNodeReader reader = new XmlNodeReader(xmlDoc);
                ds.ReadXml(reader);
                reader.Close();
            }
            return ds;
        }


        /// <summary>
        /// creates and formats the tables for each grouping (User Administration, Resource Management, etc)
        /// </summary>
        /// <param name="GroupID"></param>
        /// <param name="GroupName"></param>
        /// <param name="GroupIcon"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        private static Table CreateSettingGroup(string GroupID, string GroupName, string GroupIcon, string filename) {
            Table GroupTable = new Table();
            GroupTable.Width = Unit.Percentage(80.0);
            GroupTable.CellPadding = 0;
            GroupTable.CellSpacing = 0;
            GroupTable.BorderWidth = Unit.Pixel(0);

            TableRow trHeader = new TableRow();
            trHeader.CssClass = "ms-linksection-level2";
            trHeader.BorderWidth = Unit.Pixel(0);

            TableCell tdImage = new TableCell();

            tdImage.Wrap = false;
            tdImage.RowSpan = 2;
            tdImage.VerticalAlign = VerticalAlign.Top;
            tdImage.Style.Add("padding-bottom", "0px");
            tdImage.Style.Add("padding-left", "0px");
            tdImage.Style.Add("padding-right", "0px");
            tdImage.Style.Add("padding-top", "0px");
            Image GroupImage = new Image();

            GroupImage.AlternateText = "";
            GroupImage.ImageUrl = GroupIcon;
            tdImage.Controls.Add(GroupImage);

            TableCell tdHeader = new TableCell();
            tdHeader.CssClass = "ms-linksection-level1";  //ms-linksectionheader
            tdHeader.Style.Add("padding-bottom", "0px");
            tdHeader.Style.Add("padding-left", "0px");
            tdHeader.Style.Add("padding-right", "0px");
            tdHeader.Style.Add("padding-top", "0px");
            tdHeader.Style.Add("font-size", "11pt");
            //tdHeader.Style.Add("font-weight", "normal");
            tdHeader.Style.Add("color", "#288400");
            tdHeader.Wrap = false;
            tdHeader.Width = Unit.Percentage(100.0);
            tdHeader.VerticalAlign = VerticalAlign.Top;
            tdHeader.Text = GroupName;
            trHeader.Cells.Add(tdImage);
            trHeader.Cells.Add(tdHeader);
            GroupTable.Rows.Add(trHeader);
            TableRow trLinks = new TableRow();
            TableCell tdLinks = new TableCell();
            tdLinks.Style.Add("padding-bottom", "0px");
            tdLinks.Style.Add("padding-left", "0px");
            tdLinks.Style.Add("padding-right", "0px");
            tdLinks.Style.Add("padding-top", "0px");
            tdLinks.Style.Add("line-height", "16px");
            tdLinks.Width = Unit.Percentage(100.0);

            tdLinks.Controls.Add(CreateSettingLinks(GroupID, filename));
            trLinks.Cells.Add(tdLinks);
            GroupTable.Rows.Add(trLinks);
            return GroupTable;
        }

        /// <summary>
        /// creates and formats the links under the group headers (User Administration, Resource Management, etc)
        /// </summary>
        /// <param name="GroupID"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        private static Table CreateSettingLinks(string GroupID, string filename) {
            Table GroupLinks = new Table();
            GroupLinks.CellPadding = 0;
            GroupLinks.CellSpacing = 0;
            GroupLinks.BorderWidth = Unit.Pixel(0);

            DataSet ds = MenuGroupsDS(filename);
            DataView dv = new DataView(ds.Tables[1]);
            dv.Sort = "Index ASC";
            dv.RowFilter = "GroupID = '" + GroupID + "'";

            TableRow trOuter = new TableRow();
            TableCell tdOuter = new TableCell();
            tdOuter.CssClass = "ms-propertysheet";
            tdOuter.Style.Add("padding-left", "1px");

            Table InnerTable = new Table();
            InnerTable.CellPadding = 0;
            InnerTable.CellSpacing = 0;
            InnerTable.BorderWidth = Unit.Pixel(0);

            foreach (DataRow dr in dv.ToTable().Rows) {
                TableRow tr = new TableRow();
                //TableCell tdImage = new TableCell();
                //tdImage.Width = Unit.Pixel(8);
                //tdImage.CssClass = "ms-descriptiontext";
                //tdImage.Wrap = false;
                //tdImage.VerticalAlign = VerticalAlign.Top;
                //tdImage.Style.Add("padding-top", "9px");
                //Image Bullet = new Image();
                //Bullet.Height = Unit.Pixel(5);
                //Bullet.Width = Unit.Pixel(5);
                //Bullet.AlternateText = "";
                //Bullet.ImageUrl = "~/_layouts/images/setrect.gif";
                //tdImage.Controls.Add(Bullet);

                TableCell tdLink = new TableCell();
                tdLink.CssClass = "ms-linksection-level1";
                tdLink.VerticalAlign = VerticalAlign.Top;
                tdLink.Style.Add("padding-bottom", "0px");
                tdLink.Style.Add("padding-left", "0px");
                tdLink.Style.Add("padding-right", "0px");
                tdLink.Style.Add("padding-top", "0px");
                LinkButton Link = new LinkButton();
                Link.PostBackUrl = dr["URL"].ToString();
                Link.Text = dr["Name"].ToString();
                Link.Style.Add("text-decoration", "none");
                Link.Style.Add("color", "#0072bc");
                //  Link.Style.Add("hover", "underline");
                tdLink.Controls.Add(Link);

                // tr.Cells.Add(tdImage);
                tr.Cells.Add(tdLink);

                InnerTable.Rows.Add(tr);
            }

            tdOuter.Controls.Add(InnerTable);
            trOuter.Cells.Add(tdOuter);
            GroupLinks.Rows.Add(trOuter);

            return GroupLinks;
        }
        /// <summary>
        /// &nbsp;>&nbsp;
        /// </summary>
        public const string BREAD_CRUMB_SEPARATOR = "&nbsp;>&nbsp;";

        private static ArrayList BreadCrumbLinks(SPWeb current) {
            ArrayList Links = new ArrayList();
            SPWeb web = current;

            try {
                Links.Add(String.Format("<a href=\"{0}\">{1}</a>", current.Url, current.Title));

                while (!web.IsRootWeb) {
                    Links.Add(String.Format("<a href=\"{0}\">{1}</a>", web.Url, web.Title));
                    web = web.ParentWeb;
                }

                Links.Add(String.Format("<a href=\"{0}\">{1}</a>", web.Url, web.Title));
            } catch {
            } finally {
                web.Close();
            }

            return Links;
        }

        public static string BuildBreadCrumb(SPWeb current) {
            StringBuilder BreadCrumb = new StringBuilder();

            try {
                ArrayList Links = BreadCrumbLinks(current);

                for (int i = Links.Count - 1; i > 0; i--) {
                    BreadCrumb.AppendFormat("{0}{1}", Links[i], BREAD_CRUMB_SEPARATOR);
                }
            } catch {
            }

            return BreadCrumb.ToString().Substring(0, BreadCrumb.Length - BREAD_CRUMB_SEPARATOR.Length);
        }
        public static string Encrypt(string PlainText) {
            byte[] key = new byte[] { 18, 52, 53, 124, 33, 36, 77, 48, 29, 50, 111, 112, 213, 14, 135, 116, 167, 198, 109, 200, 211, 29, 33, 35 };
            byte[] iv = new byte[] { 18, 125, 37, 140, 65, 56, 76, 18, 99, 107, 122, 123, 153, 114, 159, 196, 179, 198, 192, 220, 212, 123, 33, 54 };

            TripleDESCryptoServiceProvider cryptoProvider = new TripleDESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(key, iv), CryptoStreamMode.Write);
            StreamWriter sw = new StreamWriter(cs);
            sw.Write(PlainText);
            sw.Flush();
            cs.FlushFinalBlock();
            ms.Flush();
            return System.Convert.ToBase64String(ms.GetBuffer(), 0, int.Parse(ms.Length.ToString()));
        }
        public static string Encrypt(string PlainText, byte[] EncryptKey) {
            byte[] key = EncryptKey;
            byte[] iv = EncryptKey;

            TripleDESCryptoServiceProvider cryptoProvider = new TripleDESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(key, iv), CryptoStreamMode.Write);
            StreamWriter sw = new StreamWriter(cs);
            sw.Write(PlainText);
            sw.Flush();
            cs.FlushFinalBlock();
            ms.Flush();
            return System.Convert.ToBase64String(ms.GetBuffer(), 0, int.Parse(ms.Length.ToString()));
        }
        public static string Decrypt(string EncodedText) {
            byte[] key = new byte[] { 18, 52, 53, 124, 33, 36, 77, 48, 29, 50, 111, 112, 213, 14, 135, 116, 167, 198, 109, 200, 211, 29, 33, 35 };
            byte[] iv = new byte[] { 18, 125, 37, 140, 65, 56, 76, 18, 99, 107, 122, 123, 153, 114, 159, 196, 179, 198, 192, 220, 212, 123, 33, 54 };

            TripleDESCryptoServiceProvider cryptoProvider = new TripleDESCryptoServiceProvider();
            byte[] buffer = System.Convert.FromBase64String(EncodedText);
            MemoryStream ms = new MemoryStream(buffer);
            CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(key, iv), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
        public static string Decrypt(string EncodedText, byte[] EncryptKey) {
            byte[] key = EncryptKey;
            byte[] iv = EncryptKey;

            TripleDESCryptoServiceProvider cryptoProvider = new TripleDESCryptoServiceProvider();
            byte[] buffer = System.Convert.FromBase64String(EncodedText);
            MemoryStream ms = new MemoryStream(buffer);
            CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(key, iv), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
        public static string GenerateSalt() {
            string salt = string.Empty;

            Random r = new Random();
            byte[] key = new byte[24];
            r.NextBytes(key);

            for (int x = 0; x < key.Length; x++) {
                salt += (key[x] + ", ");
            }
            if (salt.Length > 0) {
                salt = salt.Substring(0, salt.Length - 2);
            }
            return salt;
        }
        public static byte[] RestoreKey(string salt) {
            byte[] key = new byte[24];
            string[] nums = salt.Split(',');
            for (int x = 0; x < nums.Length; x++) {
                key[x] = byte.Parse(nums[x]);
            }
            return key;
        }
        /// <summary>
        /// Generates Random Text
        /// </summary>
        /// <param name="size"></param>
        /// <param name="lowerCase">Forces response to be lowercase</param>
        /// <returns></returns>
        public static string GenerateRandomText(int size, bool lowerCase) {
            StringBuilder sb = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++) {
                ch = System.Convert.ToChar(System.Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                sb.Append(ch);
            }
            if (lowerCase)

                return sb.ToString().ToLower();
            return sb.ToString();
        }
        /// <summary>
        ///  Can the object's string be converted to a date
        /// </summary>
        /// <param name="PossibleDate"></param>
        /// <returns></returns>
        public static bool IsDate(Object PossibleDate) {
            string Date = PossibleDate.ToString();
            try {
                DateTime dt = DateTime.Parse(Date);
                if (dt != DateTime.MinValue && dt != DateTime.MaxValue)
                    return true;
                return false;
            } catch {
                return false;
            }
        }
        /// <summary>
        /// Is the string a valid DateTime Date
        /// </summary>
        /// <param name="Date"></param>
        /// <returns></returns>
        public static bool IsDate(string Date) {
            try {
                DateTime dt = DateTime.Parse(Date);
                if (dt != DateTime.MinValue && dt != DateTime.MaxValue)
                    return true;
                return false;
            } catch {
                return false;
            }
        }
        /// <summary>
        /// Does the string consist of only numbers
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static bool IsNumeric(string Value) {
            Regex regex = new Regex(@"^\d+$");
            Match match = regex.Match(Value);
            return match.Success;
        }
        public static string Left(string Source, int Length) {
            //we start at 0 since we want to get the characters starting from the
            //left and with the specified lenght and assign it to a variable
            if (Source.Length < Length) {
                return Source;
            } else {
                return Source.Substring(0, Length);
            }
        }
        /// <summary>
        /// start at the index based on the lenght of the sting minus
        /// the specified lenght and return it
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static string Right(string Source, int Length) {
            if (Source.Length < Length) {
                return string.Empty;
            } else {
                return Source.Substring(Source.Length - Length, Length);
            }
        }
        /// <summary>
        /// start at the specified index in the string ang get N number of
        ///characters depending on the lenght and return it
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="StartIndex"></param>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static string Mid(string Source, int StartIndex, int Length) {
            if (Source.Length < StartIndex) {
                return string.Empty;
            }
            if (Source.Length < (StartIndex + Length)) {
                return string.Empty;
            }
            return Source.Substring(StartIndex, Length);
        }

        public static string TextToJavaScript(string Text) {
            string s = Text;

            s = s.Replace("'", "\\'");

            return s;
        }
        /// <summary>
        /// Excape apostrophes in text with double apostophes
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string TextToSQL(string Text) {
            string s = Text;

            s = s.Replace("'", "''");

            return s;
        }
        /// <summary>
        /// Removes Comments, then Script, then all other tags.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string StripHtml(string text) {
            if (string.IsNullOrEmpty(text.Trim()))
                return text;

            string result = text;
            Regex commentRegex = new Regex(@"<!--.*?-->", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
            Regex scriptRegex = new Regex(@"<script.*?</script>", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
            Regex tagRegex = new Regex(@"<.*?>", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
            result = commentRegex.Replace(result, "");
            result = scriptRegex.Replace(result, "");
            result = tagRegex.Replace(result, "");
            return result;
        }

        public static string Truncate(string Source, int Length) {
            if (Length > 0) {
                if (Source == null || Source.Length < Length || Source.IndexOf(" ", Length) == -1)
                    return Source;
                Length = (Length < 3) ? 3 : Length;
                return Source.Substring(0, Source.IndexOf(" ", (Length - 3))) + "...";
            } else {
                return Source;
            }
        }

        public static string FormatView(this string Source) {
            string _s = Source;
            _s = Regex.Replace(_s, @"(https?|ftp|file)://[-\w\d+&@#/%?=~()|!:,.;]*", delegate(Match m) {
                return string.Format("<a href=\"{0}\">{0}</a>", m.ToString());
            });
            return _s.Replace("\n", "<br />");
        }
        public static string HtmlEncode(this string Source) {
            char[] chars = HttpUtility.HtmlEncode(Source).ToCharArray();
            string _s = string.Empty;

            foreach (char c in chars) {
                int v = Convert.ToInt32(c);
                if (v > 127)
                    _s += string.Format("&#{0};", v);
                else
                    _s += c;
            }

            return _s;
        }
        public static string HtmlDecode(this string Source) {
            return HttpUtility.HtmlDecode(Source);
        }

        /// <summary>
        /// Sorts a DropDownList
        /// </summary>
        public static void SortItems(DropDownList DDL) {
            List<ListItem> l = new List<ListItem>();
            for (int i = 0; i < DDL.Items.Count; i++)
                l.Add(DDL.Items[i]);
            l.Sort(delegate(ListItem x, ListItem y) {
                return x.Text.CompareTo(y.Text);
            });
            DDL.Items.Clear();
            foreach (ListItem li in l)
                DDL.Items.Add(li);
        }

        public static bool FindItemAndSelect(DropDownList DDL, string Type, string Value) {
            ListItem li = null;
            try {
                DDL.SelectedIndex = -1;
                switch (Type.ToLower()) {
                    case "value":
                        li = DDL.Items.FindByValue(Value);
                        if (li != null) {
                            li.Selected = true;
                        }
                        break;
                    case "text":
                        li = DDL.Items.FindByText(Value);
                        if (li != null) {
                            li.Selected = true;
                        }
                        break;
                    case "index":
                        Int32 idxVal = -1;
                        Int32.TryParse(Value, out idxVal);
                        DDL.SelectedIndex = idxVal;
                        break;
                }
            } catch (Exception ex) {
                Error.WriteError(ex);
            }
            return (li != null) ? true : false;
        }
        public static bool FindItemAndSelect(RadioButtonList RBL, string Type, string Value) {
            ListItem li = null;
            try {
                RBL.SelectedIndex = -1;
                switch (Type.ToLower()) {
                    case "value":
                        li = RBL.Items.FindByValue(Value);
                        if (li != null) {
                            li.Selected = true;
                        }
                        break;
                    case "text":
                        li = RBL.Items.FindByText(Value);
                        if (li != null) {
                            li.Selected = true;
                        }
                        break;
                    case "index":
                        Int32 idxVal = -1;
                        Int32.TryParse(Value, out idxVal);
                        RBL.SelectedIndex = idxVal;
                        break;
                }
            } catch (Exception ex) {
                Error.WriteError(ex);
            }
            return (li != null) ? true : false;
        }
        public static bool IsFeatureActive(SPSite Site) {
            bool b = false;
            try {
                foreach (SPFeature f in Site.Features) {
                    if (f.DefinitionId == new Guid("24681492-c11e-41eb-b3ef-6fad7e444ec3")) {
                        b = true;
                        break;
                    }
                }
            } catch (Exception ex) {
                Error.WriteError(ex);
            }
            return b;
        }
        public static string GetResource(string File) {
            string s = string.Empty;
            s = ""; //string.Format("{0}\\TEMPLATE\\LAYOUTS\\PAXTRAX\\XML\\{1}", SPUtility.GetGenericSetupPath(string.Empty), File);
            return s;
        }

        public static string HighLightText(string Text, string Filter) {
            string upperValue = Filter.ToUpper();
            Text = Text.Replace(upperValue, "~|~" + upperValue + "~!~");
            string lowerValue = Filter.ToLower();
            Text = Text.Replace(lowerValue, "~|~" + lowerValue + "~!~");

            if (Filter.Length > 1) {
                try {
                    int minusOne = Filter.Length - 1;
                    int minusTwo = Filter.Length - 2;
                    string first = Filter.Substring(0, 1);
                    string second = Filter.Substring(1, 1);
                    string remainderAfterOne = Filter.Substring(1, minusOne);
                    string remainderAfterTwo = Filter.Substring(2, minusTwo);
                    string mixedValue1 = first.ToUpper() + remainderAfterOne.ToLower();
                    string mixedValue2 = first.ToUpper() + second.ToUpper() + remainderAfterTwo.ToLower();
                    try { Text = Text.Replace(mixedValue1, "~|~" + mixedValue1 + "~!~"); } catch { }
                    try { Text = Text.Replace(mixedValue2, "~|~" + mixedValue2 + "~!~"); } catch { }
                } catch { }
            }
            Text = Text.Replace("~|~", "<span style='color:red; background-color:yellow;'>");
            Text = Text.Replace("~!~", "</span>");
            return Text;
        }

        public static string ConvertTextForJavaScript(string Text) {
            string s = Text;

            s = s.Replace("'", "\\'");

            return s;
        }
        public static void AlertMsg(string AlertMessage, Page WebPageInstance) {
            AlertMessage = ConvertTextForJavaScript(AlertMessage);
            string sb;
            sb = "<script language=javascript>";
            sb += "$(document).ready(function () {";
            sb += "alert";
            sb += "('";
            sb = sb + AlertMessage;
            sb = sb + "');";
            sb = sb + "});";
            sb = sb + "</script>";

            if (!WebPageInstance.ClientScript.IsStartupScriptRegistered("alertMsgHandler"))
                WebPageInstance.ClientScript.RegisterStartupScript(typeof(Page), "alertMsgHandler", sb.ToString());
        }
    }
}
