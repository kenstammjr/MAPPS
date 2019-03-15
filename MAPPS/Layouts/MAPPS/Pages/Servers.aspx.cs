using System;
using System.Data;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using System.Collections.Generic;
using System.Drawing;

namespace MAPPS.Pages {
    public partial class Servers : PageBase {
        protected const string PAGE_TITLE = "Servers";
        protected const string PAGE_DESCRIPTION = "View and manage servers";
        public const string PAGE_URL = BASE_PAGES_URL + "Servers.aspx";

        private string jsActionNewItem;

        #region _Methods_

        protected override void Page_Load(object sender, EventArgs e) {
            try {
                base.Page_Load(sender, e);
                ReadParameters();
                SetupContribute();
                if (!IsPostBack) {
                    if (Filter != string.Empty)
                        txtSearch.Text = Filter;
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
                jsActionNewItem = string.Format("window.location.replace('{0}/{1}?View=New&ID=0'); return false;", SPContext.Current.Web.Url, ServerItem.PAGE_URL);
                lbtnNew.Visible = true;
                gvData.Columns[0].Visible = true;
            } else {
                Response.Redirect(string.Format("{0}/{1}?code={2}", SPContext.Current.Web.Url, Message.URL_USERMESSAGE, Message.Code.MemberAccessReq), false);
            }
        }

        //This is the GridView of all the items in the Table
        protected override void Fill() {
            try {
                DataView dv = new DataView();
                if (txtSearch.Text == string.Empty) {
                    gvData.EmptyDataText = Message.EMPTY_LIST;
                    dv = new DataView(MAPPS.Server.Items().Tables[0]);
                } else {
                    gvData.EmptyDataText = Message.EMPTY_LIST_SEARCHED;
                    dv = new DataView(MAPPS.Server.Items(txtSearch.Text.Trim()).Tables[0]);
                }
                dv.Sort = (GridSortDirection == SortDirection.Ascending) ? GridSortExpression + " ASC" : GridSortExpression + " DESC";

                gvData.PageSize = GridViewPageSize;
                gvData.PageIndex = PageIndex;
                ItemCount = dv.Table.Rows.Count;

                if (SetupPager()) {
                    tablePager.Visible = true;
                    string maxRecords = MAPPS.Configuration.AppSetting("MaxRecords");
                    if (ItemCount == int.Parse(maxRecords)) {
                        lblPagingItemsTotal.Text = string.Format("<font color=red>{0}</font>", lblPagingItemsTotal.Text);
                        lblPagingItemsTotal.ToolTip = string.Format("Maximum record threshold of {0} encounterd! The value can be adjusted by an application administrator.", maxRecords);
                    }
                } else
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
                        Label lblItemID = (Label)e.Row.FindControl("lblItemID");
                        int id = int.Parse(lblItemID.Text);
                        ImageButton ibtnEdit = (ImageButton)e.Row.FindControl("ibtnEdit");
                        LinkButton lbtnName = (LinkButton)e.Row.FindControl("lbtnName");
                        Label lblTypeName = (Label)e.Row.FindControl("lblTypeName");
                        Label lblFunctionName = (Label)e.Row.FindControl("lblFunctionName");
                        Label lblDescription = (Label)e.Row.FindControl("lblDescription");
                        Label lblVersionName = (Label)e.Row.FindControl("lblVersionName");
                        Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                        Label lblPrimaryPOC = (Label)e.Row.FindControl("lblPrimaryPOC");
                        Label lblAlternatePOC = (Label)e.Row.FindControl("lblAlternatePOC");
                        Label lblPurpose = (Label)e.Row.FindControl("lblPurpose");
                        Label lblIPAddress = (Label)e.Row.FindControl("lblIPAddress");

                        int rowID = int.Parse(lbtnName.CommandArgument.ToString());

                        string urlView = string.Format("{0}/{1}?View=View&ID={2}&ServerID={2}&Filter={3}", this.Web.Url, Pages.ServerItem.PAGE_URL, id, txtSearch.Text.Trim());
                        string urlEdit = string.Format("{0}/{1}?View=Edit&ID={2}&ServerID={2}&Filter={3}", this.Web.Url, Pages.ServerItem.PAGE_URL, id, txtSearch.Text.Trim());

                        ibtnEdit.OnClientClick = "openModalDialog('Server - " + string.Format("{0}", lbtnName.Text) + "', '" + urlEdit + "'); return false;";
                        lbtnName.OnClientClick = "openModalDialog('Server - " + string.Format("{0}", lbtnName.Text) + "', '" + urlView + "'); return false;";


                        if (lblStatus.Text.ToUpper().Contains("OFF")) {
                            lblStatus.ForeColor = Color.Red;
                            lblStatus.BackColor = Color.Yellow;
                        }
                        if (txtSearch.Text.Length > 0) {
                            Filter = txtSearch.Text.Trim();
                            lbtnName.Text = Common.HighLightText(lbtnName.Text, Filter);
                            lblTypeName.Text = Common.HighLightText(lblTypeName.Text, Filter);
                            lblFunctionName.Text = Common.HighLightText(lblFunctionName.Text, Filter);
                            lblVersionName.Text = Common.HighLightText(lblVersionName.Text, Filter);
                            lblDescription.Text = Common.HighLightText(lblDescription.Text, Filter);
                            lblPrimaryPOC.Text = Common.HighLightText(lblPrimaryPOC.Text, Filter);
                            lblAlternatePOC.Text = Common.HighLightText(lblAlternatePOC.Text, Filter);
                            lblPurpose.Text = Common.HighLightText(lblPurpose.Text, Filter);
                            lblIPAddress.Text = Common.HighLightText(lblIPAddress.Text, Filter);
                            lblStatus.Text = Common.HighLightText(lblStatus.Text, Filter);
                        }
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
                        Response.Redirect(string.Format("{0}/{1}?View=View&ID={2}&Filter={3}", SPContext.Current.Web.Url, ServerItem.PAGE_URL, ItemID, Filter), false);
                    }
                    if (e.CommandName == "EditItem") {
                        ItemID = int.Parse(e.CommandArgument.ToString());
                        Response.Redirect(string.Format("{0}/{1}?View=Edit&ID={2}&Filter={3}", SPContext.Current.Web.Url, ServerItem.PAGE_URL, ItemID, Filter), false);
                    }
                } catch (Exception ex) {
                    MAPPS.Error.WriteError(ex);
                    if (ShowDebug)
                        lblErrorMessage.Text = ex.ToString();
                }
            }
        }
        protected override void PageIndexChange(int Index) {
            string searchString = string.Empty;
            if (txtSearch.Text.Length > 0 && txtSearch.Text != "Search...")
                searchString = txtSearch.Text;
            PageIndex = Index;
            string url = QueryStringParameter(CurrentURL, Request.QueryString, new string[] { "ID", "PageIndex", "Filter" }, new string[] { ItemID.ToString(), PageIndex.ToString(), searchString });
            Response.Redirect(url, false);
        }
        #endregion _Methods_

        #region _Events_

        protected void lbtnNew_Click(object sender, EventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?View=New&id={2}&Filter={3}", SPContext.Current.Web.Url, Pages.ServerItem.PAGE_URL, 0, Filter), false);
        }
        protected void txtSearch_TextChanged(object sender, EventArgs e) {
            Filter = txtSearch.Text.Trim();
            PageIndex = 0;
            Fill();
        }
        #endregion


    }
}