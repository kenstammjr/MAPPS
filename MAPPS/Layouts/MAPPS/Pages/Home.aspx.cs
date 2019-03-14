using System;
using System.Data;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;

namespace MAPPS.Pages {
    public partial class Home : PageBase {

        protected const string PAGE_TITLE = "MAPPS";
        protected const string PAGE_DESCRIPTION = "MEPCOM Applications";
        public const string PAGE_URL = BASE_PAGES_URL + "Home.aspx";


        #region _Methods_
        protected override void Page_Load(object sender, EventArgs e) {
            try {
                base.Page_Load(sender, e);
                SetupContribute();
                if (!IsPostBack) {
                    //Fill();
                }
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }

        private void SetupContribute() {

        }

        protected override void AddRibbonTab() {
            try {
                base.AddRibbonTab();
                Ribbon.CommandUIVisible = false;
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }

        protected override void Fill() {
            try {
                DataTable dt = Module.Items();
                foreach (DataRow dr in dt.Rows) {
                    dr["URL"] = SPContext.Current.Web.Url + dr["URL"].ToString();
                    dr["ImageURL"] = SPContext.Current.Web.Url + dr["ImageURL"].ToString();
                }
                string rowFilter = "IsActive = 1";
                string sort = "DisplayIndex ASC, Name ASC";

                Table tbl = new Table();
                tbl.Width = Unit.Percentage(100);
                if (dt.Rows.Count > 0) {
                    foreach (DataRow dr in dt.Select(rowFilter, sort)) {
                        tbl.Controls.Add(ModuleRow(dr));
                    }
                    this.Controls.Add(tbl);
                } else {
                    Label lbl = new Label();
                    lbl.CssClass = "ms-vb";
                    if (dt.Rows.Count == 0)
                        lbl.Text = "There are currently no SIS modules to display.<br>Modules will be displayed once they are installed and configured.";
                    else
                        lbl.Text = "There are currently no active SIS modules to display.<br>Modules will be displayed once they are installed and configured.";
                    this.Controls.Add(lbl);
                }
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }

        private TableRow ModuleRow(DataRow dr) {
            TableRow tr = new TableRow();
            try {
                string moduleTitle = dr["Name"].ToString();
                string moduleDescription = dr["Description"].ToString();
                string moduleURL = dr["URL"].ToString();
                string moduleImageURL = dr["ImageURL"].ToString();
                string moduleVersion = dr["DBVersion"].ToString();
                string moduleAdminURL = dr["AdminURL"].ToString();

                TableCell tcImage = new TableCell();
                tcImage.Width = Unit.Pixel(5);
                tcImage.Attributes.Add("padding-right", "10px");

                ImageButton ibtn = new ImageButton();
                ibtn.ImageUrl = moduleImageURL;
                ibtn.ToolTip = moduleDescription;
                ibtn.PostBackUrl = moduleURL;
                tcImage.Controls.Add(ibtn);
                tr.Controls.Add(tcImage);

                TableCell tcSpacer = new TableCell();
                tr.Controls.Add(tcSpacer);

                TableCell tcModule = new TableCell();
                tcModule.Width = Unit.Percentage(100);
                tcModule.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
                HyperLink lnk = new HyperLink();
                HyperLink lnkAdmin = new HyperLink();
                Label lblDescription = new Label();
                Label lblVersion = new Label();

                lnk.Text = moduleTitle;
                lnk.NavigateUrl = moduleURL;
                lnk.ToolTip = moduleDescription;
                lnk.CssClass = "ms-vb";
                lnk.Font.Bold = true;
                tcModule.Controls.Add(lnk);

                lblVersion.Text = " " + moduleVersion;
                lblVersion.CssClass = "ms-vb";
                lblVersion.Font.Italic = true;
                tcModule.Controls.Add(lblVersion);

                lblDescription.Text = "<br>" + moduleDescription;
                lblDescription.CssClass = "ms-vb";
                tcModule.Controls.Add(lblDescription);
                if (IsAdmin) {
                    lnkAdmin.Text = "<br>[Administration]";
                    lnkAdmin.NavigateUrl = moduleAdminURL;
                    lnkAdmin.ToolTip = "Module Administration";
                    lnkAdmin.CssClass = "ms-vb";
                    lnkAdmin.Font.Bold = false;
                    tcModule.Controls.Add(lnkAdmin);
                }
                tr.Controls.Add(tcModule);
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
            return tr;
        }
        #endregion _Methods_

        #region _Events_

        #endregion

    }
}
