﻿using System;
using System.Data;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using System.Collections.Generic;

namespace MAPPS.Pages {
    public partial class IssueItem : PageBase {
        protected const string PAGE_TITLE = "Issue Details";
        protected const string PAGE_DESCRIPTION = "View and manage issue details";
        public const string PAGE_URL = BASE_PAGES_URL + "IssueItem.aspx";

        protected string jsActionCancel;
        private string itemRedirect;
        private string newItemRedirect;

        private ProjectIssue item;

        #region _Methods_

        protected override void Page_Load(object sender, EventArgs e) {
            try {
                base.Page_Load(sender, e);

                SetupContribute();
                ReadParameters();
                btnSave.OnClientClick = jsActionSave;
                jsActionCancel = string.Format("rbnCancelClick('{0}/{1}?filter={2}'); return false;", SPContext.Current.Web.Url, Pages.Issues.PAGE_URL, Filter);
                itemRedirect = string.Format("{0}/{1}", SPContext.Current.Web.Url, Pages.Issues.PAGE_URL);
                newItemRedirect = string.Format("{0}/{1}?View=New&id={2}", SPContext.Current.Web.Url, Pages.IssueItem.PAGE_URL, 0);

                btnCancel.OnClientClick = jsActionCancel;
                ibtnRibbonCancel.OnClientClick = jsActionCancel;
                lbtnRibbonCancel.OnClientClick = jsActionCancel;

                if (!IsPostBack) {
                    ibtnRibbonDelete.OnClientClick = "return confirm('Are you sure you want to delete this item?');";
                    lbtnRibbonDelete.OnClientClick = "return confirm('Are you sure you want to delete this item?');";
                    Fill();
                } else {
                    HandlePostBack();
                }
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }
        public virtual string GetItemsURL(bool AsLink) {
            string s = string.Empty;
            try {
                string url = string.Format("{0}/{1}?filter={2}", SPContext.Current.Web.Url, Pages.Issues.PAGE_URL, Filter);
                s = (AsLink) ? string.Format("<a href=\"{0}\" class=\"mapps-breadcrumb\">Ports</a>", url) : url;
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
            return s;
        }

        protected override void AddRibbonTab() {
            try {
                switch (IView) {
                    case ItemView.New:
                        RibbonItem("Ribbon.MAPPS.Commit.SaveItem.Click", jsActionSave, true);
                        RibbonItem("Ribbon.MAPPS.Commit.Cancel.Click", jsActionCancel, true);
                        break;
                    case ItemView.View:
                        RibbonItem("Ribbon.MAPPS.Manage.EditItem.Click", jsActionEdit, IsAdmin);
                        RibbonItem("Ribbon.MAPPS.Commit.DeleteItem.Click", jsActionDelete, IsAdmin);
                        RibbonItem("Ribbon.MAPPS.Commit.Cancel.Click", jsActionCancel, true);
                        break;
                    case ItemView.Edit:
                        RibbonItem("Ribbon.MAPPS.Manage.ViewItem.Click", jsActionView, true);
                        RibbonItem("Ribbon.MAPPS.Commit.SaveItem.Click", jsActionSave, IsAdmin);
                        RibbonItem("Ribbon.MAPPS.Commit.DeleteItem.Click", jsActionDelete, IsAdmin);
                        RibbonItem("Ribbon.MAPPS.Commit.Cancel.Click", jsActionCancel, true);
                        break;
                }

                base.AddRibbonTab();
                Ribbon.Minimized = true;
                //Ribbon.InitialTabId = RIBBON_INITIAL_TAB_ID;

            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }

        protected override void AddTabEvents() {
            try {
                switch (IView) {
                    case ItemView.New:
                        RibbonItem("Ribbon.MAPPS.Commit.SaveItem.Click", jsActionSave, true);
                        RibbonItem("Ribbon.MAPPS.Commit.Cancel.Click", jsActionCancel, true);
                        break;
                    case ItemView.View:
                        RibbonItem("Ribbon.MAPPS.Manage.EditItem.Click", jsActionEdit, IsAdmin);
                        RibbonItem("Ribbon.MAPPS.Commit.DeleteItem.Click", jsActionDelete, IsAdmin);
                        RibbonItem("Ribbon.MAPPS.Commit.Cancel.Click", jsActionCancel, true);
                        break;
                    case ItemView.Edit:
                        RibbonItem("Ribbon.MAPPS.Manage.ViewItem.Click", jsActionView, true);
                        RibbonItem("Ribbon.MAPPS.Commit.SaveItem.Click", jsActionSave, IsAdmin);
                        RibbonItem("Ribbon.MAPPS.Commit.DeleteItem.Click", jsActionDelete, IsAdmin);
                        RibbonItem("Ribbon.MAPPS.Commit.Cancel.Click", jsActionCancel, true);
                        break;
                }
                base.AddTabEvents();
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }

        private void SetupContribute() {

            if (IsManager) {
                item = (ItemID == 0) ? new MAPPS.ProjectIssue() : new MAPPS.ProjectIssue(ItemID);
            } else {
                Response.Redirect(string.Format("{0}/{1}?code={2}", SPContext.Current.Web.Url, Message.URL_USERMESSAGE, Message.Code.MngrAccessReq), false);

            }
        }

        private void HandlePostBack() {
            try {
                Page.Validate();
                if (Page.IsValid) {
                    switch (this.Page.Request["__EVENTTARGET"]) {
                        case RIBBON_POSTBACK_SAVE_EVENT:
                            if (SaveItem())
                                if (SPContext.Current.IsPopUI)
                                    Script("window.frameElement.commitPopup('');");
                                else
                                    Response.Redirect(string.Format("{0}/{1}?filter={2}", SPContext.Current.Web.Url, Pages.Issues.PAGE_URL, Filter), false);
                            break;
                        case RIBBON_POSTBACK_DELETE_EVENT:
                            if (DeleteItem())
                                if (SPContext.Current.IsPopUI)
                                    Script("window.frameElement.commitPopup('');");
                                else
                                    Response.Redirect(string.Format("{0}/{1}?filter={2}", SPContext.Current.Web.Url, Pages.Issues.PAGE_URL, Filter), false);
                            else
                                ScriptAlert("Deleting this item failed. Verify there are no associated items and  try again. If you continue see this error, contact an administrator.");
                            break;
                        case RIBBON_POSTBACK_EDIT_EVENT:
                            Response.Redirect(QueryStringParameter(CurrentURL, Request.QueryString, "View", "Edit"), false);
                            break;
                        case RIBBON_POSTBACK_VIEW_EVENT:
                            Response.Redirect(QueryStringParameter(CurrentURL, Request.QueryString, "View", "View"), false);
                            break;
                    }
                } else {
                    Script("resizeModalDialog('True');");
                }
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }


        protected override void Fill() {
            try {
                bool isView = (IView == ItemView.View);
                bool isNew = (IView == ItemView.New);

                tdEdit.Visible = isView;
                tdView.Visible = !isView && ItemID != 0;
                tdDelete.Visible = !isView && ItemID != 0;
                tdSave.Visible = !isView;

                txtIssue.Text = item.Issue;
                txtIssue.Visible = !isView;
                lblIssueView.Text = item.Issue;
                lblIssueView.Visible = isView;
                lblIssueRequired.Visible = !isView;

                txtActionTaken.Text = item.ActionTaken.ToString();
                txtActionTaken.Visible = !isView;
                lblActionTakenView.Text = item.ActionTaken.ToString();
                lblActionTakenView.Visible = isView;
                lblActionTakenRequired.Visible = !isView;

                txtTicketNumber.Text = item.TicketNumber.ToString();
                txtTicketNumber.Visible = !isView;
                lblTicketNumberView.Text = item.TicketNumber.ToString();
                lblTicketNumberView.Visible = isView;

                lblCreatedInfo.Text = string.Format("Created at {0} by {1}", MAPPS.Common.ConvertUTCToWebLocalTime(this.Web, item.CreatedOn), item.CreatedBy);
                lblCreatedInfo.Visible = (item.ID != 0);
                lblUpdatedInfo.Text = string.Format("Last modified at {0} by {1}", MAPPS.Common.ConvertUTCToWebLocalTime(this.Web, item.ModifiedOn), item.ModifiedBy);
                lblUpdatedInfo.Visible = (item.ID != 0);

                btnSave.Visible = !isView;
                btnCancel.Text = isView ? "Close" : "Cancel";

            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }

        private bool SaveItem() {
            bool success = false;

            try {
                bool isUpdate = (IView == ItemView.Edit);
                item = (isUpdate) ? new MAPPS.ProjectIssue(ItemID) : new MAPPS.ProjectIssue();
                item.Issue = txtIssue.Text.Trim();
                item.ActionTaken = txtActionTaken.Text.Trim();
                item.TicketNumber = txtTicketNumber.Text.Trim();
                item.ModifiedBy = CurrentUser.DisplayName;

                if (!isUpdate) {
                    item.CreatedBy = item.ModifiedBy;
                    if (item.Insert()) {
                        success = true;
                    }
                } else {
                    if (item.Update()) {
                        success = true;
                    }
                }
                ItemID = (success) ? item.ID : 0;
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
            return success;
        }

        private bool DeleteItem() {
            bool success = false;
            try {
                MAPPS.ProjectIssue item = new MAPPS.ProjectIssue(ItemID);
                if (item.Delete()) {
                    success = true;
                    ItemID = 0;
                } else {
                    // display message that item is im use
                }
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
            return success;
        }


        #endregion _Methods_

        protected void btnSave_Click(object sender, EventArgs e) {
            SaveItem();
            Response.Redirect(string.Format("{0}/{1}?Filter={2}", SPContext.Current.Web.Url, Pages.Issues.PAGE_URL, Filter), false);
        }

        protected void btnCancel_Click(object sender, EventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?Filter={2}", SPContext.Current.Web.Url, Pages.Issues.PAGE_URL, Filter), false);
        }

        protected void ibtnRibbonNew_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?View=New&id={2}&portid={2}&IsDlg=1&Filter={3}", SPContext.Current.Web.Url, Pages.IssueItem.PAGE_URL, 0, Filter), false);
        }

        protected void lbtnRibbonNew_Click(object sender, EventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?View=New&id={2}&portid={2}&IsDlg=1&Filter={3}", SPContext.Current.Web.Url, Pages.IssueItem.PAGE_URL, 0, Filter), false);
        }

        protected void ibtnRibbonView_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?View=View&id={2}&portid={2}&IsDlg=1&Filter={3}", SPContext.Current.Web.Url, Pages.IssueItem.PAGE_URL, ItemID, Filter), false);
        }

        protected void lbtnRibbonView_Click(object sender, EventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?View=View&id={2}&portid={2}&IsDlg=1&Filter={3}", SPContext.Current.Web.Url, Pages.IssueItem.PAGE_URL, ItemID, Filter), false);
        }

        protected void ibtnRibbonEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?View=Edit&id={2}&portid={2}&IsDlg=1&Filter={3}", SPContext.Current.Web.Url, Pages.IssueItem.PAGE_URL, ItemID, Filter), false);

        }

        protected void lbtnRibbonEdit_Click(object sender, EventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?View=Edit&id={2}&portid={2}&IsDlg=1&Filter={3}", SPContext.Current.Web.Url, Pages.IssueItem.PAGE_URL, ItemID, Filter), false);
        }

        protected void ibtnRibbonSave_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            SaveItem();
            if (SPContext.Current.IsPopUI)
                Script("window.frameElement.commitPopup('');");
            else
                Response.Redirect(string.Format("{0}/{1}?Filter={2}", SPContext.Current.Web.Url, Pages.Issues.PAGE_URL, Filter), false);
        }

        protected void lbtnRibbonSave_Click(object sender, EventArgs e) {
            SaveItem();
            if (SPContext.Current.IsPopUI)
                Script("window.frameElement.commitPopup('');");
            else
                Response.Redirect(string.Format("{0}/{1}?Filter={2}", SPContext.Current.Web.Url, Pages.Issues.PAGE_URL, Filter), false);
        }

        protected void ibtnRibbonDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            DeleteItem();
            if (SPContext.Current.IsPopUI)
                Script("window.frameElement.commitPopup('');");
            else
                Response.Redirect(string.Format("{0}/{1}?Filter={2}", SPContext.Current.Web.Url, Pages.Issues.PAGE_URL, Filter), false);
        }

        protected void lbtnRibbonDelete_Click(object sender, EventArgs e) {
            DeleteItem();
            if (SPContext.Current.IsPopUI)
                Script("window.frameElement.commitPopup('');");
            else
                Response.Redirect(string.Format("{0}/{1}?Filter={2}", SPContext.Current.Web.Url, Pages.Issues.PAGE_URL, Filter), false);
        }

        protected void ibtnRibbonCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?Filter={2}", SPContext.Current.Web.Url, Pages.Issues.PAGE_URL, Filter), false);
        }

        protected void lbtnRibbonCancel_Click(object sender, EventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?Filter={2}", SPContext.Current.Web.Url, Pages.Issues.PAGE_URL, Filter), false);
        }
    }
}
