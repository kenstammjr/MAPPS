using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace MAPPS.CONTROLTEMPLATES {
    public partial class Modules : UserControlBase {

        protected override void Page_Load(object sender, EventArgs e) {
            try {
                if (!IsPostBack) {
                    Fill();
                }
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }
        protected override void Fill() {
            try {
                DataView dv = new DataView(Module.Items());
                dv.Sort = "DisplayIndex";
                dv.RowFilter = "IsActive = 1";
                gvData.DataSource = dv;
                gvData.DataBind();
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }

        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e) {
            try {
                if (e.CommandName == "Select") {
                    string url = e.CommandArgument.ToString();
                    Response.Redirect(string.Format("{0}/{1}", SPContext.Current.Web.Url, url), false);
                }
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }
    }
}
