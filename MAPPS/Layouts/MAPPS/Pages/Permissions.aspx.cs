using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MAPPS.Pages {
    public partial class Permissions : PageBase {

        protected const string PAGE_TITLE = "Permissions";
        protected const string PAGE_DESCRIPTION = "View and Manage Application Permissions";
        public const string PAGE_URL = BASE_PAGES_URL + "Permissions.aspx";
        protected override void Page_Load(object sender, EventArgs e) {
            try {
                base.Page_Load(sender, e);
                if (!IsPostBack) {
                    if (Session["URLReferrer"] == null) {
                        Session["URLReferrer"] = Request.UrlReferrer.ToString();
                    }
                    Fill();
                    lbxAppGroups.SelectedIndex = 0;
                    FillAppSecurityGroupMembers(Int32.Parse(lbxAppGroups.SelectedValue));
                }
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }

        protected override void Fill() {
            FillAppSecurityGroups();
        }

        #region _ Group Type Selections _

        protected void lbtnAppGroups_Click(object sender, EventArgs e) {
            txtAppSearch.Text = string.Empty;
            lbxAppUserLookup.Items.Clear();
            FillAppSecurityGroups();
            lbxAppGroups.SelectedIndex = 0;
            FillAppSecurityGroupMembers(Int32.Parse(lbxAppGroups.SelectedValue));
            trAppGroupsListView.Visible = true;
        }

        #endregion

        #region _ Application Security Groups _

        private void FillAppSecurityGroups() {
            try {
                lbxAppGroups.Items.Clear();
                DataSet ds = SecurityGroup.Items();
                DataView dv = new DataView(ds.Tables[0]);
                dv.Sort = "DisplayIndex, Name";
                lbxAppGroups.DataSource = dv;
                lbxAppGroups.DataTextField = "Name";
                lbxAppGroups.DataValueField = "ID";
                lbxAppGroups.DataBind();
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }
        private void FillAppSecurityGroupMembers(int SecurityGroupID) {
            try {
                lbxAppGroupMembers.Items.Clear();
                DataSet ds = SecurityGroupMembership.Items(SecurityGroupID);
                DataView dv = new DataView(ds.Tables[0]);
                dv.Sort = "UserName ASC";
                lbxAppGroupMembers.DataSource = dv;
                lbxAppGroupMembers.DataTextField = "UserName";
                lbxAppGroupMembers.DataValueField = "UserID";
                lbxAppGroupMembers.DataBind();
                if (lbxAppGroupMembers.Items.Count > 0)
                    btnRemFromAppGroup.Enabled = true;
                else
                    btnRemFromAppGroup.Enabled = false;

            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }
        protected void btnRemFromAppGroup_Click(object sender, EventArgs e) {
            try {
                // get role name associated with selected security group
                SecurityGroup group = new SecurityGroup(Int32.Parse(lbxAppGroups.SelectedValue));
                Role role = new Role(group.RoleID);
                // evaluate if user may administor members of security group based on role
                User currentUser = new User(Context.User.Identity.Name);

                if (currentUser.InRole(role.Name)) {
                    foreach (ListItem li in lbxAppGroupMembers.Items) {
                        if (li.Selected) {
                            // delete individual membership
                            SecurityGroupMembership membership = new SecurityGroupMembership(Int32.Parse(lbxAppGroups.SelectedValue), int.Parse(li.Value));
                            if (!membership.Delete()) {
                                Transaction xAction = new Transaction();
                                xAction.Action = "Failed to delete " + new User(Int32.Parse(li.Value)).UserName + " from security group " + new SecurityGroup(Int32.Parse(lbxAppGroups.SelectedValue)).Name;
                                xAction.Category = "Application Administration";
                                xAction.Type = Transaction.TYPE_FAILURE;
                                xAction.CreatedBy = currentUser.UserName;
                                xAction.Insert();
                            } else {
                                Transaction xAction = new Transaction();
                                xAction.Action = "Successfully deleted " + new User(Int32.Parse(li.Value)).UserName + " from security group " + new SecurityGroup(Int32.Parse(lbxAppGroups.SelectedValue)).Name;
                                xAction.Category = "Application Administration";
                                xAction.Type = Transaction.TYPE_SUCCESS;
                                xAction.CreatedBy = currentUser.UserName;
                                xAction.Insert();
                            }
                        }
                    }
                    FillAppSecurityGroupMembers(Int32.Parse(lbxAppGroups.SelectedValue));
                } else {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You do not have permission to remove members from this security group!');", true);
                }
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }
        protected void btnAddToAppGroup_Click(object sender, EventArgs e) {
            try {
                // get role name associated with selected security group
                SecurityGroup Group = new SecurityGroup(Int32.Parse(lbxAppGroups.SelectedValue));
                Role role = new Role(Group.RoleID);
                // evaluate if user may administor members of security group based on role
                User userProfile = new User(Context.User.Identity.Name);
                if (userProfile.InRole(role.Name)) {
                    foreach (ListItem li in lbxAppUserLookup.Items) {
                        if (li.Selected) {
                            // add individual membership
                            SecurityGroupMembership membership = new SecurityGroupMembership();
                            membership.SecurityGroupID = Int32.Parse(lbxAppGroups.SelectedValue);
                            membership.UserID = int.Parse(li.Value);
                            membership.CreatedBy = userProfile.UserName.StripClaim();
                            membership.ModifiedBy = userProfile.UserName.StripClaim();

                            if (!membership.Insert()) {
                                Transaction xAction = new Transaction();
                                xAction.Action = string.Format("Failed to add {0} to security group {1}", new User(Int32.Parse(li.Value)).UserName, new SecurityGroup(Int32.Parse(lbxAppGroups.SelectedValue)).Name);
                                xAction.Category = "Application Administration";
                                xAction.Type = Transaction.TYPE_FAILURE;
                                xAction.CreatedBy = userProfile.UserName;
                                xAction.Insert();
                            }
                            else {
                                Transaction xAction = new Transaction();
                                xAction.Action = string.Format("Successfully added {0} to security group {1}", new User(Int32.Parse(li.Value)).UserName, new SecurityGroup(Int32.Parse(lbxAppGroups.SelectedValue)).Name);
                                xAction.Category = "Application Administration";
                                xAction.Type = Transaction.TYPE_SUCCESS;
                                xAction.CreatedBy = userProfile.UserName;
                                xAction.Insert();
                            }
                        }
                    }
                    FillAppSecurityGroupMembers(Int32.Parse(lbxAppGroups.SelectedValue));
                } else {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You do not have permission to add members to this security group!');", true);
                }
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }
        protected void ibtnAppSearch_Click(object sender, ImageClickEventArgs e) {
            if (txtAppSearch.Text.Trim().Length > 0) {
                string searchText = txtAppSearch.Text.Trim();
                //string filter = "UserName LIKE '%" + SearchText + "%'";
                try {
                    //DataTable dt = MAPPS.User.All(0, searchText);
                    DataTable dt = MAPPS.User.Items(searchText).Tables[0]; //.All(0, searchText);
                    //dt.DefaultView.RowFilter = filter;
                    dt.DefaultView.Sort = "UserName ASC";
                    lbxAppUserLookup.DataSource = dt;
                    lbxAppUserLookup.DataTextField = "UserName";
                    lbxAppUserLookup.DataValueField = "ID";
                    lbxAppUserLookup.DataBind();
                } catch (Exception ex) {
                    MAPPS.Error.WriteError(ex);
                    if (ShowDebug)
                        lblErrorMessage.Text = ex.ToString();
                }
            }
            if (lbxAppUserLookup.Items.Count > 0) {
                btnAddToAppGroup.Enabled = true;
            } else {
                btnAddToAppGroup.Enabled = false;
            }
        }
        protected void lbxAppGroups_SelectedIndexChanged(object sender, EventArgs e) {
            FillAppSecurityGroupMembers(Int32.Parse(lbxAppGroups.SelectedValue));
        }
        protected void btnAppClose_Click(object sender, EventArgs e) {
            string returnTo = Session["URLReferrer"].ToString();
            Session.Remove("URLReferrer");
            Response.Redirect(returnTo, false);
        }

        #endregion

  

        private void HideViews() {
            trAppGroupsListView.Visible = false;
        }

    }
}
