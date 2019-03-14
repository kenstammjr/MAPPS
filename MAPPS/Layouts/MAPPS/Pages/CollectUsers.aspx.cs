﻿using System;
using System.Data;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using System.Collections.Generic;

namespace MAPPS.Pages {
    public partial class CollectUsers : PageBase {
        protected const string PAGE_TITLE = "Activity";
        protected const string PAGE_DESCRIPTION = "View application activity";
        public const string PAGE_URL = BASE_PAGES_URL + "CollectUsers.aspx";

        #region _Methods_

        protected override void Page_Load(object sender, EventArgs e) {
            try {
                base.Page_Load(sender, e);

                UserControlBase ucb = (LeftNavBar)LoadControl("~/_ControlTemplates/15/MAPPS/LeftNavBar.ascx");
                ucb.Tab = UserControlBase.Tabs.Administration;
                PlaceHolderLeftNavBar.Controls.Add(ucb);

                SetupContribute();

                if (!IsPostBack) {
                    GridSortExpression = "CreatedOn";
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
            if (!IsAdmin) {
                 Response.Redirect(string.Format("{0}/{1}?code={2}", SPContext.Current.Web.Url, Message.URL_USERMESSAGE, Message.Code.SecGrpMembershipReq), false);
            }
        }
        protected override void Fill() {
            try {
                gvData.EmptyDataText = "No activity has been recored in the application.";
                DataView dv = new DataView(Action.Items().Tables[0]);
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
                Action.Write(string.Format("Application activity reviewed"), CurrentUserWithDomain);
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
        }

        #endregion _Methods_

        #region _Events_

        #endregion


    }
}