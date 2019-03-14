using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace MAPPS.WebParts {
    [ToolboxItemAttribute(false)]
    public class ServerName : WebPart {
        Label lblServerName = new Label();
        protected override void CreateChildControls() {
            this.Title = "Server Name";
            this.ChromeType = PartChromeType.None;
            lblServerName.Text = System.Environment.MachineName;
            this.Controls.Add(lblServerName);
        }
    }
}
