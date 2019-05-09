using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace MAPPS.CONTROLTEMPLATES {
    public partial class ServerCertificates : ServerControlBase {
        protected override void Page_Load(object sender, EventArgs e) {
            try {
                if (!IsPostBack) {
                    ReadParameters();
                    Fill();
                    btnDelete.OnClientClick = "return confirm('Are you sure you want to delete this item?');";
                }
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }
        protected override void Fill() {
            try {
                tblList.Visible = true;
                tblItem.Visible = false;

                // determine the user identity
                MAPPS.User user = new MAPPS.User(Context.User.Identity.Name);
                UserID = user.ID;

                // determine user role
                if (user.InRole("Manager")) {
                    // do something
                }

                // determine if new drive was clicked from server item ribbon
                if (Request["addcert"] != null) {
                    hfItemID.Value = "0";
                    btnDelete.Visible = false;
                    FillItem("Edit", 0);
                }

                DataView dv = new DataView(Certificate.Items(ServerID).Tables[0]);
                gvData.EmptyDataText = "No certificates to display";
                gvData.DataSource = dv;
                gvData.DataBind();
                gvData.Width = Unit.Percentage(50);

            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }

        }
        private void FillItem(string Mode, int ID) {
            tblList.Visible = false;
            tblItem.Visible = true;
            ddlCertificate.Items.Clear();
            ddlCertificate.DataSource = MAPPS.Certificate.Items();
            ddlCertificate.DataTextField = "Name";
            ddlCertificate.DataValueField = "ID";
            ddlCertificate.DataBind();
            ddlCertificate.Items.Insert(0, new ListItem("Choose", "0"));

            ServerCertificate item = new ServerCertificate(ID);
            try {
                bool isView = (Mode == "View");
                bool isNew = (ID == 0);
                btnDelete.Visible = !isView && ID != 0;

                ddlCertificate.SelectedIndex = -1;
                try { ddlCertificate.Items.FindByValue(item.CertificateID.ToString()).Selected = true; } catch { }
                ddlCertificate.Visible = !isView;
                lblNameView.Text = new Certificate(item.CertificateID).Name;
                lblNameView.Visible = isView;
                lblNameRequired.Visible = !isView;

                lblCreatedInfo.Text = string.Format("Created at {0} by {1}", MAPPS.Common.ConvertUTCToWebLocalTime(SPContext.Current.Web, item.CreatedOn), item.CreatedBy);
                lblCreatedInfo.Visible = (item.ID != 0);
                lblUpdatedInfo.Text = string.Format("Last modified at {0} by {1}", MAPPS.Common.ConvertUTCToWebLocalTime(SPContext.Current.Web, item.ModifiedOn), item.ModifiedBy);
                lblUpdatedInfo.Visible = (item.ID != 0);

                btnSave.Visible = !isView;
                btnCancel.Text = isView ? "Close" : "Cancel";
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }

        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e) {
            try {
                if (e.CommandName == "ViewItem") {
                    hfItemID.Value = e.CommandArgument.ToString();
                    FillItem("View", int.Parse(hfItemID.Value));
                }
                if (e.CommandName == "EditItem") {
                    hfItemID.Value = e.CommandArgument.ToString();
                    FillItem("Edit", int.Parse(hfItemID.Value));
                }
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }
        protected bool PassedValidation() {
            bool bContinue = true;

            lblNameView.Text = "";
            lblNameView.CssClass = "";
            lblNameView.Visible = false;
            if (ddlCertificate.SelectedIndex < 1) {
                lblNameView.Text = Message.REQUIRED_FIELD;
                lblNameView.CssClass = "ms-formvalidation";
                lblNameView.Visible = true;
                bContinue = false;
            }
            return bContinue;
        }
        private void SaveItem() {
            bool success = false;
            try {
                if (PassedValidation()) {
                    ServerCertificate item = new ServerCertificate(int.Parse(hfItemID.Value));
                    MAPPS.User user = new MAPPS.User(Context.User.Identity.Name);
                    item.ServerID = ServerID;
                    item.CertificateID = int.Parse(ddlCertificate.SelectedValue);

                    item.ModifiedBy = user.UserName;
                    if (item.ID == 0) {
                        item.CreatedBy = item.ModifiedBy;
                        if (item.Insert())
                            success = true;
                    } else {
                        if (item.Update())
                            success = true;
                    }
                    if (success)
                        Fill();
                } else {
                    // validation failed
                }
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
            Response.Redirect(string.Format("{0}/{1}?View=Edit&ID={2}&ServerID={2}&IsDlg=1Filter={3}", SPContext.Current.Web.Url, Pages.ServerItem.PAGE_URL, ServerID, Filter), false);

        }
        private void DeleteItem() {
            bool success = false;
            try {
                ServerCertificate item = new ServerCertificate(int.Parse(hfItemID.Value));
                if (item.Delete()) {
                    success = true;
                    hfItemID.Value = "0";
                }
                if (success)
                    Fill();
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
            Response.Redirect(string.Format("{0}/{1}?View=Edit&ID={2}&ServerID={2}&IsDlg=1Filter={3}", SPContext.Current.Web.Url, Pages.ServerItem.PAGE_URL, ServerID, Filter), false);

        }
        protected void lbtnNewItem_Click(object sender, EventArgs e) {
            hfItemID.Value = "0";
            FillItem("Edit", 0);
        }
        protected void lbtnNew_Click(object sender, EventArgs e) {
            hfItemID.Value = "0";
            FillItem("Edit", 0);
        }

        protected void lbtnEdit_Click(object sender, EventArgs e) {
            FillItem("Edit", int.Parse(hfItemID.Value));
        }

        protected void lbtnView_Click(object sender, EventArgs e) {
            FillItem("View", int.Parse(hfItemID.Value));
        }

        protected void lbtnDelete_Click(object sender, EventArgs e) {
            DeleteItem();
        }

        protected void btnSave_Click(object sender, EventArgs e) {
            SaveItem();
        }

        protected void btnCancel_Click(object sender, EventArgs e) {
            Response.Redirect(string.Format("{0}/{1}?View=Edit&ID={2}&ServerID={2}&IsDlg=1Filter={3}", SPContext.Current.Web.Url, Pages.ServerItem.PAGE_URL, ServerID, Filter), false);

        }

        protected void btnDelete_Click(object sender, EventArgs e) {
            DeleteItem();

        }
    }
}
