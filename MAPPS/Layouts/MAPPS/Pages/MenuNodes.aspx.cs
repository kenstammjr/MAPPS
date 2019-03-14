using System;
using System.Data;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using System.Collections.Generic;

namespace MAPPS.Pages {
    public partial class MenuNodes : PageBase {
        protected const string PAGE_TITLE = "Menu Nodes";
        protected const string PAGE_DESCRIPTION = "View and manage menu nodes";
        public const string PAGE_URL = BASE_PAGES_URL + "MenuNodes.aspx";

        private string jsActionNewItem;

        #region _Methods_

        protected override void Page_Load(object sender, EventArgs e) {
            try {
                base.Page_Load(sender, e);

                SetupContribute();

                if (!IsPostBack) {
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
            if (IsAdmin) {
                jsActionNewItem = string.Format("window.location.replace('{0}/{1}?View=New&ID=0'); return false;", SPContext.Current.Web.Url, OrganizationItem.PAGE_URL);
                lbtnNew.Visible = true;
                gvData.Columns[0].Visible = true;
            } else {
                Response.Redirect(string.Format("{0}/{1}?code={2}", SPContext.Current.Web.Url, Message.URL_USERMESSAGE, Message.Code.AdminAccessReq), false);
            }
        }
        protected void BuildBreadcrumb() {
            try {
                string bc = "<SPAN class=menu-item-text><b>Parent Organizations</b></SPAN>";
                if (ItemID > 0) {
                    bc = string.Format("<SPAN class=menu-item-text><a  href='{0}'>Parent Organizations</a></SPAN>", "Organizations.aspx");
                    DataSet ds = Organization.Parents(ItemID);
                    foreach (DataRow dr in ds.Tables[0].Rows) {
                        bc = bc + string.Format("{3}<SPAN class=menu-item-text><a href='{0}?ID={1}'>{2}</a></SPAN>", "Organizations.aspx", dr["ID"].ToString(), dr["Name"].ToString(), GetBreadCrumbSpacer());
                    }
                    bc = bc + GetBreadCrumbSpacer() + "<SPAN><b>" + new Organization(ItemID).Name + "</b></SPAN>";
                }
                lblBreadCrumb.Text = bc;
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }

        protected override void Fill() {
            try {
                BuildBreadcrumb();
                DataView dv = new DataView();
                if (txtSearch.Text == string.Empty) {
                    gvData.EmptyDataText = Message.EMPTY_LIST;
                    dv = new DataView(MAPPS.Organization.Items(ItemID).Tables[0]);
                } else {
                    gvData.EmptyDataText = Message.EMPTY_LIST_SEARCHED;
                    //dv = new DataView(MAPPS.Organization.Items(txtSearch.Text.Trim()).Tables[0]);
                    dv = new DataView(MAPPS.Organization.Items(ItemID).Tables[0]);
                }
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
                        Response.Redirect(string.Format("{0}/{1}?View=View&ID={2}", SPContext.Current.Web.Url, OrganizationItem.PAGE_URL, ItemID), false);
                    }
                    if (e.CommandName == "EditItem") {
                        ItemID = int.Parse(e.CommandArgument.ToString());
                        Response.Redirect(string.Format("{0}/{1}?View=Edit&ID={2}", SPContext.Current.Web.Url, OrganizationItem.PAGE_URL, ItemID), false);
                    }
                    if (e.CommandName == "GetChildren") {
                        ItemID = int.Parse(e.CommandArgument.ToString());
                        Fill();
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
            Response.Redirect(string.Format("{0}/{1}?View=New&id={2}", SPContext.Current.Web.Url, Pages.OrganizationItem.PAGE_URL, 0), false);
        }
        protected void txtSearch_TextChanged(object sender, EventArgs e) {
            PageIndex = 0;
            Fill();
        }

        #endregion

    }
}