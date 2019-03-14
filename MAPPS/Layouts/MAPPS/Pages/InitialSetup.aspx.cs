using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Utilities;
using System;
using System.Data;
using System.DirectoryServices;
using System.Web;
using System.Xml;
using Microsoft.SharePoint.WebControls;
using System.Collections.Generic;
using Microsoft.Office.Server.UserProfiles;

namespace MAPPS.Pages {
    public partial class InitialSetup : PageBase {

        protected const string PAGE_TITLE = "Initial Setup";
        protected const string PAGE_DESCRIPTION = "Allows a site collection administrator to configure the application for initial use";
        public const string PAGE_URL = BASE_PAGES_URL + "InitialSetup.aspx";

        protected string jsActionCancel;

        protected override void Page_Load(object sender, EventArgs e) {
            try {
                base.Page_Load(sender, e);
                SetupContribute();
                if (!IsPostBack) {
                    Fill();
                }
            }
            catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }
        protected override void AddRibbonTab() {
            try {
                base.AddRibbonTab();
                Ribbon.CommandUIVisible = false;
            }
            catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }
        private void SetupContribute() {

        }

        protected override void Fill() {

            try {
                if (MAPPS.User.Count() == 0) {
                    UserProfileManager userProfileManager = new UserProfileManager(SPServiceContext.GetContext(SPContext.Current.Site));
                    ProfilePropertyManager profilePropMgr = new UserProfileConfigManager(SPServiceContext.GetContext(SPContext.Current.Site)).ProfilePropertyManager;
                    ProfileSubtypePropertyManager subtypePropMgr = profilePropMgr.GetProfileSubtypeProperties("UserProfile");
                    UserProfile userProfile = userProfileManager.GetUserProfile(Context.User.Identity.Name.Replace("i:0#.w|", "").Replace("0#.w|", ""));
                    hfUserProfileRecordID.Value = (userProfile.RecordId.ToString());

                    IEnumerator<ProfileSubtypeProperty> userProfileSubtypeProperties = subtypePropMgr.GetEnumerator();
                    while (userProfileSubtypeProperties.MoveNext()) {
                        string propName = userProfileSubtypeProperties.Current.Name;
                        ProfileValueCollectionBase values = userProfile.GetProfileValueCollection(propName);
                        if (values.Count > 0) {

                            // Handle multivalue properties.
                            foreach (var value in values) {
                                switch (propName) {
                                    case "AccountName":
                                        lblAccountNameView.Text = value.ToString();
                                        break;
                                    case "FirstName":
                                        lblFirstNameView.Text = value.ToString();
                                        break;
                                    case "LastName":
                                        lblLastNameView.Text = value.ToString();
                                        break;
                                    case "PreferredName":
                                        lblPreferredNameView.Text = value.ToString();
                                        break;
                                    case "UserProfile_GUID":
                                        lblUserProfileGuidView.Text = value.ToString();
                                        break;
                                    case "SPS-UserPrincipalName":
                                        lblUserPrincipalNameView.Text = value.ToString();
                                        break;
                                    case "SPS-DistinguishedName":
                                        lblDistinguishedNameView.Text = value.ToString();
                                        break;
                                }
                                if (ShowDebug)
                                    lblErrorMessage.Text += string.Format("Name: {0} Value: {1}</br>", propName, value.ToString());
                            }
                        }
                    }
                } else {
                    trAccountName.Visible = false;
                    trLastName.Visible = false;
                    trFirstName.Visible = false;
                    trPreferredName.Visible = false;
                    trUserProfile_GUID.Visible = false;
                    trUserPrincipalName.Visible = false;
                    trDistinguishedName.Visible = false;
                    lblMessageView.CssClass = "ms-error";
                    lblMessageView.Text = "There are already users in the data system.  This option is only available when no user records exist.";
                    btnSave.Visible = false;
                }
            }
            catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                lblErrorMessage.Text += string.Format("ERROR: {0}", ex.ToString());
            }
        }

        private bool SaveItem() {
            bool success = false;

            try {
                if (!SPContext.Current.Web.UserIsSiteAdmin) { // if not site collection admin, redirect to message board
                    lblMessageView.CssClass = "ms-error";
                    lblMessageView.Text = "Process Failed! <br> - You must be a site collection administrator to add an initial user to the application";
                    trMessage.Visible = true;
                }
                else {
                    if (SecurityGroupMembership.SecurityGroupMembershipActiveCount("Administrator") == 0) { // if no admins are assigned
                                                                                                            // is current user in the users table?
                        MAPPS.User currentUser = new MAPPS.User(Context.User.Identity.Name);
                        if (currentUser.ID > 0) {
                            // user exist, just assign admin role
                            SecurityGroupMembership membership = new SecurityGroupMembership();
                            membership.UserID = currentUser.ID;
                            membership.SecurityGroupID = 1;
                            if (membership.Insert()) {
                                lblMessageView.Text = "Permission Granted! <br> - Admin permissions have been granted to the existing user account";
                                trMessage.Visible = true;
                            }
                            else {
                                lblMessageView.CssClass = "ms-error";
                                lblMessageView.Text = "Permission Assignment Failed! <br> - Admin permissions could not be granted to the existing user account. Check the application exceptions";
                                trMessage.Visible = true;
                            }
                        }
                        else {
                            // user does not exist, create user record based on information from the user profile service
                            currentUser.UserName = lblAccountNameView.Text;
                            currentUser.LastName = lblLastNameView.Text;
                            currentUser.FirstName = lblFirstNameView.Text;
                            currentUser.PreferredName = lblPreferredNameView.Text;
                            currentUser.SPObjectGuid = lblUserProfileGuidView.Text;
                            currentUser.UserProfileRecordID = Int32.Parse(hfUserProfileRecordID.Value);
                            currentUser.Insert();
                            SecurityGroupMembership membership = new SecurityGroupMembership();
                            membership.UserID = currentUser.ID;
                            membership.SecurityGroupID = 1;
                            if (membership.Insert()) {
                                lblMessageView.Text = "Permission Granted! <br> - Admin permissions have been granted to the new user account";
                                trMessage.Visible = true;
                            }
                            else {
                                lblMessageView.CssClass = "ms-error";
                                lblMessageView.Text = "Permission Assignment Failed! <br> - Admin permissions could not be granted to the new user account. Check the application exceptions";
                                trMessage.Visible = true;
                            }
                        }
                    }
                    else {
                        lblMessageView.CssClass = "ms-error";
                        lblMessageView.Text = "The application already has at least one assigned administrator.  Review assignments by selecting permissions";
                        trMessage.Visible = true;
                    }
                }
            }
            catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
            return success;
        }
        protected void btnSave_Click(object sender, EventArgs e) {
            SaveItem();
        }

        protected void btnCancel_Click(object sender, EventArgs e) {
            Response.Redirect(string.Format("{0}/{1}", SPContext.Current.Web.Url, Pages.Administration.PAGE_URL), false);

        }
    }
}
