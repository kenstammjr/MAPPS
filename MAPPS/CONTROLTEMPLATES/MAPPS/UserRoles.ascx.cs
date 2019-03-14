using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace MAPPS.CONTROLTEMPLATES {
    public partial class UserRoles : UserControlBase {
        protected override void Page_Load(object sender, EventArgs e) {
            try {
                if (!IsPostBack) {
                    ReadParameters();
                    Fill();
                }
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }
        protected override void Fill() {
            MAPPS.User user = new MAPPS.User(ItemID);
            lblUserRoles.Text = user.Roles;
        }
    }
}