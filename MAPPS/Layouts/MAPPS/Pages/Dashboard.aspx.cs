using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

namespace MAPPS.Pages {
    public partial class Dashboard : PageBase {

        protected const string PAGE_TITLE = "Dashboard";
        protected const string PAGE_DESCRIPTION = "Vital information about you and your assigned personnel";
        public const string PAGE_URL = BASE_PAGES_URL + "Dashboard.aspx";

        #region _Methods_

        protected override void Page_Load(object sender, EventArgs e) {
            try {
                base.Page_Load(sender, e);

                if (!IsPostBack) {
                    PersonnelGridSortExpression = "LastName";
                    Fill();
                    FillTravelRequests();
                    FillLeaveRequests();
                }


            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }
        protected override void AddRibbonTab() {
            try {
                base.AddRibbonTab();
                Ribbon.CommandUIVisible = false;
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }
        protected override void Fill() {
            try {
                //gvPersonnel.EmptyDataText = Message.EMPTY_LIST;
                //DataView dv = IsHRAdmin ? new DataView(MAPPS.User.Subordinates(0)) : new DataView(MAPPS.User.Subordinates(CurrentUser.ID));
                //dv.Sort = (PersonnelGridSortDirection == SortDirection.Ascending) ? PersonnelGridSortExpression + " ASC" : PersonnelGridSortExpression + " DESC";

                //gvPersonnel.PageSize = PersonnelGridViewPageSize;
                //gvPersonnel.PageIndex = PersonnelPageIndex;
                //PersonnelItemCount = dv.Table.Rows.Count;

                //if (SetupPersonnelPager())
                //    tablePersonnelPager.Visible = true;
                //else
                //    tablePersonnelPager.Visible = false;

                //gvPersonnel.DataSource = dv;
                //gvPersonnel.DataBind();
                //lblErrorMessage.Text = CurrentUser.ID.ToString();
                //lblErrorMessage.Visible = true;
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }
        protected void FillTravelRequests() {
            try {
                //gvTravel.EmptyDataText = Message.EMPTY_LIST;
                //DataView dv = IsHRAdmin ? new DataView(TravelRequest.Items(true).Tables[0]) : new DataView(TravelRequest.Items(CurrentUser.ID, true).Tables[0]);
                //dv.Sort = (TravelGridSortDirection == SortDirection.Ascending) ? TravelGridSortExpression + " ASC" : TravelGridSortExpression + " DESC";

                //gvTravel.PageSize = TravelGridViewPageSize;
                //gvTravel.PageIndex = TravelPageIndex;
                //TravelItemCount = dv.Table.Rows.Count;

                //if (SetupTravelPager())
                //    tableTravelPager.Visible = true;
                //else
                //    tableTravelPager.Visible = false;

                //gvTravel.DataSource = dv;
                //gvTravel.DataBind();
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }
        protected void FillLeaveRequests() {
            try {
                //gvLeave.EmptyDataText = Message.EMPTY_LIST;
                //DataView dv = IsHRAdmin ? new DataView(LeaveRequest.Items(true).Tables[0]) : new DataView(LeaveRequest.Items(CurrentUser.ID, true).Tables[0]);
                //dv.Sort = (LeaveGridSortDirection == SortDirection.Ascending) ? LeaveGridSortExpression + " ASC" : LeaveGridSortExpression + " DESC";

                //gvLeave.PageSize = LeaveGridViewPageSize;
                //gvLeave.PageIndex = LeavePageIndex;
                //LeaveItemCount = dv.Table.Rows.Count;

                //if (SetupLeavePager())
                //    tableLeavePager.Visible = true;
                //else
                //    tableLeavePager.Visible = false;

                //gvLeave.DataSource = dv;
                //gvLeave.DataBind();
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }

        }

        #endregion _Methods_

        #region _Personnel Grid Methods_
        protected int PersonnelItemCount {
            get {
                if (ViewState["PersonnelItemCount"] == null)
                    ViewState["PersonnelItemCount"] = 0;
                return int.Parse(ViewState["PersonnelItemCount"].ToString());
            }
            set {
                ViewState["PersonnelItemCount"] = value;
            }
        }
        protected void gvPersonnel_RowCommand(object sender, GridViewCommandEventArgs e) {
            if (e.CommandName != "Sort" && e.CommandName != "Page") {
                try {
                    if (e.CommandName == "ViewItem") {
                        ItemID = int.Parse(e.CommandArgument.ToString());
                        //Response.Redirect(string.Format("{0}/{1}?View=View&ID={2}", SPContext.Current.Web.Url, UserItem.PAGE_URL, ItemID), false);
                    }
                    if (e.CommandName == "EditItem") {
                        ItemID = int.Parse(e.CommandArgument.ToString());
                        //Response.Redirect(string.Format("{0}/{1}?View=Edit&ID={2}", SPContext.Current.Web.Url, UserItem.PAGE_URL, ItemID), false);
                    }
                } catch (Exception ex) {
                    MAPPS.Error.WriteError(ex);
                    if (ShowDebug)
                        lblErrorMessage.Text = ex.ToString();
                }
            }
        }
        private bool SetupPersonnelPager() {
            return SetupPersonnelPager(lblPersonnelPagingItemsRange, lblPersonnelPagingItemsTotal, ibtnPersonnelPagingFirst, ibtnPersonnelPagingPrevious, ddlPersonnelPagingPages, ibtnPersonnelPagingNext, ibtnPersonnelPagingLast, ddlPersonnelItemsPerPage);
        }
        protected SortDirection PersonnelGridSortDirection {
            get {
                if (ViewState["PersonnelGridSortDirection"] == null) {
                    ViewState["PersonnelGridSortDirection"] = SortDirection.Ascending;
                    return SortDirection.Ascending;
                } else {
                    return (SortDirection)ViewState["PersonnelGridSortDirection"];
                }
            }
            set {
                ViewState["PersonnelGridSortDirection"] = value;
            }
        }
        protected string PersonnelGridSortExpression {
            get {
                if (ViewState["PersonnelGridSortExpression"] == null)
                    ViewState["PersonnelGridSortExpression"] = "ID";
                return ViewState["PersonnelGridSortExpression"].ToString();
            }
            set {
                ViewState["PersonnelGridSortExpression"] = value.Trim();
            }
        }
        protected int PersonnelGridViewPageSize {
            get {
                if (ViewState["PersonnelGridViewPageSize"] == null)
                    ViewState["PersonnelGridViewPageSize"] = Common.GridViewPageSize();
                return int.Parse(ViewState["PersonnelGridViewPageSize"].ToString());
            }
            set {
                ViewState["PersonnelGridViewPageSize"] = value;
            }
        }
        protected int PersonnelPageIndex {
            get {
                if (ViewState["PersonnelPageIndex"] == null)
                    ViewState["PersonnelPageIndex"] = 0;
                return int.Parse(ViewState["PersonnelPageIndex"].ToString());
            }
            set {
                ViewState["PersonnelPageIndex"] = value;
            }
        }
        protected int PersonnelNumberOfPages {
            get {
                if (ViewState["PersonnelNumberOfPages"] == null)
                    ViewState["PersonnelNumberOfPages"] = 0;
                return int.Parse(ViewState["PersonnelNumberOfPages"].ToString());
            }
            set {
                ViewState["PersonnelNumberOfPages"] = value;
            }
        }
        protected SortDirection SwitchPersonnelSort() {
            PersonnelGridSortDirection = (PersonnelGridSortDirection == SortDirection.Ascending) ? SortDirection.Descending : SortDirection.Ascending;
            return PersonnelGridSortDirection;
        }
        protected void SetupPersonnelSort(GridView gv, GridViewRowEventArgs e) {
            try {
                foreach (DataControlField field in gv.Columns) {
                    TableCell td = e.Row.Cells[gv.Columns.IndexOf(field)];
                    Label lblSpace = new Label();
                    lblSpace.Text = "&nbsp;";
                    //Add tooltips to headers 
                    if (!string.IsNullOrEmpty(field.SortExpression)) {
                        td.ToolTip = "Sort by " + field.HeaderText;
                    }

                    //Override width attibute from style class
                    td.Style.Add(HtmlTextWriterStyle.Width, "Auto");

                    if (Page.IsPostBack) {
                        //Add appropriate sort image to sorted column
                        if (field.SortExpression == PersonnelGridSortExpression) {
                            Image sortImage = new Image();
                            if (PersonnelGridSortDirection == SortDirection.Descending) {
                                sortImage.ImageUrl = "/_layouts/15/Images/nga/rsort.gif";
                                sortImage.ToolTip = "Sort Descending";
                            } else {
                                sortImage.ImageUrl = "/_layouts/15/Images/nga/sort.gif";
                                sortImage.ToolTip = "Sort Ascending";
                            }
                            td.Controls.Add(lblSpace);
                            td.Controls.Add(sortImage);
                        }
                    }
                }
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
        }
        protected void gvPersonnel_Sorting(object sender, GridViewSortEventArgs e) {
            if (PersonnelGridSortExpression != e.SortExpression)
                PersonnelGridSortDirection = SortDirection.Ascending;
            else
                PersonnelGridSortDirection = SwitchSort();
            PersonnelGridSortExpression = e.SortExpression;
            Fill();
        }
        protected void gvPersonnel_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            try {
                PersonnelPageIndex = e.NewPageIndex;
                Fill();
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
        }
        protected void gvPersonnel_RowCreated(object sender, GridViewRowEventArgs e) {
            try {
                if (e.Row.RowType == DataControlRowType.Pager)
                    e.Row.Visible = false;
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
        }
        protected bool SetupPersonnelPager(Label lblPersonnelPagingItemsRange, Label lblPersonnelPagingItemsTotal, ImageButton ibtnPersonnelPagingFirst, ImageButton ibtnPersonnelPagingPrevious, DropDownList ddlPersonnelPagingPages, ImageButton ibtnPersonnelPagingNext, ImageButton ibtnPersonnelPagingLast, DropDownList ddlPersonnelItemsPerPage) {
            bool b = false;
            try {
                if (PersonnelItemCount > 0) {
                    if (PersonnelItemCount > PersonnelGridViewPageSize)
                        PersonnelNumberOfPages = (int)Math.Ceiling((double)PersonnelItemCount / (double)PersonnelGridViewPageSize);
                    else
                        PersonnelNumberOfPages = 1;

                    int floor = (PersonnelPageIndex * PersonnelGridViewPageSize) + 1;
                    int ceil = (PersonnelPageIndex * PersonnelGridViewPageSize) + PersonnelGridViewPageSize;
                    int ceilOrItems = (ceil > PersonnelItemCount) ? PersonnelItemCount : ceil;

                    int pageShowLimitStart = 1;
                    int pageShowLimitEnd = PersonnelNumberOfPages;

                    ddlPersonnelPagingPages.Items.Clear();
                    for (int i = pageShowLimitStart; i <= pageShowLimitEnd; i++) {
                        if (i <= PersonnelNumberOfPages) {
                            ListItem li = new ListItem(string.Format("{0}", i), (i - 1).ToString());
                            li.Selected = (PersonnelPageIndex + 1 == i);
                            ddlPersonnelPagingPages.Items.Add(li);
                        }
                    }
                    ddlPersonnelPagingPages.Enabled = (ddlPersonnelPagingPages.Items.Count > 1);

                    lblPersonnelPagingItemsRange.Text = string.Format("{0}-{1}", floor, ceilOrItems);
                    lblPersonnelPagingItemsTotal.Text = PersonnelItemCount.ToString();
                    ibtnPersonnelPagingFirst.Enabled = (PersonnelPageIndex != 0);
                    ibtnPersonnelPagingFirst.ImageUrl = (ibtnPersonnelPagingFirst.Enabled) ? "/_layouts/15/images/mewa_leftPage.gif" : "/_layouts/15/images/mewa_leftPageG.gif";
                    ibtnPersonnelPagingFirst.CommandArgument = "0";
                    ibtnPersonnelPagingPrevious.Enabled = (PersonnelPageIndex != 0);
                    ibtnPersonnelPagingPrevious.ImageUrl = (ibtnPersonnelPagingPrevious.Enabled) ? "/_layouts/15/images/mewa_left.gif" : "/_layouts/15/images/mewa_leftG.gif";
                    ibtnPersonnelPagingPrevious.CommandArgument = (PersonnelPageIndex - 1).ToString();
                    ibtnPersonnelPagingNext.Enabled = (PersonnelPageIndex + 1 != PersonnelNumberOfPages);
                    ibtnPersonnelPagingNext.ImageUrl = (ibtnPersonnelPagingNext.Enabled) ? "/_layouts/15/images/mewa_right.gif" : "/_layouts/15/images/mewa_rightG.gif";
                    ibtnPersonnelPagingNext.CommandArgument = (PersonnelPageIndex + 1).ToString();
                    ibtnPersonnelPagingLast.Enabled = (PersonnelPageIndex + 1 != PersonnelNumberOfPages);
                    ibtnPersonnelPagingLast.ImageUrl = (ibtnPersonnelPagingLast.Enabled) ? "/_layouts/15/images/mewa_rightPage.gif" : "/_layouts/15/images/mewa_rightPageG.gif";
                    ibtnPersonnelPagingLast.CommandArgument = (PersonnelNumberOfPages - 1).ToString();

                    int[] PersonnelItemsPerPage = { PersonnelGridViewPageSize, 25, 50, 100 };
                    PersonnelItemsPerPage = PersonnelItemsPerPage.Where(i => i <= ItemCount).Distinct().OrderBy(i => i).ToArray();
                    ddlPersonnelItemsPerPage.Items.Clear();
                    foreach (int i in PersonnelItemsPerPage) {
                        ListItem li = new ListItem(i.ToString());
                        li.Selected = (i == PersonnelGridViewPageSize);
                        ddlPersonnelItemsPerPage.Items.Add(li);
                    }
                    if (ddlPersonnelItemsPerPage.Items.Count == 0) {
                        ddlPersonnelItemsPerPage.Items.Add(new ListItem(PersonnelGridViewPageSize.ToString()));
                        ddlPersonnelItemsPerPage.Enabled = false;
                    } else {
                        ddlPersonnelItemsPerPage.Enabled = true;
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
        protected void ibtnPersonnelPager_Click(object sender, EventArgs e) {
            try {
                ImageButton ibtn = (ImageButton)sender;
                PersonnelPageIndexChange(int.Parse(ibtn.CommandArgument));
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
        }
        protected void ddlPersonnelPagingPages_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                DropDownList ddl = (DropDownList)sender;
                PersonnelPageIndexChange(int.Parse(ddl.SelectedValue));
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
        }
        protected void ddlPersonnelItemsPerPage_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                DropDownList ddl = (DropDownList)sender;
                PersonnelGridViewPageSize = int.Parse(ddl.SelectedValue);
                PersonnelPageIndexChange(0);
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
        }
        protected void PersonnelPageIndexChange(int Index) {
            PersonnelPageIndex = Index;
            Fill();
        }

        #endregion

        #region _Leave Grid Methods_
        protected int LeaveItemCount {
            get {
                if (ViewState["LeaveItemCount"] == null)
                    ViewState["LeaveItemCount"] = 0;
                return int.Parse(ViewState["LeaveItemCount"].ToString());
            }
            set {
                ViewState["LeaveItemCount"] = value;
            }
        }
        protected void gvLeave_RowCommand(object sender, GridViewCommandEventArgs e) {
            if (e.CommandName != "Sort" && e.CommandName != "Page") {
                try {
                    if (e.CommandName == "ViewItem") {
                        ItemID = int.Parse(e.CommandArgument.ToString());
                        //Response.Redirect(string.Format("{0}/{1}?View=View&ID={2}", SPContext.Current.Web.Url, UserItem.PAGE_URL, ItemID), false);
                    }
                    if (e.CommandName == "EditItem") {
                        ItemID = int.Parse(e.CommandArgument.ToString());
                        //Response.Redirect(string.Format("{0}/{1}?View=Edit&ID={2}", SPContext.Current.Web.Url, UserItem.PAGE_URL, ItemID), false);
                    }
                } catch (Exception ex) {
                    MAPPS.Error.WriteError(ex);
                    if (ShowDebug)
                        lblErrorMessage.Text = ex.ToString();
                }
            }
        }
        private bool SetupLeavePager() {
            return SetupLeavePager(lblLeavePagingItemsRange, lblLeavePagingItemsTotal, ibtnLeavePagingFirst, ibtnLeavePagingPrevious, ddlLeavePagingPages, ibtnLeavePagingNext, ibtnLeavePagingLast, ddlLeaveItemsPerPage);
        }
        protected SortDirection LeaveGridSortDirection {
            get {
                if (ViewState["LeaveGridSortDirection"] == null) {
                    ViewState["LeaveGridSortDirection"] = SortDirection.Ascending;
                    return SortDirection.Ascending;
                } else {
                    return (SortDirection)ViewState["LeaveGridSortDirection"];
                }
            }
            set {
                ViewState["LeaveGridSortDirection"] = value;
            }
        }
        protected string LeaveGridSortExpression {
            get {
                if (ViewState["LeaveGridSortExpression"] == null)
                    ViewState["LeaveGridSortExpression"] = "ID";
                return ViewState["LeaveGridSortExpression"].ToString();
            }
            set {
                ViewState["LeaveGridSortExpression"] = value.Trim();
            }
        }
        protected int LeaveGridViewPageSize {
            get {
                if (ViewState["LeaveGridViewPageSize"] == null)
                    ViewState["LeaveGridViewPageSize"] = Common.GridViewPageSize();
                return int.Parse(ViewState["LeaveGridViewPageSize"].ToString());
            }
            set {
                ViewState["LeaveGridViewPageSize"] = value;
            }
        }
        protected int LeavePageIndex {
            get {
                if (ViewState["LeavePageIndex"] == null)
                    ViewState["LeavePageIndex"] = 0;
                return int.Parse(ViewState["LeavePageIndex"].ToString());
            }
            set {
                ViewState["LeavePageIndex"] = value;
            }
        }
        protected int LeaveNumberOfPages {
            get {
                if (ViewState["LeaveNumberOfPages"] == null)
                    ViewState["LeaveNumberOfPages"] = 0;
                return int.Parse(ViewState["LeaveNumberOfPages"].ToString());
            }
            set {
                ViewState["LeaveNumberOfPages"] = value;
            }
        }
        protected SortDirection SwitchLeaveSort() {
            LeaveGridSortDirection = (LeaveGridSortDirection == SortDirection.Ascending) ? SortDirection.Descending : SortDirection.Ascending;
            return LeaveGridSortDirection;
        }
        protected void SetupLeaveSort(GridView gv, GridViewRowEventArgs e) {
            try {
                foreach (DataControlField field in gv.Columns) {
                    TableCell td = e.Row.Cells[gv.Columns.IndexOf(field)];
                    Label lblSpace = new Label();
                    lblSpace.Text = "&nbsp;";
                    //Add tooltips to headers 
                    if (!string.IsNullOrEmpty(field.SortExpression)) {
                        td.ToolTip = "Sort by " + field.HeaderText;
                    }

                    //Override width attibute from style class
                    td.Style.Add(HtmlTextWriterStyle.Width, "Auto");

                    if (Page.IsPostBack) {
                        //Add appropriate sort image to sorted column
                        if (field.SortExpression == LeaveGridSortExpression) {
                            Image sortImage = new Image();
                            if (LeaveGridSortDirection == SortDirection.Descending) {
                                sortImage.ImageUrl = "/_layouts/15/Images/nga/rsort.gif";
                                sortImage.ToolTip = "Sort Descending";
                            } else {
                                sortImage.ImageUrl = "/_layouts/15/Images/nga/sort.gif";
                                sortImage.ToolTip = "Sort Ascending";
                            }
                            td.Controls.Add(lblSpace);
                            td.Controls.Add(sortImage);
                        }
                    }
                }
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
        }
        protected void gvLeave_Sorting(object sender, GridViewSortEventArgs e) {
            if (LeaveGridSortExpression != e.SortExpression)
                LeaveGridSortDirection = SortDirection.Ascending;
            else
                LeaveGridSortDirection = SwitchSort();
            LeaveGridSortExpression = e.SortExpression;
            Fill();
        }
        protected void gvLeave_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            try {
                LeavePageIndex = e.NewPageIndex;
                Fill();
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
        }
        protected void gvLeave_RowCreated(object sender, GridViewRowEventArgs e) {
            try {
                if (e.Row.RowType == DataControlRowType.Pager)
                    e.Row.Visible = false;
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
        }
        protected bool SetupLeavePager(Label lblLeavePagingItemsRange, Label lblLeavePagingItemsTotal, ImageButton ibtnLeavePagingFirst, ImageButton ibtnLeavePagingPrevious, DropDownList ddlLeavePagingPages, ImageButton ibtnLeavePagingNext, ImageButton ibtnLeavePagingLast, DropDownList ddlLeaveItemsPerPage) {
            bool b = false;
            try {
                if (LeaveItemCount > 0) {
                    if (LeaveItemCount > LeaveGridViewPageSize)
                        LeaveNumberOfPages = (int)Math.Ceiling((double)LeaveItemCount / (double)LeaveGridViewPageSize);
                    else
                        LeaveNumberOfPages = 1;

                    int floor = (LeavePageIndex * LeaveGridViewPageSize) + 1;
                    int ceil = (LeavePageIndex * LeaveGridViewPageSize) + LeaveGridViewPageSize;
                    int ceilOrItems = (ceil > LeaveItemCount) ? LeaveItemCount : ceil;

                    int pageShowLimitStart = 1;
                    int pageShowLimitEnd = LeaveNumberOfPages;

                    ddlLeavePagingPages.Items.Clear();
                    for (int i = pageShowLimitStart; i <= pageShowLimitEnd; i++) {
                        if (i <= LeaveNumberOfPages) {
                            ListItem li = new ListItem(string.Format("{0}", i), (i - 1).ToString());
                            li.Selected = (LeavePageIndex + 1 == i);
                            ddlLeavePagingPages.Items.Add(li);
                        }
                    }
                    ddlLeavePagingPages.Enabled = (ddlLeavePagingPages.Items.Count > 1);

                    lblLeavePagingItemsRange.Text = string.Format("{0}-{1}", floor, ceilOrItems);
                    lblLeavePagingItemsTotal.Text = LeaveItemCount.ToString();
                    ibtnLeavePagingFirst.Enabled = (LeavePageIndex != 0);
                    ibtnLeavePagingFirst.ImageUrl = (ibtnLeavePagingFirst.Enabled) ? "/_layouts/15/images/mewa_leftPage.gif" : "/_layouts/15/images/mewa_leftPageG.gif";
                    ibtnLeavePagingFirst.CommandArgument = "0";
                    ibtnLeavePagingPrevious.Enabled = (LeavePageIndex != 0);
                    ibtnLeavePagingPrevious.ImageUrl = (ibtnLeavePagingPrevious.Enabled) ? "/_layouts/15/images/mewa_left.gif" : "/_layouts/15/images/mewa_leftG.gif";
                    ibtnLeavePagingPrevious.CommandArgument = (LeavePageIndex - 1).ToString();
                    ibtnLeavePagingNext.Enabled = (LeavePageIndex + 1 != LeaveNumberOfPages);
                    ibtnLeavePagingNext.ImageUrl = (ibtnLeavePagingNext.Enabled) ? "/_layouts/15/images/mewa_right.gif" : "/_layouts/15/images/mewa_rightG.gif";
                    ibtnLeavePagingNext.CommandArgument = (LeavePageIndex + 1).ToString();
                    ibtnLeavePagingLast.Enabled = (LeavePageIndex + 1 != LeaveNumberOfPages);
                    ibtnLeavePagingLast.ImageUrl = (ibtnLeavePagingLast.Enabled) ? "/_layouts/15/images/mewa_rightPage.gif" : "/_layouts/15/images/mewa_rightPageG.gif";
                    ibtnLeavePagingLast.CommandArgument = (LeaveNumberOfPages - 1).ToString();

                    int[] LeaveItemsPerPage = { LeaveGridViewPageSize, 25, 50, 100 };
                    LeaveItemsPerPage = LeaveItemsPerPage.Where(i => i <= ItemCount).Distinct().OrderBy(i => i).ToArray();
                    ddlLeaveItemsPerPage.Items.Clear();
                    foreach (int i in LeaveItemsPerPage) {
                        ListItem li = new ListItem(i.ToString());
                        li.Selected = (i == LeaveGridViewPageSize);
                        ddlLeaveItemsPerPage.Items.Add(li);
                    }
                    if (ddlLeaveItemsPerPage.Items.Count == 0) {
                        ddlLeaveItemsPerPage.Items.Add(new ListItem(LeaveGridViewPageSize.ToString()));
                        ddlLeaveItemsPerPage.Enabled = false;
                    } else {
                        ddlLeaveItemsPerPage.Enabled = true;
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
        protected void ibtnLeavePager_Click(object sender, EventArgs e) {
            try {
                ImageButton ibtn = (ImageButton)sender;
                LeavePageIndexChange(int.Parse(ibtn.CommandArgument));
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
        }
        protected void ddlLeavePagingPages_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                DropDownList ddl = (DropDownList)sender;
                LeavePageIndexChange(int.Parse(ddl.SelectedValue));
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
        }
        protected void ddlLeaveItemsPerPage_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                DropDownList ddl = (DropDownList)sender;
                LeaveGridViewPageSize = int.Parse(ddl.SelectedValue);
                LeavePageIndexChange(0);
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
        }
        protected void LeavePageIndexChange(int Index) {
            LeavePageIndex = Index;
            Fill();
        }

        #endregion

        #region _Travel Grid Methods_
        protected int TravelItemCount {
            get {
                if (ViewState["TravelItemCount"] == null)
                    ViewState["TravelItemCount"] = 0;
                return int.Parse(ViewState["TravelItemCount"].ToString());
            }
            set {
                ViewState["TravelItemCount"] = value;
            }
        }
        protected void gvTravel_RowCommand(object sender, GridViewCommandEventArgs e) {
            if (e.CommandName != "Sort" && e.CommandName != "Page") {
                try {
                    if (e.CommandName == "ViewItem") {
                        ItemID = int.Parse(e.CommandArgument.ToString());
                        //Response.Redirect(string.Format("{0}/{1}?View=View&ID={2}", SPContext.Current.Web.Url, UserItem.PAGE_URL, ItemID), false);
                    }
                    if (e.CommandName == "EditItem") {
                        ItemID = int.Parse(e.CommandArgument.ToString());
                        //Response.Redirect(string.Format("{0}/{1}?View=Edit&ID={2}", SPContext.Current.Web.Url, UserItem.PAGE_URL, ItemID), false);
                    }
                } catch (Exception ex) {
                    MAPPS.Error.WriteError(ex);
                    if (ShowDebug)
                        lblErrorMessage.Text = ex.ToString();
                }
            }
        }
        private bool SetupTravelPager() {
            return SetupTravelPager(lblTravelPagingItemsRange, lblTravelPagingItemsTotal, ibtnTravelPagingFirst, ibtnTravelPagingPrevious, ddlTravelPagingPages, ibtnTravelPagingNext, ibtnTravelPagingLast, ddlTravelItemsPerPage);
        }
        protected SortDirection TravelGridSortDirection {
            get {
                if (ViewState["TravelGridSortDirection"] == null) {
                    ViewState["TravelGridSortDirection"] = SortDirection.Ascending;
                    return SortDirection.Ascending;
                } else {
                    return (SortDirection)ViewState["TravelGridSortDirection"];
                }
            }
            set {
                ViewState["TravelGridSortDirection"] = value;
            }
        }
        protected string TravelGridSortExpression {
            get {
                if (ViewState["TravelGridSortExpression"] == null)
                    ViewState["TravelGridSortExpression"] = "ID";
                return ViewState["TravelGridSortExpression"].ToString();
            }
            set {
                ViewState["TravelGridSortExpression"] = value.Trim();
            }
        }
        protected int TravelGridViewPageSize {
            get {
                if (ViewState["TravelGridViewPageSize"] == null)
                    ViewState["TravelGridViewPageSize"] = Common.GridViewPageSize();
                return int.Parse(ViewState["TravelGridViewPageSize"].ToString());
            }
            set {
                ViewState["TravelGridViewPageSize"] = value;
            }
        }
        protected int TravelPageIndex {
            get {
                if (ViewState["TravelPageIndex"] == null)
                    ViewState["TravelPageIndex"] = 0;
                return int.Parse(ViewState["TravelPageIndex"].ToString());
            }
            set {
                ViewState["TravelPageIndex"] = value;
            }
        }
        protected int TravelNumberOfPages {
            get {
                if (ViewState["TravelNumberOfPages"] == null)
                    ViewState["TravelNumberOfPages"] = 0;
                return int.Parse(ViewState["TravelNumberOfPages"].ToString());
            }
            set {
                ViewState["TravelNumberOfPages"] = value;
            }
        }
        protected SortDirection SwitchTravelSort() {
            TravelGridSortDirection = (TravelGridSortDirection == SortDirection.Ascending) ? SortDirection.Descending : SortDirection.Ascending;
            return TravelGridSortDirection;
        }
        protected void SetupTravelSort(GridView gv, GridViewRowEventArgs e) {
            try {
                foreach (DataControlField field in gv.Columns) {
                    TableCell td = e.Row.Cells[gv.Columns.IndexOf(field)];
                    Label lblSpace = new Label();
                    lblSpace.Text = "&nbsp;";
                    //Add tooltips to headers 
                    if (!string.IsNullOrEmpty(field.SortExpression)) {
                        td.ToolTip = "Sort by " + field.HeaderText;
                    }

                    //Override width attibute from style class
                    td.Style.Add(HtmlTextWriterStyle.Width, "Auto");

                    if (Page.IsPostBack) {
                        //Add appropriate sort image to sorted column
                        if (field.SortExpression == TravelGridSortExpression) {
                            Image sortImage = new Image();
                            if (TravelGridSortDirection == SortDirection.Descending) {
                                sortImage.ImageUrl = "/_layouts/15/Images/nga/rsort.gif";
                                sortImage.ToolTip = "Sort Descending";
                            } else {
                                sortImage.ImageUrl = "/_layouts/15/Images/nga/sort.gif";
                                sortImage.ToolTip = "Sort Ascending";
                            }
                            td.Controls.Add(lblSpace);
                            td.Controls.Add(sortImage);
                        }
                    }
                }
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
        }
        protected void gvTravel_Sorting(object sender, GridViewSortEventArgs e) {
            if (TravelGridSortExpression != e.SortExpression)
                TravelGridSortDirection = SortDirection.Ascending;
            else
                TravelGridSortDirection = SwitchSort();
            TravelGridSortExpression = e.SortExpression;
            Fill();
        }
        protected void gvTravel_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            try {
                TravelPageIndex = e.NewPageIndex;
                Fill();
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
        }
        protected void gvTravel_RowCreated(object sender, GridViewRowEventArgs e) {
            try {
                if (e.Row.RowType == DataControlRowType.Pager)
                    e.Row.Visible = false;
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
        }
        protected bool SetupTravelPager(Label lblTravelPagingItemsRange, Label lblTravelPagingItemsTotal, ImageButton ibtnTravelPagingFirst, ImageButton ibtnTravelPagingPrevious, DropDownList ddlTravelPagingPages, ImageButton ibtnTravelPagingNext, ImageButton ibtnTravelPagingLast, DropDownList ddlTravelItemsPerPage) {
            bool b = false;
            try {
                if (TravelItemCount > 0) {
                    if (TravelItemCount > TravelGridViewPageSize)
                        TravelNumberOfPages = (int)Math.Ceiling((double)TravelItemCount / (double)TravelGridViewPageSize);
                    else
                        TravelNumberOfPages = 1;

                    int floor = (TravelPageIndex * TravelGridViewPageSize) + 1;
                    int ceil = (TravelPageIndex * TravelGridViewPageSize) + TravelGridViewPageSize;
                    int ceilOrItems = (ceil > TravelItemCount) ? TravelItemCount : ceil;

                    int pageShowLimitStart = 1;
                    int pageShowLimitEnd = TravelNumberOfPages;

                    ddlTravelPagingPages.Items.Clear();
                    for (int i = pageShowLimitStart; i <= pageShowLimitEnd; i++) {
                        if (i <= TravelNumberOfPages) {
                            ListItem li = new ListItem(string.Format("{0}", i), (i - 1).ToString());
                            li.Selected = (TravelPageIndex + 1 == i);
                            ddlTravelPagingPages.Items.Add(li);
                        }
                    }
                    ddlTravelPagingPages.Enabled = (ddlTravelPagingPages.Items.Count > 1);

                    lblTravelPagingItemsRange.Text = string.Format("{0}-{1}", floor, ceilOrItems);
                    lblTravelPagingItemsTotal.Text = TravelItemCount.ToString();
                    ibtnTravelPagingFirst.Enabled = (TravelPageIndex != 0);
                    ibtnTravelPagingFirst.ImageUrl = (ibtnTravelPagingFirst.Enabled) ? "/_layouts/15/images/mewa_leftPage.gif" : "/_layouts/15/images/mewa_leftPageG.gif";
                    ibtnTravelPagingFirst.CommandArgument = "0";
                    ibtnTravelPagingPrevious.Enabled = (TravelPageIndex != 0);
                    ibtnTravelPagingPrevious.ImageUrl = (ibtnTravelPagingPrevious.Enabled) ? "/_layouts/15/images/mewa_left.gif" : "/_layouts/15/images/mewa_leftG.gif";
                    ibtnTravelPagingPrevious.CommandArgument = (TravelPageIndex - 1).ToString();
                    ibtnTravelPagingNext.Enabled = (TravelPageIndex + 1 != TravelNumberOfPages);
                    ibtnTravelPagingNext.ImageUrl = (ibtnTravelPagingNext.Enabled) ? "/_layouts/15/images/mewa_right.gif" : "/_layouts/15/images/mewa_rightG.gif";
                    ibtnTravelPagingNext.CommandArgument = (TravelPageIndex + 1).ToString();
                    ibtnTravelPagingLast.Enabled = (TravelPageIndex + 1 != TravelNumberOfPages);
                    ibtnTravelPagingLast.ImageUrl = (ibtnTravelPagingLast.Enabled) ? "/_layouts/15/images/mewa_rightPage.gif" : "/_layouts/15/images/mewa_rightPageG.gif";
                    ibtnTravelPagingLast.CommandArgument = (TravelNumberOfPages - 1).ToString();

                    int[] TravelItemsPerPage = { TravelGridViewPageSize, 25, 50, 100 };
                    TravelItemsPerPage = TravelItemsPerPage.Where(i => i <= ItemCount).Distinct().OrderBy(i => i).ToArray();
                    ddlTravelItemsPerPage.Items.Clear();
                    foreach (int i in TravelItemsPerPage) {
                        ListItem li = new ListItem(i.ToString());
                        li.Selected = (i == TravelGridViewPageSize);
                        ddlTravelItemsPerPage.Items.Add(li);
                    }
                    if (ddlTravelItemsPerPage.Items.Count == 0) {
                        ddlTravelItemsPerPage.Items.Add(new ListItem(TravelGridViewPageSize.ToString()));
                        ddlTravelItemsPerPage.Enabled = false;
                    } else {
                        ddlTravelItemsPerPage.Enabled = true;
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
        protected void ibtnTravelPager_Click(object sender, EventArgs e) {
            try {
                ImageButton ibtn = (ImageButton)sender;
                TravelPageIndexChange(int.Parse(ibtn.CommandArgument));
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
        }
        protected void ddlTravelPagingPages_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                DropDownList ddl = (DropDownList)sender;
                TravelPageIndexChange(int.Parse(ddl.SelectedValue));
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
        }
        protected void ddlTravelItemsPerPage_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                DropDownList ddl = (DropDownList)sender;
                TravelGridViewPageSize = int.Parse(ddl.SelectedValue);
                TravelPageIndexChange(0);
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
        }
        protected void TravelPageIndexChange(int Index) {
            TravelPageIndex = Index;
            Fill();
        }

        #endregion



    }
}
