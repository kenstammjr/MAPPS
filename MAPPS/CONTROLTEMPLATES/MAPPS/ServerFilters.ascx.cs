using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace MAPPS.CONTROLTEMPLATES {
    public partial class ServerFilters : UserControl {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                ddlServerFunction.DataSource = MAPPS.ServerFunction.Items();
                ddlServerFunction.DataTextField = "Name";
                ddlServerFunction.DataValueField = "ID";
                ddlServerFunction.DataBind();
                ddlServerFunction.Items.Insert(0, new ListItem("Any", "0"));

                ddlServerType.DataSource = MAPPS.ServerType.Items();
                ddlServerType.DataTextField = "Name";
                ddlServerType.DataValueField = "ID";
                ddlServerType.DataBind();
                ddlServerType.Items.Insert(0, new ListItem("Any", "0"));

                ddlServerStatus.DataSource = MAPPS.ServerStatus.Items();
                ddlServerStatus.DataTextField = "Name";
                ddlServerStatus.DataValueField = "ID";
                ddlServerStatus.DataBind();
                ddlServerStatus.Items.Insert(0, new ListItem("Any", "0"));

                ddlServerVersion.DataSource = MAPPS.ServerVersion.Items();
                ddlServerVersion.DataTextField = "Name";
                ddlServerVersion.DataValueField = "ID";
                ddlServerVersion.DataBind();
                ddlServerVersion.Items.Insert(0, new ListItem("Any", "0"));

            }
        }


        protected void btnApply_Click(object sender, EventArgs e) {

        }

        protected void btnReset_Click(object sender, EventArgs e) {

        }
    }
}

