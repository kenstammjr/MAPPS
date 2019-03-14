using System;
using System.Data;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using System.Collections.Generic;

namespace MAPPS.Pages {
    public partial class SettingItem : PageBase {
        protected const string PAGE_TITLE = "Item";
        protected const string PAGE_DESCRIPTION = "View and manage settings";
        public const string PAGE_URL = BASE_PAGES_URL + "SettingItem.aspx";

        protected string jsActionCancel = string.Format("rbnCancelClick('{0}/{1}'); return false;", SPContext.Current.Web.Url, Pages.Settings.PAGE_URL);
        private Setting item;

        #region _Methods_

        protected override void Page_Load(object sender, EventArgs e) {
            try {
                base.Page_Load(sender, e);

                //UserControlBase ucb = (LeftNavBar)LoadControl("~/_ControlTemplates/15/MAPPS/LeftNavBar.ascx");
                //ucb.Tab = UserControlBase.Tabs.Administration;
                //PlaceHolderLeftNavBar.Controls.Add(ucb);

                SetupContribute();
                btnSave.OnClientClick = jsActionSave;
                btnCancel.OnClientClick = jsActionCancel;

                if (!IsPostBack) {
                    Fill();
                }
                else {
                    HandlePostBack();
                }
            }
            catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }
        public virtual string GetItemsURL(bool AsLink) {
            string s = string.Empty;
            try {
                string url = string.Format("{0}/{1}", SPContext.Current.Web.Url, Pages.Settings.PAGE_URL);
                s = (AsLink) ? string.Format("<a href=\"{0}\" class=\"mapps-breadcrumb\">Settings</a>", url) : url;
            }
            catch (Exception ex) {
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
                Ribbon.Minimized = false;
                //Ribbon.InitialTabId = RIBBON_INITIAL_TAB_ID;

            }
            catch (Exception ex) {
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
            }
            catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }

        private void SetupContribute() {
            item = (ItemID == 0) ? new Setting() : new Setting(ItemID);
        }

        private void HandlePostBack() {
            try {
                // Ensure the page validation is called before checking isvalid
                Page.Validate();

                if (Page.IsValid) {
                    switch (this.Page.Request["__EVENTTARGET"]) {
                        case RIBBON_POSTBACK_SAVE_EVENT:
                            if (SaveItem())
                                if (SPContext.Current.IsPopUI)
                                    Script("window.frameElement.commitPopup('');");
                                else
                                    Response.Redirect(string.Format("{0}/{1}", SPContext.Current.Web.Url, Pages.Settings.PAGE_URL), false);
                            break;
                        case RIBBON_POSTBACK_DELETE_EVENT:
                            if (DeleteItem())
                                if (SPContext.Current.IsPopUI)
                                    Script("window.frameElement.commitPopup('');");
                                else
                                    Response.Redirect(string.Format("{0}/{1}", SPContext.Current.Web.Url, Pages.Settings.PAGE_URL), false);
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
                }
                else {
                    Script("resizeModalDialog('True');");
                }
            }
            catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }

        protected override void Fill() {
            try {
                bool isView = (IView == ItemView.View);
                bool isNew = (IView == ItemView.New);

                txtKey.Text = item.Key;
                txtKey.Visible = !isView;
                lblKeyView.Text = item.Key;
                lblKeyView.Visible = isView;
                lblKeyRequired.Visible = !isView;

                txtValue.Text = item.IsPassword ? "***********" : item.Value;
                txtValue.Visible = !isView;
                lblValueView.Text = item.IsPassword ? "***********" : item.Value;
                lblValueView.Visible = isView;
                lblValueRequired.Visible = !isView;

                txtDescription.Text = item.Description;
                txtDescription.Visible = !isView;
                lblDescriptionView.Text = item.Description;
                lblDescriptionView.Visible = isView;
                lblDescriptionRequired.Visible = !isView;


                cbPassword.Checked = item.IsPassword;
                cbPassword.Visible = !isView;
                lblPasswordView.Text = item.IsPassword ? "Yes" : "No";
                lblPasswordView.Visible = isView;

                cbMultiline.Checked = item.IsMultiline;
                cbMultiline.Visible = !isView;
                lblMultilineView.Text = item.IsMultiline ? "Yes" : "No";
                lblMultilineView.Visible = isView;

                if (item.IsMultiline)
                    txtValue.TextMode = TextBoxMode.MultiLine;
                else
                    txtValue.TextMode = TextBoxMode.SingleLine;

                if (item.IsPassword) {
                    txtValue.TextMode = TextBoxMode.Password;
                    cbPassword.Enabled = false;
                }

                lblUpdatedInfo.Text = string.Format("Last modified at {0} by {1}", MAPPS.Common.ConvertUTCToWebLocalTime(this.Web, item.ModifiedOn), item.ModifiedBy);
                lblUpdatedInfo.Visible = (item.ID != 0);

                btnSave.Visible = !isView;
                btnCancel.Text = isView ? "Close" : "Cancel";
            }
            catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }

        private bool SaveItem() {
            bool success = false;
            try {
                bool isUpdate = (IView == ItemView.Edit);
                item = (isUpdate) ? new Setting(ItemID) : new Setting();
                item.Key = txtKey.Text.Trim();
                item.Value = txtValue.Text.Trim();
                item.Description = txtDescription.Text.Trim();
                item.ModifiedBy = CurrentUser.DisplayName;

                if (!isUpdate) {

                    if (item.Insert()) {
                        success = true;
                        Transaction xAction = new Transaction();
                        xAction.Action = string.Format("Application Setting[{1}] {0} created", item.Key, item.ID);
                        xAction.Category = "ADMIN";
                        xAction.Type = Transaction.TYPE_SUCCESS;
                        xAction.CreatedBy = CurrentUserWithDomain;
                        xAction.Insert();
                    }
                    else {
                        Transaction xAction = new Transaction();
                        xAction.Action = string.Format("Application Setting[{1}] {0} created", item.Key, item.ID);
                        xAction.Category = "ADMIN";
                        xAction.Type = Transaction.TYPE_FAILURE;
                        xAction.CreatedBy = CurrentUserWithDomain;
                        xAction.Insert();
                    }
                }
                else {
                    if (item.Update()) {
                        success = true;
                        Transaction xAction = new Transaction();
                        xAction.Action = string.Format("Application Setting[{1}] {0} updated", item.Key, item.ID);
                        xAction.Category = "ADMIN";
                        xAction.Type = Transaction.TYPE_SUCCESS;
                        xAction.CreatedBy = CurrentUserWithDomain;
                        xAction.Insert();
                    }
                    else {
                        Transaction xAction = new Transaction();
                        xAction.Action = string.Format("Application Setting[{1}] {0} update", item.Key, item.ID);
                        xAction.Category = "ADMIN";
                        xAction.Type = Transaction.TYPE_FAILURE;
                        xAction.CreatedBy = CurrentUserWithDomain;
                        xAction.Insert();
                    }
                }
                ItemID = (success) ? item.ID : 0;
            }
            catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
            return success;
        }

        private bool DeleteItem() {
            bool success = false;
            try {
                Setting item = new Setting(ItemID);
                if (item.Delete())
                    success = true;
                ItemID = 0;
            }
            catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
            return success;
        }


        #endregion _Methods_
    }
}
