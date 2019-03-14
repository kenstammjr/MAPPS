using System;
using System.Data;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using System.Collections.Generic;
using System.Drawing;

namespace MAPPS.Pages {
    public partial class ServerFunctions : PageBase {
        protected const string PAGE_TITLE = "Server Functions";
        protected const string PAGE_DESCRIPTION = "View and manage server functions";
        public const string PAGE_URL = BASE_PAGES_URL + "ServerFunctions.aspx";

        private string jsActionNewItem;

        #region _Methods_

        protected override void Page_Load(object sender, EventArgs e) {
            try {
                base.Page_Load(sender, e);
                SetupContribute();
                if (!IsPostBack) {
                    GridSortExpression = "Name";
                    Fill();
                }
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }

        protected override void AddRibbonTab() {
            try {
                RibbonItem("Ribbon.MAPPS.New.NewItem.Click", jsActionNewItem, IsAdmin);
                base.AddRibbonTab();
                Ribbon.Minimized = true;
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }

        protected override void AddTabEvents() {
            try {
                RibbonItem("Ribbon.MAPPS.New.NewItem.Click", jsActionNewItem, IsAdmin);
                base.AddTabEvents();
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }

        private void SetupContribute() {
            if (IsManager) {
                jsActionNewItem = string.Format("window.location.replace('{0}/{1}?View=New&ID=0'); return false;", SPContext.Current.Web.Url, ServerFunctionItem.PAGE_URL);
                lbtnNew.Visible = true;
                gvData.Columns[0].Visible = true;
            } else {
                Response.Redirect(string.Format("{0}/{1}?code={2}", SPContext.Current.Web.Url, Message.URL_USERMESSAGE, Message.Code.MemberAccessReq), false);
            }
        }

        //This is the GridView of all the items in the Table
        protected override void Fill() {
            try {
                gvData.EmptyDataText = Message.EMPTY_LIST;
                DataView dv = new DataView(MAPPS.ServerFunction.Items().Tables[0]);
                dv.Sort = (GridSortDirection == SortDirection.Ascending) ? GridSortExpression + " ASC" : GridSortExpression + " DESC";

                gvData.PageSize = GridViewPageSize;
                gvData.PageIndex = PageIndex;
                ItemCount = dv.Table.Rows.Count;

                if (SetupPager())
                    tablePager.Visible = true;
                else
                    tablePager.Visible = false;

                gvData.DataSource = dv;
                gvData.DataBind();

            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }

        private bool SetupPager() {
            return SetupPager(lblPagingItemsRange, lblPagingItemsTotal, ibtnPagingFirst, ibtnPagingPrevious, ddlPagingPages, ibtnPagingNext, ibtnPagingLast, ddlItemsPerPage);
        }

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e) {
            try {
                switch (e.Row.RowType) {
                    case DataControlRowType.DataRow:
                        //Label lblItemID = (Label)e.Row.FindControl("lblItemID");
                        //int id = int.Parse(lblItemID.Text);
                        //Label lblActive = (Label)e.Row.FindControl("lblActive");
                        //lblActive.Text = lblActive.Text == "True" ? "<img src=\"/_layouts/15/images/kpidefault-0.gif\" />" : "<img src=\"/_layouts/15/images/kpidefault-2.gif\" />";
                        break;
                    case DataControlRowType.Header:
                        SetupSort(gvData, e);
                        break;
                }
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }
        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e) {
            if (e.CommandName != "Sort" && e.CommandName != "Page") {
                try {
                    if (e.CommandName == "ViewItem") {
                        ItemID = int.Parse(e.CommandArgument.ToString());
                        Response.Redirect(string.Format("{0}/{1}?View=View&ID={2}", SPContext.Current.Web.Url, ServerFunctionItem.PAGE_URL, ItemID), false);
                    }
                    if (e.CommandName == "EditItem") {
                        ItemID = int.Parse(e.CommandArgument.ToString());
                        Response.Redirect(string.Format("{0}/{1}?View=Edit&ID={2}", SPContext.Current.Web.Url, ServerFunctionItem.PAGE_URL, ItemID), false);
                    }
                } catch (Exception ex) {
                    MAPPS.Error.WriteError(ex);
                    if (ShowDebug)
                        lblErrorMessage.Text = ex.ToString();
                }
            }
        }

        #endregion _Methods_

        #region _Events_

        protected void lbtnNew_Click(object sender, EventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?View=New&id={2}", SPContext.Current.Web.Url, Pages.ServerFunctionItem.PAGE_URL, 0), false);
        }

        #endregion


    }
}