using System;
using System.Data;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using System.Collections.Generic;

namespace MAPPS.Pages {
    public partial class ApplicationItem : PageBase {
        protected const string PAGE_TITLE = "Application Details";
        protected const string PAGE_DESCRIPTION = "View and manage component details";
        public const string PAGE_URL = BASE_PAGES_URL + "ApplicationItem.aspx";

        protected string jsActionCancel;
        private string itemRedirect;
        private string newItemRedirect;

        private Application item;

        #region _Methods_

        protected override void Page_Load(object sender, EventArgs e) {
            try {
                base.Page_Load(sender, e);

                SetupContribute();
                btnSave.OnClientClick = jsActionSave;
                jsActionCancel = string.Format("rbnCancelClick('{0}/{1}'); return false;", SPContext.Current.Web.Url, Pages.Applications.PAGE_URL);
                itemRedirect = string.Format("{0}/{1}", SPContext.Current.Web.Url, Pages.Applications.PAGE_URL);
                newItemRedirect = string.Format("{0}/{1}?View=New&id={2}", SPContext.Current.Web.Url, Pages.ApplicationItem.PAGE_URL, 0);

                btnCancel.OnClientClick = jsActionCancel;

                if (!IsPostBack) {
                    lbtnDelete.OnClientClick = "return confirm('Are you sure you want to delete this item?');";
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
                //string url = itemRedirect;
                s = (AsLink) ? string.Format("<a href=\"{0}\" class=\"mapps-breadcrumb\">Applications</a>", itemRedirect) : itemRedirect;
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
                item = (ItemID == 0) ? new Application() : new Application(ItemID);
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
                                    Response.Redirect(itemRedirect, false);
                            break;
                        case RIBBON_POSTBACK_DELETE_EVENT:
                            if (DeleteItem())
                                if (SPContext.Current.IsPopUI)
                                    Script("window.frameElement.commitPopup('');");
                                else
                                    Response.Redirect(itemRedirect, false);
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
                lbtnEdit.Visible = isView && !item.IsProtected;
                lbtnView.Visible = !isView && ItemID != 0;
                lbtnDelete.Visible = !isView && ItemID != 0;
                lblReqMsg.Visible = !isView;

                txtName.Text = item.Name;
                txtName.Visible = !isView;
                lblNameView.Text = item.Name;
                lblNameView.Visible = isView;
                lblNameRequired.Visible = !isView;

                txtDescription.Text = item.Description;
                txtDescription.Visible = !isView;
                lblDescriptionView.Text = item.Description;
                lblDescriptionView.Visible = isView;
                lblDescriptionRequired.Visible = !isView;

                txtDisplayIndex.Text = item.DisplayIndex.ToString();
                txtDisplayIndex.Visible = !isView;
                lblDisplayIndexView.Text = item.DisplayIndex.ToString();
                lblDisplayIndexView.Visible = isView;
                lblDisplayIndexRequired.Visible = !isView;

                cbIsActive.Checked = item.IsActive;
                cbIsActive.Visible = !isView;
                lblIsActiveView.Text = item.IsActive ? "Yes" : "No";
                lblIsActiveView.Visible = isView;

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
                item = (isUpdate) ? new Application(ItemID) : new Application();
                item.Name = txtName.Text.Trim();
                item.Description = txtDescription.Text.Trim();
                if (!Common.IsNumeric(txtDisplayIndex.Text.Trim()))
                    txtDisplayIndex.Text = "1";
                item.DisplayIndex = int.Parse(txtDisplayIndex.Text.Trim());
                item.IsActive = cbIsActive.Checked;
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
                Application item = new Application(ItemID);
                if (!item.IsProtected) {
                    //if (!Application.InUse(item.ID)) {
                        if (item.Delete()) {
                            success = true;
                            ItemID = 0;
                        } else {
                            // notify user the record deletion failed
                            Common.AlertMsg("Attempting to delete this item generated an error", this.Page);
                        }
                    //} else {
                    //    // notify user the record can not be deleted because it is inuse
                    //    Common.AlertMsg("This item cannot be deleted because it is associated with one or more records", this.Page);
                    //}
                } else {
                    // notify user the record can not be deleted because it is protected
                    Common.AlertMsg("This item cannot be deleted because it is protected", this.Page);
                }
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
            return success;
        }


        #endregion _Methods_

        protected void lbtnNew_Click(object sender, EventArgs e) {
            Response.Redirect(newItemRedirect, false);
        }

        protected void lbtnEdit_Click(object sender, EventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?View=Edit&id={2}", SPContext.Current.Web.Url, Pages.ApplicationItem.PAGE_URL, ItemID), false);
        }

        protected void lbtnView_Click(object sender, EventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?View=View&id={2}", SPContext.Current.Web.Url, Pages.ApplicationItem.PAGE_URL, ItemID), false);
        }

        protected void lbtnDelete_Click(object sender, EventArgs e) {
            DeleteItem();
            Response.Redirect(itemRedirect, false);
        }

        protected void btnSave_Click(object sender, EventArgs e) {
            SaveItem();
            Response.Redirect(itemRedirect, false);
        }

        protected void btnCancel_Click(object sender, EventArgs e) {
            Response.Redirect(itemRedirect, false);
        }
    }
}
