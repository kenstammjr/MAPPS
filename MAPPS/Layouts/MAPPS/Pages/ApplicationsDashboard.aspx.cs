using System;
using System.Data;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;

namespace MAPPS.Pages {
    public partial class ApplicationsDashboard : PageBase {

        protected const string PAGE_TITLE = "Applications Dashboard";
        protected const string PAGE_DESCRIPTION = "MEPCOM Application Status Monitoring";
        public const string PAGE_URL = BASE_PAGES_URL + "ApplicationsDashboard.aspx";


        #region _Methods_
        protected override void Page_Load(object sender, EventArgs e) {
            try {
                base.Page_Load(sender, e);
                SetupContribute();
                if (!IsPostBack) {
                    Fill();
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
                DataSet ds = MAPPS.Application.Items();
                DataTable dt = ds.Tables[0];
                int cnt = dt.Rows.Count;
                int third = cnt / 3;

                DataColumn colStr = new DataColumn();
                colStr.DataType = Type.GetType("System.String");

                DataTable dtLeft = new DataTable();
                DataTable dtRight = new DataTable();
                DataTable dtCenter = new DataTable();

                colStr = new DataColumn("Name");
                colStr.AllowDBNull = true;
                dtLeft.Columns.Add(colStr);

                colStr = new DataColumn("Name");
                colStr.AllowDBNull = true;
                dtRight.Columns.Add(colStr);

                colStr = new DataColumn("Name");
                colStr.AllowDBNull = true;
                dtCenter.Columns.Add(colStr);

                colStr = new DataColumn("Status");
                colStr.AllowDBNull = true;
                dtLeft.Columns.Add(colStr);

                colStr = new DataColumn("Status");
                colStr.AllowDBNull = true;
                dtRight.Columns.Add(colStr);

                colStr = new DataColumn("Status");
                colStr.AllowDBNull = true;
                dtCenter.Columns.Add(colStr);

                int i = 0;
                foreach (DataRow dataRow in dt.Rows) {
                    if (i < third) {
                        DataRow dr = dtLeft.NewRow();
                        dr["Name"] = dataRow["Name"];
                        dtLeft.Rows.Add(dr);
                        
                    }
                    if (i > third && i < third * 2) {
                        DataRow dr = dtCenter.NewRow();
                        dr["Name"] = dataRow["Name"];
                        dtCenter.Rows.Add(dr);
                        
                    }
                    if (i > third * 2) {
                        DataRow dr = dtRight.NewRow();
                        dr["Name"] = dataRow["Name"];
                        dtRight.Rows.Add(dr);
                        
                    }
                    i++;
                }

                rptLeft.DataSource = dtLeft;
                rptLeft.DataBind();
                rptCenter.DataSource = dtCenter;
                rptCenter.DataBind();
                rptRight.DataSource = dtRight;
                rptRight.DataBind();
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }


        #endregion _Methods_

        #region _Events_

        #endregion

    }
}
