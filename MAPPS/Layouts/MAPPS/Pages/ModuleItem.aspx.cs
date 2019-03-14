using System;
using System.Data;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using System.Collections.Generic;

namespace MAPPS.Pages {
    public partial class ModuleItem : PageBase {
        protected const string PAGE_TITLE = "Item";
        protected const string PAGE_DESCRIPTION = "View and manage a modules";
        public const string PAGE_URL = BASE_PAGES_URL + "ModuleItem.aspx";

        protected string jsActionCancel = string.Format("rbnCancelClick('{0}/{1}'); return false;", SPContext.Current.Web.Url, Pages.Modules.PAGE_URL);
        private Module item;

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
                } else {
                    HandlePostBack();
                }
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }
        public virtual string GetModulesURL(bool AsLink) {
            string s = string.Empty;
            try {
                string url = string.Format("{0}/{1}", SPContext.Current.Web.Url, Pages.Modules.PAGE_URL);
                s = (AsLink) ? string.Format("<a href=\"{0}\" class=\"mapps-breadcrumb\">Modules</a>", url) : url;
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
                Ribbon.Minimized = false;
                Ribbon.InitialTabId = RIBBON_INITIAL_TAB_ID;

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
            item = (ItemID == 0) ? new Module() : new Module(ItemID);
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
                                    Response.Redirect(string.Format("{0}/{1}", SPContext.Current.Web.Url, Pages.Modules.PAGE_URL), false);
                            break;
                        case RIBBON_POSTBACK_DELETE_EVENT:
                            if (DeleteItem())
                                if (SPContext.Current.IsPopUI)
                                    Script("window.frameElement.commitPopup('');");
                                else
                                    Response.Redirect(string.Format("{0}/{1}", SPContext.Current.Web.Url, Pages.Modules.PAGE_URL), false);
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

                txtName.Text = item.Name;
                txtName.Visible = !isView;
                lblNameView.Text = item.Name;
                lblNameView.Visible = isView;
                lblNameRequired.Visible = !isView;

                txtDescription.Text = item.Description;
                txtDescription.Visible = !isView;
                lblDescriptionView.Text = item.Description;
                lblDescriptionView.Visible = isView;

                txtDirectory.Text = item.Directory;
                txtDirectory.Visible = !isView;
                lblDirectoryView.Text = item.Directory;
                lblDirectoryView.Visible = isView;

                txtURL.Text = item.URL;
                txtURL.Visible = !isView;
                lblURLView.Text = item.URL;
                lblURLView.Visible = isView;
                lblURLRequired.Visible = !isView;

                txtAdminURL.Text = item.AdminURL;
                txtAdminURL.Visible = !isView;
                lblAdminURLView.Text = item.AdminURL;
                lblAdminURLView.Visible = isView;
                lblAdminURLRequired.Visible = !isView;

                txtImageURL.Text = item.ImageURL;
                txtImageURL.Visible = !isView;
                lblImageURLView.Text = item.ImageURL;
                lblImageURLView.Visible = isView;
                lblImageURLRequired.Visible = !isView;

                txtDBVersion.Text = item.DBVersion;
                txtDBVersion.Visible = !isView;
                lblDBVersionView.Text = item.DBVersion;
                lblDBVersionView.Visible = isView;
                lblDBVersionRequired.Visible = !isView;

                txtDisplayIndex.Text = item.DisplayIndex.ToString();
                txtDisplayIndex.Visible = !isView;
                lblDisplayIndexView.Text = item.DisplayIndex.ToString();
                lblDisplayIndexView.Visible = isView;
                lblDisplayIndexRequired.Visible = !isView;

                cbActive.Checked = item.IsActive;
                cbActive.Visible = !isView;
                lblActiveView.Text = item.IsActive ? "Yes" : "No";
                lblActiveView.Visible = isView;

                lblCreatedInfo.Text = string.Format("Created at {0} by {1}", MAPPS.Common.ConvertUTCToWebLocalTime (this.Web, item.CreatedOn), item.CreatedBy);
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
                if (!IsValid) {
                    Script("resizeModalDialog('True');");
                }

                bool isUpdate = (IView == ItemView.Edit);
                item = (isUpdate) ? new Module(ItemID) : new Module();
                item.Name = txtName.Text.Trim();
                item.Description = txtDescription.Text.Trim();
                item.Directory = txtDirectory.Text.Trim();
                item.URL = txtURL.Text.Trim();
                item.AdminURL = txtAdminURL.Text.Trim();
                item.ImageURL = txtImageURL.Text.Trim();
                item.DBVersion = txtDBVersion.Text.Trim();
                item.DisplayIndex = Common.IsNumeric(txtDisplayIndex.Text.Trim()) ? int.Parse(txtDisplayIndex.Text.Trim()) : 0;
                item.IsActive = cbActive.Checked;
                item.ModifiedBy = CurrentUser.DisplayName;

                if (!isUpdate) {
                    item.CreatedBy = item.ModifiedBy;
                    if (item.Insert()) {
                        success = true;
                        Action.Write(string.Format("Module[{1}] titled {0} created", item.Name, item.ID), CurrentUserWithDomain);
                    }
                } else {
                    if (item.Update()) {
                        success = true;
                        Action.Write(string.Format("Module[{1}] titled {0} updated", item.Name, item.ID), CurrentUserWithDomain);
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
                Module item = new Module(ItemID);
                if (item.Delete())
                    success = true;
                ItemID = 0;
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
            return success;
        }


        #endregion _Methods_
    }
}
