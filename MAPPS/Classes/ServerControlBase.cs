using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace MAPPS{
    public abstract class ServerControlBase : UserControl {

        #region _Constants_

        protected const string RIBBON_POSTBACK_SAVE_EVENT = "RibbonSaveItemClick";
        protected const string RIBBON_POSTBACK_DELETE_EVENT = "RibbonDeleteItemClick";
        protected const string RIBBON_POSTBACK_EDIT_EVENT = "RibbonEditItemClick";
        protected const string RIBBON_POSTBACK_VIEW_EVENT = "RibbonViewItemClick";

        protected string jsActionView = PostBackJavaScript(RIBBON_POSTBACK_VIEW_EVENT);
        protected string jsActionSave = PostBackJavaScript(RIBBON_POSTBACK_SAVE_EVENT);
        protected string jsActionEdit = PostBackJavaScript(RIBBON_POSTBACK_EDIT_EVENT);
        protected string jsActionDelete = string.Format("if (confirm('Are you sure you want to delete this item?') == true) __doPostBack('{0}', ''); return false;", RIBBON_POSTBACK_DELETE_EVENT);

        // these should be overridden in your specific application
        public const string RIBBON_INITIAL_TAB_ID = "Ribbon.MAPPS";
        public const string RIBBON_XML_FILE_LOCATION = "MAPPS.Layouts.MAPPS.Ribbon";

        #endregion

        #region _Protected Properties from PageBases_

        protected SortDirection GridSortDirection {
            get {
                if (ViewState["GridSortDirection"] == null) {
                    ViewState["GridSortDirection"] = SortDirection.Ascending;
                    return SortDirection.Ascending;
                } else {
                    return (SortDirection)ViewState["GridSortDirection"];
                }
            }
            set {
                ViewState["GridSortDirection"] = value;
            }
        }
        protected string GridSortExpression {
            get {
                if (ViewState["GridSortExpression"] == null)
                    ViewState["GridSortExpression"] = "ID";
                return ViewState["GridSortExpression"].ToString();
            }
            set {
                ViewState["GridSortExpression"] = value.Trim();
            }
        }
        protected int GridViewPageSize {
            get {
                if (ViewState["GridViewPageSize"] == null)
                    ViewState["GridViewPageSize"] = Common.GridViewPageSize();
                return int.Parse(ViewState["GridViewPageSize"].ToString());
            }
            set {
                ViewState["GridViewPageSize"] = value;
            }
        }
        protected int PageIndex {
            get {
                if (ViewState["CurrentPageIndex"] == null)
                    ViewState["CurrentPageIndex"] = 0;
                return int.Parse(ViewState["CurrentPageIndex"].ToString());
            }
            set {
                ViewState["CurrentPageIndex"] = value;
            }
        }
        protected int NumberOfPages {
            get {
                if (ViewState["NumberOfPages"] == null)
                    ViewState["NumberOfPages"] = 0;
                return int.Parse(ViewState["NumberOfPages"].ToString());
            }
            set {
                ViewState["NumberOfPages"] = value;
            }
        }
        protected bool ShowDebug {
            get {
                if (ViewState["ShowDebug"] == null) {
                    ViewState["ShowDebug"] = Setting.KeyValue("DirectoryShowDebug");
                    ViewState["ShowDebug"] = ((string)ViewState["ShowDebug"] == string.Empty) ? false : ViewState["ShowDebug"];
                }
                return bool.Parse(ViewState["ShowDebug"].ToString());
            }
        }
        protected int ItemID {
            get {
                if (ViewState["ItemID"] == null)
                    ViewState["ItemID"] = 0;
                return int.Parse(ViewState["ItemID"].ToString());
            }
            set {
                ViewState["ItemID"] = value;
            }
        }
        protected int ParentID {
            get {
                if (ViewState["ParentID"] == null)
                    ViewState["ParentID"] = 0;
                return int.Parse(ViewState["ParentID"].ToString());
            }
            set {
                ViewState["ParentID"] = value;
            }
        }
        protected int ServerID {
            get {
                if (ViewState["ServerID"] == null)
                    ViewState["ServerID"] = 0;
                return int.Parse(ViewState["ServerID"].ToString());
            }
            set {
                ViewState["ServerID"] = value;
            }
        }
        protected int UserID {
            get {
                if (ViewState["UserID"] == null)
                    ViewState["UserID"] = 0;
                return int.Parse(ViewState["UserID"].ToString());
            }
            set {
                ViewState["UserID"] = value;
            }
        }
        protected int StudentID {
            get {
                if (ViewState["StudentID"] == null)
                    ViewState["StudentID"] = 0;
                return int.Parse(ViewState["StudentID"].ToString());
            }
            set {
                ViewState["StudentID"] = value;
            }
        }
        protected int CourseID {
            get {
                if (ViewState["CourseID"] == null)
                    ViewState["CourseID"] = 0;
                return int.Parse(ViewState["CourseID"].ToString());
            }
            set {
                ViewState["CourseID"] = value;
            }
        }
        protected int IterationID {
            get {
                if (ViewState["IterationID"] == null)
                    ViewState["IterationID"] = 0;
                return int.Parse(ViewState["IterationID"].ToString());
            }
            set {
                ViewState["IterationID"] = value;
            }
        }
        protected string Filter {
            get {
                if (ViewState["Filter"] == null)
                    ViewState["Filter"] = string.Empty;
                return ViewState["Filter"].ToString();
            }
            set {
                ViewState["Filter"] = value;
            }
        }
        protected string ReturnURL {
            get {
                if (ViewState["ReturnURL"] == null)
                    ViewState["ReturnURL"] = string.Empty;
                return ViewState["ReturnURL"].ToString();
            }
            set {
                ViewState["ReturnURL"] = value;
            }
        }
        protected int ItemCount {
            get {
                if (ViewState["ItemCount"] == null)
                    ViewState["ItemCount"] = 0;
                return int.Parse(ViewState["ItemCount"].ToString());
            }
            set {
                ViewState["ItemCount"] = value;
            }
        }
        protected Uri URLReferrer {
            get {
                if (ViewState["URLReferrer"] == null && this.Request.UrlReferrer != null) {
                    ViewState["URLReferrer"] = this.Request.UrlReferrer;
                } else {
                    ViewState["URLReferrer"] = this.Request.Url;
                }
                return new Uri(ViewState["URLReferrer"].ToString());
            }
        }

        protected enum ItemView {
            New,
            View,
            Edit,
            List,
            Extension
        }
        protected ItemView IView {
            get {
                if (ViewState["IView"] == null)
                    ViewState["IView"] = ItemView.List;
                return (ItemView)ViewState["IView"];
            }
            set {
                ViewState["IView"] = value;
            }
        }

        protected MAPPS.User CurrentUser;
        protected string CurrentUserWithDomain {
            get {
                if (ViewState["CurrentUserWithDomain"] == null)
                    ViewState["CurrentUserWithDomain"] = string.Empty;
                return ViewState["CurrentUserWithDomain"].ToString();
            }
            set {
                ViewState["CurrentUserWithDomain"] = value;
            }
        }
        protected bool IsAdmin = false;
        protected bool IsManager = false;
        protected bool IsAnonymous = false;
        protected SPRibbon Ribbon;
        protected List<IRibbonCommand> RibbonCommands = new List<IRibbonCommand>();

        #endregion

        #region _Other Properties_

        public Tabs Tab {
            get {
                if (ViewState["Tab"] == null)
                    ViewState["Tab"] = Tabs.Home;
                return (Tabs)Enum.Parse(typeof(Tabs), ViewState["Tab"].ToString());
            }
            set {
                ViewState["Tab"] = value;
            }
        }
        public enum Tabs {
            Home,
            Security,
            Training,
            Help,
            Administration
        }

        #endregion

        protected virtual void Page_Load(object sender, EventArgs e) {
            try {
                if (Context.User.Identity.Name != null) {
                    CurrentUser = new MAPPS.User(Context.User.Identity.Name);
                    CurrentUserWithDomain = CurrentUser.UserName; // string.Format("{0}\\{1}", CurrentUser.Domain.StripClaim(), CurrentUser.UserName);
                    IsAdmin = CurrentUser.InRole(RoleType.Administrator.ToString());
                    IsManager = CurrentUser.InRole(RoleType.Manager.ToString());
                } else {
                    IsAnonymous = true;
                }

                ReadParameters();

                if (!IsAnonymous) {
                    MAPPS.User user = new MAPPS.User(CurrentUserWithDomain);

                }
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
        }

        protected virtual void ReadParameters() {
            try {
                foreach (string name in Request.QueryString.AllKeys) {
                    string value = Request.QueryString[name].ToString();
                    if (!string.IsNullOrEmpty(value)) {
                        switch (name.ToLower()) {
                            case "id":
                                ItemID = int.Parse(value);
                                break;
                            case "parentid":
                                ParentID = int.Parse(value);
                                break;
                            case "serverid":
                                ServerID = int.Parse(value);
                                break;
                            case "userid":
                                UserID = int.Parse(value);
                                break;
                            case "studentid":
                                StudentID = int.Parse(value);
                                break;
                            case "courseid":
                                CourseID = int.Parse(value);
                                break;
                            case "iterationid":
                                IterationID = int.Parse(value);
                                break;
                            case "view":
                                IView = (ItemView)Enum.Parse(typeof(ItemView), value);
                                break;
                            case "pageindex":
                                PageIndex = int.Parse(value);
                                break;
                            case "filter":
                                Filter = value;
                                break;
                            case "rtn":
                                ReturnURL = value;
                                break;
                        }
                    }
                }
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
        }


        protected abstract void Fill();

        #region _Public Methods_
        public static string PostBackJavaScript(string Event) {
            return string.Format("__doPostBack('{0}', ''); return false;", Event);
        }

        /// <summary>
        /// QueryString helper method that returns a URL with the new value
        /// </summary>
        public static string QueryStringParameter(Uri URL, NameValueCollection QueryString, string Key, string NewValue) {
            return QueryStringParameter(URL, QueryString, new string[] { Key }, new string[] { NewValue });
        }

        /// <summary>
        /// QueryString helper method that gets value of a key
        /// </summary>
        public static string QueryStringParameter(Uri URL, NameValueCollection QueryString, string Key) {
            NameValueCollection qs = HttpUtility.ParseQueryString(QueryString.ToString());
            return qs.Get(Key);
        }

        /// <summary>
        /// QueryString helper method that returns a URL with the new values
        /// </summary>
        public static string QueryStringParameter(Uri URL, NameValueCollection QueryString, string[] Keys, string[] NewValues) {
            string qss = QueryString.ToString();
            NameValueCollection qs = HttpUtility.ParseQueryString(qss);
            for (int i = 0; i < Keys.Length; i++) {
                qs.Set(Keys[i], NewValues[i]);
            }
            return string.Format("{0}?{1}", URL.AbsolutePath, qs);
        }

        #endregion

        #region _Ribbon_

        /// <summary>
        /// Builds and adds the custom ribbon tab
        /// </summary>
        protected virtual void AddRibbonTab() {
            try {
                // Gets the current instance of the ribbon on the page and replaces it with the custom ribbon.
                Ribbon = SPRibbon.GetCurrent(this.Page);
                XmlDocument ribbonExtensions = new XmlDocument();
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(string.Format("{0}.{1}.xml", RIBBON_XML_FILE_LOCATION, "MAPPStab"));
                XmlTextReader mainTab = new XmlTextReader(stream);
                ribbonExtensions.Load(mainTab);
                Ribbon.RegisterDataExtension(ribbonExtensions.FirstChild, "Ribbon.Tabs._children");

                // Adds the custom tab
                stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(string.Format("{0}.{1}.xml", RIBBON_XML_FILE_LOCATION, "MAPPScontextualtab"));
                XmlTextReader contextualTab = new XmlTextReader(stream);
                ribbonExtensions.Load(contextualTab);
                Ribbon.RegisterDataExtension(ribbonExtensions.FirstChild, "Ribbon.Templates._children");

                Ribbon.CommandUIVisible = true;
                if (!Ribbon.IsTabAvailable(RIBBON_INITIAL_TAB_ID))
                    Ribbon.MakeTabAvailable(RIBBON_INITIAL_TAB_ID);
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
        }

        /// <summary>
        /// Builds and adds the custom ribbon menu objects
        /// </summary>
        protected virtual void AddTabEvents() {
            try {
                // Register javascript ribbon commands with 
                // RibbonCommands.Add()
                // before running this method

                // Register initialize function
                var manager = new SPRibbonScriptManager();
                var methodInfo = typeof(SPRibbonScriptManager).GetMethod("RegisterInitializeFunction", BindingFlags.Instance | BindingFlags.NonPublic);
                methodInfo.Invoke(manager, new object[] {
                    Page, "InitPageComponent", "/_layouts/15/mapps/scripts/PageComponent.js", false, "MAPPS.PageComponent.initialize()"
                });

                // Register ribbon scripts
                manager.RegisterGetCommandsFunction(Page, "getGlobalCommands", RibbonCommands);
                manager.RegisterCommandEnabledFunction(Page, "commandEnabled", RibbonCommands);
                manager.RegisterHandleCommandFunction(Page, "handleCommand", RibbonCommands);
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
        }

        /// <summary>
        /// Adds the custom ribbon commands to the items
        /// </summary>
        protected virtual void RibbonItem(string Action, string JavaScriptAction, bool Enabled) {
            RibbonCommands.Add(new SPRibbonCommand(string.Format("{0}", Action), JavaScriptAction, Enabled.ToString().ToLower()));
        }

        /// <summary>
        /// Removes ribbon commands by ID
        /// </summary>
        protected void RibbonTrimIds(List<string> CommandIDs) {
            try {
                foreach (string cmd in CommandIDs)
                    if (!RibbonCommands.Exists(x => x.Id == cmd))
                        Ribbon.TrimById(cmd.Truncate(cmd.Length - 6, false));
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
        }

        #endregion

        #region _Sorting/Paging_
        protected SortDirection SwitchSort() {
            GridSortDirection = (GridSortDirection == SortDirection.Ascending) ? SortDirection.Descending : SortDirection.Ascending;
            return GridSortDirection;
        }
        protected void SetupSort(GridView gv, GridViewRowEventArgs e) {
            try {
                foreach (DataControlField field in gv.Columns) {
                    TableCell td = e.Row.Cells[gv.Columns.IndexOf(field)];

                    //Add tooltips to headers 
                    if (!string.IsNullOrEmpty(field.SortExpression)) {
                        td.ToolTip = "Sort by " + field.HeaderText;
                    }

                    //Override width attibute from style class
                    td.Style.Add(HtmlTextWriterStyle.Width, "Auto");

                    if (Page.IsPostBack) {
                        //Add appropriate sort image to sorted column
                        if (field.SortExpression == GridSortExpression) {
                            Image sortImage = new Image();
                            if (GridSortDirection == SortDirection.Descending) {
                                sortImage.ImageUrl = "/_layouts/Images/mapps/rsort.gif";
                                sortImage.ToolTip = "Sort Descending";
                            } else {
                                sortImage.ImageUrl = "/_layouts/Images/mapps/sort.gif";
                                sortImage.ToolTip = "Sort Ascending";
                            }
                            td.Controls.Add(sortImage);
                        }
                    }
                }
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
        }
        protected void gvData_Sorting(object sender, GridViewSortEventArgs e) {
            if (GridSortExpression != e.SortExpression)
                GridSortDirection = SortDirection.Ascending;
            else
                GridSortDirection = SwitchSort();
            GridSortExpression = e.SortExpression;
            Fill();
        }

        /// <summary>
        /// Handles Paging for Gridview.
        /// </summary>
        protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            try {
                PageIndex = e.NewPageIndex;
                Fill();
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
        }

        /// <summary>
        /// Hides Paging for Gridview.
        /// </summary>
        protected void gvData_RowCreated(object sender, GridViewRowEventArgs e) {
            try {
                if (e.Row.RowType == DataControlRowType.Pager)
                    e.Row.Visible = false;
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
        }

        /// <summary>
        /// Custom pager for grid views
        /// </summary>
        /// <returns>bool for visibility</returns>
        protected bool SetupPager(Label lblPagingItemsRange, Label lblPagingItemsTotal, ImageButton ibtnPagingFirst, ImageButton ibtnPagingPrevious, DropDownList ddlPagingPages, ImageButton ibtnPagingNext, ImageButton ibtnPagingLast, DropDownList ddlItemsPerPage) {
            bool b = false;
            try {
                if (ItemCount > 0) {
                    if (ItemCount > GridViewPageSize)
                        NumberOfPages = (int)Math.Ceiling((double)ItemCount / (double)GridViewPageSize);
                    else
                        NumberOfPages = 1;

                    int floor = (PageIndex * GridViewPageSize) + 1;
                    int ceil = (PageIndex * GridViewPageSize) + GridViewPageSize;
                    int ceilOrItems = (ceil > ItemCount) ? ItemCount : ceil;

                    int pageShowLimitStart = 1;
                    int pageShowLimitEnd = NumberOfPages;

                    ddlPagingPages.Items.Clear();
                    for (int i = pageShowLimitStart; i <= pageShowLimitEnd; i++) {
                        if (i <= NumberOfPages) {
                            ListItem li = new ListItem(string.Format("{0}", i), (i - 1).ToString());
                            li.Selected = (PageIndex + 1 == i);
                            ddlPagingPages.Items.Add(li);
                        }
                    }
                    ddlPagingPages.Enabled = (ddlPagingPages.Items.Count > 1);

                    lblPagingItemsRange.Text = string.Format("{0}-{1}", floor, ceilOrItems);
                    lblPagingItemsTotal.Text = ItemCount.ToString();
                    ibtnPagingFirst.Enabled = (PageIndex != 0);
                    ibtnPagingFirst.ImageUrl = (ibtnPagingFirst.Enabled) ? "/_layouts/images/mewa_leftPage.gif" : "/_layouts/images/mewa_leftPageG.gif";
                    ibtnPagingFirst.CommandArgument = "0";
                    ibtnPagingPrevious.Enabled = (PageIndex != 0);
                    ibtnPagingPrevious.ImageUrl = (ibtnPagingPrevious.Enabled) ? "/_layouts/images/mewa_left.gif" : "/_layouts/images/mewa_leftG.gif";
                    ibtnPagingPrevious.CommandArgument = (PageIndex - 1).ToString();
                    ibtnPagingNext.Enabled = (PageIndex + 1 != NumberOfPages);
                    ibtnPagingNext.ImageUrl = (ibtnPagingNext.Enabled) ? "/_layouts/images/mewa_right.gif" : "/_layouts/images/mewa_rightG.gif";
                    ibtnPagingNext.CommandArgument = (PageIndex + 1).ToString();
                    ibtnPagingLast.Enabled = (PageIndex + 1 != NumberOfPages);
                    ibtnPagingLast.ImageUrl = (ibtnPagingLast.Enabled) ? "/_layouts/images/mewa_rightPage.gif" : "/_layouts/images/mewa_rightPageG.gif";
                    ibtnPagingLast.CommandArgument = (NumberOfPages - 1).ToString();

                    int[] ItemsPerPage = { GridViewPageSize, 10, 25, 50, 100 };
                    ItemsPerPage = ItemsPerPage.Where(i => i <= ItemCount).Distinct().OrderBy(i => i).ToArray();
                    ddlItemsPerPage.Items.Clear();
                    foreach (int i in ItemsPerPage) {
                        ListItem li = new ListItem(i.ToString());
                        li.Selected = (i == GridViewPageSize);
                        ddlItemsPerPage.Items.Add(li);
                    }
                    if (ddlItemsPerPage.Items.Count == 0) {
                        ddlItemsPerPage.Items.Add(new ListItem(GridViewPageSize.ToString()));
                        ddlItemsPerPage.Enabled = false;
                    } else {
                        ddlItemsPerPage.Enabled = true;
                    }

                    b = true;
                } else {
                    b = false;
                }
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
            return b;
        }

        /// <summary>
        /// Paging catch all click event
        /// </summary>
        protected void ibtnPager_Click(object sender, EventArgs e) {
            try {
                ImageButton ibtn = (ImageButton)sender;
                PageIndexChange(int.Parse(ibtn.CommandArgument));
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
        }

        /// <summary>
        /// Paging dropdown change event
        /// </summary>
        protected void ddlPagingPages_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                DropDownList ddl = (DropDownList)sender;
                PageIndexChange(int.Parse(ddl.SelectedValue));
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
        }

        /// <summary>
        /// Paging dropdown items per page event
        /// </summary>
        protected void ddlItemsPerPage_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                DropDownList ddl = (DropDownList)sender;
                GridViewPageSize = int.Parse(ddl.SelectedValue);
                PageIndexChange(0);
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
        }

        /// <summary>
        /// Paging event
        /// </summary>
        protected virtual void PageIndexChange(int Index) {
            PageIndex = Index;
            Fill();
        }

        #endregion


    }
}
