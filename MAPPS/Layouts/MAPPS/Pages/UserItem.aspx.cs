using System;
using System.Data;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using System.Collections.Generic;
using Microsoft.SharePoint.WebControls;
using Microsoft.Office.Server.UserProfiles;

namespace MAPPS.Pages {
    public partial class UserItem : PageBase {
        protected const string PAGE_TITLE = "User Record";
        protected const string PAGE_DESCRIPTION = "View and manage a user record";
        public const string PAGE_URL = BASE_PAGES_URL + "UserItem.aspx";

        protected string jsActionCancel;

        private MAPPS.User item;

        #region _Methods_

        protected override void Page_Load(object sender, EventArgs e) {
            try {
                base.Page_Load(sender, e);

                SetupContribute();
                ReadParameters();

                btnSave.OnClientClick = jsActionSave;
                jsActionCancel = string.Format("rbnCancelClick('{0}/{1}?filter={2}'); return false;", SPContext.Current.Web.Url, Pages.Users.PAGE_URL, Filter);
                btnCancel.OnClientClick = jsActionCancel;
                ibtnRibbonCancel.OnClientClick = jsActionCancel;
                lbtnRibbonCancel.OnClientClick = jsActionCancel;

                if (spePickUser.ResolvedEntities.Count > 0) {
                    foreach (PickerEntity entity in spePickUser.ResolvedEntities) {
                        txtUserName.Text = entity.Claim.Value;

                        UserProfileManager userProfileManager = new UserProfileManager(SPServiceContext.GetContext(SPContext.Current.Site));
                        ProfilePropertyManager profilePropMgr = new UserProfileConfigManager(SPServiceContext.GetContext(SPContext.Current.Site)).ProfilePropertyManager;
                        ProfileSubtypePropertyManager subtypePropMgr = profilePropMgr.GetProfileSubtypeProperties("UserProfile");
                        UserProfile userProfile = userProfileManager.GetUserProfile(entity.Claim.Value);
                        hfUserProfileRecordID.Value = (userProfile.RecordId.ToString());

                        try {
                            IEnumerator<ProfileSubtypeProperty> userProfileSubtypeProperties = subtypePropMgr.GetEnumerator();
                            while (userProfileSubtypeProperties.MoveNext()) {
                                string propName = userProfileSubtypeProperties.Current.Name;
                                ProfileValueCollectionBase values = userProfile.GetProfileValueCollection(propName);
                                if (values.Count > 0) {

                                    // Handle multivalue properties.
                                    foreach (var value in values) {
                                        switch (propName) {

                                            case "FirstName":
                                                txtFirstName.Text = value.ToString();
                                                break;
                                            case "LastName":
                                                txtLastName.Text = value.ToString();
                                                break;
                                            case "PreferredName":
                                                txtPreferredName.Text = value.ToString();
                                                break;
                                            case "UserProfile_GUID":
                                                lblSPObjectGuidView.Text = value.ToString();
                                                break;
                                                //case "SPS-UserPrincipalName":
                                                //    lblUserPrincipalNameView.Text = value.ToString();
                                                //    break;
                                                //case "SPS-DistinguishedName":
                                                //    lblDistinguishedNameView.Text = value.ToString();
                                                //    break;
                                        }
                                        if (ShowDebug)
                                            lblErrorMessage.Text += string.Format("Name: {0} Value: {1}</br>", propName, value.ToString());
                                    }
                                }
                            }
                        } catch {
                            txtLastName.Text = "Profile Not Found";
                            txtFirstName.Text = "Profile Not Found";
                        }
                    }
                }

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
                string url = string.Format("{0}/{1}?filter={2}", SPContext.Current.Web.Url, Pages.Users.PAGE_URL, Filter);
                s = (AsLink) ? string.Format("<a href=\"{0}\" class=\"mapps-breadcrumb\">User Catalog</a>", url) : url;
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

            if (CurrentUser.InRole("Manager") || CurrentUser.ID == UserID) {
                item = (ItemID == 0) ? new MAPPS.User() : new MAPPS.User(ItemID);
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
                                    Response.Redirect(string.Format("{0}/{1}?filter={2}", SPContext.Current.Web.Url, Pages.Users.PAGE_URL, Filter), false);
                            break;
                        case RIBBON_POSTBACK_DELETE_EVENT:
                            if (DeleteItem())
                                if (SPContext.Current.IsPopUI)
                                    Script("window.frameElement.commitPopup('');");
                                else
                                    Response.Redirect(string.Format("{0}/{1}?filter={2}", SPContext.Current.Web.Url, Pages.Users.PAGE_URL, Filter), false);
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
                spePickUser.Visible = ItemID == 0;
                trLookup.Visible = ItemID == 0;

                if (!isNew)
                    txtUserName.Text = item.UserName;
                txtUserName.Visible = !isView && ItemID == 0;
                lblUserNameView.Text = item.UserName;
                lblUserNameView.Visible = isView || (!isView && ItemID != 0);
                lblUserNameRequired.Visible = !isView;

                if (!isNew)
                    txtLastName.Text = item.LastName;
                txtLastName.Visible = !isView;
                lblLastNameView.Text = item.LastName;
                lblLastNameView.Visible = isView;
                lblLastNameRequired.Visible = !isView;

                if (!isNew)
                    txtFirstName.Text = item.FirstName;
                txtFirstName.Visible = !isView;
                lblFirstNameView.Text = item.FirstName;
                lblFirstNameView.Visible = isView;
                lblFirstNameRequired.Visible = !isView;

                txtMiddleInitial.Text = item.MiddleInitial;
                txtMiddleInitial.Visible = !isView;
                lblMiddleInitialView.Text = item.MiddleInitial;
                lblMiddleInitialView.Visible = isView;

                txtGenerationalQualifier.Text = item.GenerationalQualifier;
                txtGenerationalQualifier.Visible = !isView;
                lblGenerationalQualifierView.Text = item.GenerationalQualifier;
                lblGenerationalQualifierView.Visible = isView;

                txtPreferredName.Text = item.PreferredName;
                txtPreferredName.Visible = !isView;
                lblPreferredNameView.Text = item.PreferredName;
                lblPreferredNameView.Visible = isView;

                if (!isNew)
                    txtEmail.Text = item.Email;
                txtEmail.Visible = !isView;
                lblEmailView.Text = item.Email;
                lblEmailView.Visible = isView;
                lblEmailRequired.Visible = !isView;

                ckbxSeniorStaff.Checked = item.SeniorStaff;
                ckbxSeniorStaff.Visible = !isView;
                lblSeniorStaffView.Text = item.SeniorStaff ? "Yes" : "No";
                lblSeniorStaffView.Visible = isView;

                lblADObjectGuidView.Text = item.ADObjectGuid;
                lblSPObjectGuidView.Text = item.SPObjectGuid;

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
                item = (isUpdate) ? new MAPPS.User(ItemID) : new MAPPS.User();
                item.UserName = txtUserName.Text.Trim();
                item.Email = txtEmail.Text.Trim();
                item.LastName = txtLastName.Text.Trim();
                item.FirstName = txtFirstName.Text.Trim();
                item.MiddleInitial = txtMiddleInitial.Text.Trim();
                item.GenerationalQualifier = txtGenerationalQualifier.Text.Trim();
                item.PreferredName = txtPreferredName.Text.Trim();
                item.ModifiedBy = CurrentUser.DisplayName;

                Transaction xAction = new Transaction();
                if (!isUpdate) {
                    item.CreatedBy = item.ModifiedBy;
                    if (item.Insert()) {
                        success = true;
                        xAction.Action = string.Format("Successfully added {0} to the user catalog", item.UserName);
                        xAction.Category = "Application Administration";
                        xAction.Type = Transaction.TYPE_SUCCESS;
                        xAction.CreatedBy = item.ModifiedBy;
                        xAction.Insert();
                    } else {
                        xAction.Action = string.Format("Failed to add {0} to the user catalog", item.UserName);
                        xAction.Category = "Application Administration";
                        xAction.Type = Transaction.TYPE_FAILURE;
                        xAction.CreatedBy = item.ModifiedBy;
                        xAction.Insert();
                    }
                } else {
                    if (item.Update()) {
                        success = true;
                        xAction.Action = string.Format("Successfully update {0} in the user catalog", item.UserName);
                        xAction.Category = "Application Administration";
                        xAction.Type = Transaction.TYPE_SUCCESS;
                        xAction.CreatedBy = item.ModifiedBy;
                        xAction.Insert();
                    } else {
                        xAction.Action = string.Format("Failed to update {0} in the user catalog", item.UserName);
                        xAction.Category = "Application Administration";
                        xAction.Type = Transaction.TYPE_FAILURE;
                        xAction.CreatedBy = item.ModifiedBy;
                        xAction.Insert();
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
                MAPPS.User item = new MAPPS.User(ItemID);
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

        #region _User_Events_

        protected void btnSave_Click(object sender, EventArgs e) {
            SaveItem();
            Response.Redirect(string.Format("{0}/{1}?Filter={2}", SPContext.Current.Web.Url, Pages.Users.PAGE_URL, Filter), false);

        }

        protected void btnCancel_Click(object sender, EventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?Filter={2}", SPContext.Current.Web.Url, Pages.Users.PAGE_URL, Filter), false);

        }
        protected void ibtnRibbonNew_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?View=New&id={2}&userid={2}&IsDlg=1&Filter={3}", SPContext.Current.Web.Url, Pages.UserItem.PAGE_URL, 0, Filter), false);
        }

        protected void lbtnRibbonNew_Click(object sender, EventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?View=New&id={2}&userid={2}&IsDlg=1&Filter={3}", SPContext.Current.Web.Url, Pages.UserItem.PAGE_URL, 0, Filter), false);
        }

        protected void ibtnRibbonView_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?View=View&id={2}&userid={2}&IsDlg=1&Filter={3}", SPContext.Current.Web.Url, Pages.UserItem.PAGE_URL, ItemID, Filter), false);
        }

        protected void lbtnRibbonView_Click(object sender, EventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?View=View&id={2}&userid={2}&IsDlg=1&Filter={3}", SPContext.Current.Web.Url, Pages.UserItem.PAGE_URL, ItemID, Filter), false);
        }

        protected void ibtnRibbonEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?View=Edit&id={2}&userid={2}&IsDlg=1&Filter={3}", SPContext.Current.Web.Url, Pages.UserItem.PAGE_URL, ItemID, Filter), false);
        }

        protected void lbtnRibbonEdit_Click(object sender, EventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?View=Edit&id={2}&userid={2}&IsDlg=1&Filter={3}", SPContext.Current.Web.Url, Pages.UserItem.PAGE_URL, ItemID, Filter), false);
        }

        protected void ibtnRibbonSave_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            SaveItem();
            Response.Redirect(string.Format("{0}/{1}?Filter={2}", SPContext.Current.Web.Url, Pages.Users.PAGE_URL, Filter), false);
        }

        protected void lbtnRibbonSave_Click(object sender, EventArgs e) {
            SaveItem();
            Response.Redirect(string.Format("{0}/{1}?Filter={2}", SPContext.Current.Web.Url, Pages.Users.PAGE_URL, Filter), false);
        }

        protected void ibtnRibbonDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            DeleteItem();
            Response.Redirect(string.Format("{0}/{1}?Filter={2}", SPContext.Current.Web.Url, Pages.Users.PAGE_URL, Filter), false);
        }

        protected void lbtnRibbonDelete_Click(object sender, EventArgs e) {
            DeleteItem();
            Response.Redirect(string.Format("{0}/{1}?Filter={2}", SPContext.Current.Web.Url, Pages.Users.PAGE_URL, Filter), false);
        }

        protected void ibtnRibbonCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?Filter={2}", SPContext.Current.Web.Url, Pages.Users.PAGE_URL, Filter), false);
        }

        protected void lbtnRibbonCancel_Click(object sender, EventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?Filter={2}", SPContext.Current.Web.Url, Pages.Users.PAGE_URL, Filter), false);
        }

        #endregion
    }
}
