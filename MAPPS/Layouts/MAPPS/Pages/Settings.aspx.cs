using System;
using System.Data;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using System.Collections.Generic;

namespace MAPPS.Pages {
    public partial class Settings : PageBase {
        protected const string PAGE_TITLE = "Settings";
        protected const string PAGE_DESCRIPTION = "View and manage application settings";
        public const string PAGE_URL = BASE_PAGES_URL + "Settings.aspx";

        private string jsActionNewItem;

        #region _Methods_

        protected override void Page_Load(object sender, EventArgs e) {
            try {
                base.Page_Load(sender, e);

                //UserControlBase ucb = (LeftNavBar)LoadControl("~/_ControlTemplates/15/MAPPS/LeftNavBar.ascx");
                //ucb.Tab = UserControlBase.Tabs.Administration;
                //PlaceHolderLeftNavBar.Controls.Add(ucb);

                SetupContribute();

                if (!IsPostBack) {
                    GridSortExpression = "Key";
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
                jsActionNewItem = string.Format("window.location.replace('{0}/{1}?View=New&ID=0'); return false;", SPContext.Current.Web.Url, SettingItem.PAGE_URL);
                lbtnNew.Visible = true;
                gvData.Columns[0].Visible = true;
            } else {
                Response.Redirect(string.Format("{0}/{1}?code={2}", SPContext.Current.Web.Url, Message.URL_USERMESSAGE, Message.Code.SecGrpMembershipReq), false);
            }
        }

        protected override void Fill() {
            try {
                gvData.EmptyDataText = Message.EMPTY_LIST;

                DataView dv = new DataView(Setting.Items().Tables[0]);
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
                        Response.Redirect(string.Format("{0}/{1}?View=View&ID={2}", SPContext.Current.Web.Url, SettingItem.PAGE_URL, ItemID), false);
                    }
                    if (e.CommandName == "EditItem") {
                        ItemID = int.Parse(e.CommandArgument.ToString());
                        Response.Redirect(string.Format("{0}/{1}?View=Edit&ID={2}", SPContext.Current.Web.Url, SettingItem.PAGE_URL, ItemID), false);
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
            Response.Redirect(string.Format("{0}/{1}?View=New&id={2}", SPContext.Current.Web.Url, Pages.SettingItem.PAGE_URL, 0), false);
        }
        #endregion
    }
}