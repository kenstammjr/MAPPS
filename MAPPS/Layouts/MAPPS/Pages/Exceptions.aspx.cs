using System;
using System.Xml;
using System.IO;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.SharePoint.Administration;

namespace MAPPS.Pages {
    public partial class Exceptions : PageBase {
        protected const string PAGE_TITLE = "Exceptions";
        protected const string PAGE_DESCRIPTION = "View and clear application exceptions";
        public const string PAGE_URL = BASE_PAGES_URL + "Exceptions.aspx";

        #region _Methods_

        protected override void Page_Load(object sender, EventArgs e) {
            try {
                base.Page_Load(sender, e);
                SetupContribute();
                if (!IsPostBack) {
                    lbtnPurge.OnClientClick = "return confirm('Are you sure you want to purge all items from this list?');";
                    GridSortExpression = "DateOccurred";
                    GridSortDirection = SortDirection.Descending;
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
                base.AddRibbonTab();
                Ribbon.CommandUIVisible = false;
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }

        private void SetupContribute() {
            if (IsAdmin) {
                lbtnPurge.Visible = true;
                gvData.Columns[0].Visible = true;
            } else {
                Response.Redirect(string.Format("{0}/{1}?code={2}", SPContext.Current.Web.Url, Message.URL_USERMESSAGE, Message.Code.MemberAccessReq), false);
            }
        }

        protected override void Fill() {
            try {
                gvData.EmptyDataText = (txtSearch.Text.IsNullOrWhiteSpace()) ? Message.EMPTY_LIST_NO_NEW : Message.EMPTY_LIST_SEARCHED;
                DataView dv = new DataView(MAPPS.Error.Items(txtSearch.Text.Trim()).Tables[0]);
                dv.Sort = (GridSortDirection == SortDirection.Ascending) ? GridSortExpression + " ASC" : GridSortExpression + " DESC";
                gvData.PageSize = GridViewPageSize;
                gvData.PageIndex = PageIndex;
                ItemCount = dv.Table.Rows.Count;
                if (SetupPager())
                    tablePager.Visible = true;
                else
                    tablePager.Visible = false;

                if (dv != null) {
                    gvData.DataSource = dv;
                    gvData.DataBind();
                }
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
                        Response.Redirect(string.Format("{0}/{1}?View=View&ID={2}", SPContext.Current.Web.Url, ExceptionItem.PAGE_URL, ItemID), false);
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

        protected void lbtnPurge_Click(object sender, EventArgs e) {
            if (IsAdmin) {
                try {
                    MAPPS.Error.DeleteErrors();
                } catch (Exception ex) {
                    MAPPS.Error.WriteError(ex);
                }
                txtSearch.Text = string.Empty;
                ViewState["PageNumber"] = "0";
                Fill();
            } else {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You must be a member of the Application Administrators security group to purge all records!');", true);
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e) {
            PageIndex = 0;
            Fill();
        }

        #endregion
    }
}