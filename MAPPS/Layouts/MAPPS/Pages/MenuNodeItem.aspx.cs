using System;
using System.Data;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using System.Collections.Generic;

namespace MAPPS.Pages {
    public partial class MenuNodeItem : PageBase {
        protected const string PAGE_TITLE = "MenuNode Details";
        protected const string PAGE_DESCRIPTION = "View and manage menu node details";
        public const string PAGE_URL = BASE_PAGES_URL + "MenuNodeItem.aspx";

        protected string jsActionCancel;
        private MAPPS.MenuNode item;

        #region _Methods_

        protected override void Page_Load(object sender, EventArgs e) {
            try {
                base.Page_Load(sender, e);

                SetupContribute();
                ReadParameters();
                btnSave.OnClientClick = jsActionSave;
                jsActionCancel = string.Format("rbnCancelClick('{0}/{1}?filter={2}'); return false;", SPContext.Current.Web.Url, Pages.MenuNodes.PAGE_URL, Filter);
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
                string url = string.Format("{0}/{1}", SPContext.Current.Web.Url, Pages.MenuNodes.PAGE_URL);
                s = (AsLink) ? string.Format("<a href=\"{0}\" class=\"mapps-breadcrumb\">Nodes</a>", url) : url;
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

            if (CurrentUser.InRole("UserAdmin")) {
                item = (ItemID == 0) ? new MAPPS.MenuNode() : new MAPPS.MenuNode(ItemID);
            } else {
                Response.Redirect(string.Format("{0}/{1}?code={2}&IsDlg=1", SPContext.Current.Web.Url, Message.URL_USERMESSAGE, Message.Code.PersonAdminAccessReq), false);
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
                                    Response.Redirect(string.Format("{0}/{1}", SPContext.Current.Web.Url, Pages.MenuNodes.PAGE_URL), false);
                            break;
                        case RIBBON_POSTBACK_DELETE_EVENT:
                            if (DeleteItem())
                                if (SPContext.Current.IsPopUI)
                                    Script("window.frameElement.commitPopup('');");
                                else
                                    Response.Redirect(string.Format("{0}/{1}", SPContext.Current.Web.Url, Pages.MenuNodes.PAGE_URL), false);
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

                if (!IsPostBack) {
                    ddlParentNode.DataSource = MAPPS.MenuNode.Ordered();
                    ddlParentNode.DataTextField = "FullName";
                    ddlParentNode.DataValueField = "ID";
                    ddlParentNode.DataBind();
                    ddlParentNode.Items.Insert(0, new ListItem("Root", "0"));
                    ddlParentNode.SelectedIndex = 0;
                }


                txtName.Text = item.Name;
                txtName.Visible = !isView;
                lblNameView.Text = item.Name;
                lblNameView.Visible = isView;
                lblNameRequired.Visible = !isView;

                txtDescription.Text = item.Description;
                txtDescription.Visible = !isView;
                lblDescriptionView.Text = item.Description;
                lblDescriptionView.Visible = isView;

                txtURL.Text = item.URL;
                txtURL.Visible = !isView;
                lblURLView.Text = item.URL;
                lblURLView.Visible = isView;
                lblURLRequired.Visible = !isView;

                txtDisplayIndex.Text = item.DisplayIndex.ToString();
                txtDisplayIndex.Visible = !isView;
                lblDisplayIndexView.Text = item.DisplayIndex.ToString();
                lblDisplayIndexView.Visible = isView;
                lblDisplayIndexRequired.Visible = !isView;

                txtTarget.Text = item.Target.ToString();
                txtTarget.Visible = !isView;
                lblTargetView.Text = item.Target.ToString();
                lblTargetView.Visible = isView;

                cbIsVisible.Checked = item.IsVisible;
                cbIsVisible.Visible = !isView;
                lblIsVisibleView.Text = item.IsVisible ? "Yes" : "No";
                lblIsVisibleView.Visible = isView;

                ddlParentNode.SelectedIndex = -1;
                ddlParentNode.Items.FindByValue(item.ParentID.ToString()).Selected = true;
                ddlParentNode.Visible = !isView;
                lblParentNodeView.Text = new MenuNode(item.ParentID).Name;
                lblParentNodeView.Visible = isView;

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
                if (!IsValid) {
                    Script("resizeModalDialog('True');");
                }

                bool isUpdate = (IView == ItemView.Edit);
                item = (isUpdate) ? new MAPPS.MenuNode(ItemID) : new MAPPS.MenuNode();
                item.MenuID = 1;
                item.Name = txtName.Text.Trim();
                item.URL = txtURL.Text.Trim();
                item.Description = txtDescription.Text.Trim();
                item.DisplayIndex = Common.IsNumeric(txtDisplayIndex.Text.Trim()) ? int.Parse(txtDisplayIndex.Text.Trim()) : 0;
                item.IsVisible = cbIsVisible.Checked;
                item.Target = txtTarget.Text.Trim().Length != 0 ? txtTarget.Text.Trim() : "_self";
                item.ParentID = int.Parse(ddlParentNode.SelectedItem.Value);
                item.ModifiedBy = CurrentUser.UserName;

                if (!isUpdate) {
                    item.CreatedBy = CurrentUser.UserName;
                    if (item.Insert()) {
                        success = true;
                        //Action.Write(string.Format("{2} OrganizationDefinitions[{1}] of {0} created", item.Name, item.ID, ddlSubComponent.SelectedItem.Text), CurrentUserWithDomain);
                    }
                } else {
                    if (item.Update()) {
                        success = true;
                        //Action.Write(string.Format("{2} OrganizationDefinitions[{1}] of {0} updated", item.Name, item.ID, ddlSubComponent.SelectedItem.Text), CurrentUserWithDomain);
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
                MAPPS.MenuNode item = new MAPPS.MenuNode(ItemID);
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

        protected void btnSave_Click(object sender, EventArgs e) {
            SaveItem();
            Response.Redirect(string.Format("{0}/{1}?Filter={2}", SPContext.Current.Web.Url, Pages.MenuNodes.PAGE_URL, Filter), false);
        }

        protected void btnCancel_Click(object sender, EventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?Filter={2}", SPContext.Current.Web.Url, Pages.MenuNodes.PAGE_URL, Filter), false);

        }
        protected void ibtnRibbonNew_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?View=New&id={2}&IsDlg=1&Filter={3}", SPContext.Current.Web.Url, Pages.MenuNodeItem.PAGE_URL, 0, Filter), false);
        }

        protected void lbtnRibbonNew_Click(object sender, EventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?View=New&id={2}&IsDlg=1&Filter={3}", SPContext.Current.Web.Url, Pages.MenuNodeItem.PAGE_URL, 0, Filter), false);
        }

        protected void ibtnRibbonView_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?View=View&id={2}&IsDlg=1&Filter={3}", SPContext.Current.Web.Url, Pages.MenuNodeItem.PAGE_URL, ItemID, Filter), false);
        }

        protected void lbtnRibbonView_Click(object sender, EventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?View=View&id={2}&IsDlg=1&Filter={3}", SPContext.Current.Web.Url, Pages.MenuNodeItem.PAGE_URL, ItemID, Filter), false);
        }

        protected void ibtnRibbonEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?View=Edit&id={2}&IsDlg=1&Filter={3}", SPContext.Current.Web.Url, Pages.MenuNodeItem.PAGE_URL, ItemID, Filter), false);
        }

        protected void lbtnRibbonEdit_Click(object sender, EventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?View=Edit&id={2}&IsDlg=1&Filter={3}", SPContext.Current.Web.Url, Pages.MenuNodeItem.PAGE_URL, ItemID, Filter), false);
        }

        protected void ibtnRibbonSave_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            SaveItem();
            if (SPContext.Current.IsPopUI)
                Script("window.frameElement.commitPopup('');");
            else
                Response.Redirect(string.Format("{0}/{1}?Filter={2}", SPContext.Current.Web.Url, Pages.MenuNodes.PAGE_URL, Filter), false);
        }

        protected void lbtnRibbonSave_Click(object sender, EventArgs e) {
            SaveItem();
            if (SPContext.Current.IsPopUI)
                Script("window.frameElement.commitPopup('');");
            else
                Response.Redirect(string.Format("{0}/{1}?Filter={2}", SPContext.Current.Web.Url, Pages.MenuNodes.PAGE_URL, Filter), false);
        }

        protected void ibtnRibbonDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            DeleteItem();
            if (SPContext.Current.IsPopUI)
                Script("window.frameElement.commitPopup('');");
            else
                Response.Redirect(string.Format("{0}/{1}?Filter={2}", SPContext.Current.Web.Url, Pages.MenuNodes.PAGE_URL, Filter), false);
        }

        protected void lbtnRibbonDelete_Click(object sender, EventArgs e) {
            DeleteItem();
            if (SPContext.Current.IsPopUI)
                Script("window.frameElement.commitPopup('');");
            else
                Response.Redirect(string.Format("{0}/{1}?Filter={2}", SPContext.Current.Web.Url, Pages.MenuNodes.PAGE_URL, Filter), false);
        }

        protected void ibtnRibbonCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?Filter={2}", SPContext.Current.Web.Url, Pages.MenuNodes.PAGE_URL, Filter), false);
        }

        protected void lbtnRibbonCancel_Click(object sender, EventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?Filter={2}", SPContext.Current.Web.Url, Pages.MenuNodes.PAGE_URL, Filter), false);
        }

    }
}
