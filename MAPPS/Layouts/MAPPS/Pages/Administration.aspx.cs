using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Data;
using System.Net;
using System.Web;
using System.Xml;

namespace MAPPS.Pages {
    public partial class Administration : PageBase {

        protected const string PAGE_TITLE = "Administration";
        protected const string PAGE_DESCRIPTION = "Administrators Dashboard";
        public const string PAGE_URL = BASE_PAGES_URL + "Administration.aspx";

        #region _Methods_

        protected override void Page_Load(object sender, EventArgs e) {
            try {
                base.Page_Load(sender, e);
                Fill();
                SiteInformation();
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
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
                DataSet ds = FromXML("Administration.xml");
                DataView dvGroups = new DataView(ds.Tables[0]);
                dvGroups.Sort = "Index ASC";
                DataView dvLinks = new DataView(ds.Tables[1]);
                dvLinks.Sort = "Index ASC";

                DataTable dtGroups = dvGroups.ToTable();

                DataColumn colStr = new DataColumn();
                colStr.DataType = Type.GetType("System.String");

                DataTable dtLeft = new DataTable();
                DataTable dtRight = new DataTable();

                colStr = new DataColumn("Name");
                colStr.AllowDBNull = true;
                dtLeft.Columns.Add(colStr);
                colStr = new DataColumn("Name");
                colStr.AllowDBNull = true;
                dtRight.Columns.Add(colStr);

                colStr = new DataColumn("Icon");
                colStr.AllowDBNull = true;
                dtLeft.Columns.Add(colStr);
                colStr = new DataColumn("Icon");
                colStr.AllowDBNull = true;
                dtRight.Columns.Add(colStr);

                colStr = new DataColumn("Links");
                colStr.AllowDBNull = true;
                dtLeft.Columns.Add(colStr);
                colStr = new DataColumn("Links");
                colStr.AllowDBNull = true;
                dtRight.Columns.Add(colStr);

                for (int i = 0; i < dtGroups.Rows.Count; i++) {
                    DataRow dr = (i % 2 == 0) ? dtLeft.NewRow() : dtRight.NewRow();
                    dr["Name"] = dtGroups.Rows[i]["Name"].ToString();
                    dr["Icon"] = dtGroups.Rows[i]["Icon"].ToString();
                    dr["Links"] = BuildLinks(dtGroups.Rows[i]["ID"].ToString(), dvLinks);
                    if (i % 2 == 0)
                        dtLeft.Rows.Add(dr);
                    else
                        dtRight.Rows.Add(dr);
                }

                rptLeft.DataSource = dtLeft;
                rptLeft.DataBind();
                rptRight.DataSource = dtRight;
                rptRight.DataBind();
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }

        private string BuildLinks(string GroupID, DataView Links) {
            string s = string.Empty;
            try {
                Links.RowFilter = "GroupID = '" + GroupID + "'";
                foreach (DataRow dr in Links.ToTable().Rows)
                    s += string.Format("<li style=\"list-style-image: url('/_layouts/15/images/BCCUR.GIF')\"><a href=\"{0}\" class=\"ms-link\">{1}</li>", dr["URL"].ToString(), dr["Name"].ToString());
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
            return s;
        }

        private DataSet FromXML(string filename) {
            DataSet ds = new DataSet();
            string path = string.Empty;
            try {
                path = HttpContext.Current.Request.MapPath(filename);
            } catch {
            }
            XmlDocument xmlDoc = new XmlDocument();
            bool xmlLoad = false;
            try {
                xmlDoc.Load(path);
                xmlLoad = true;
            } catch {
            }
            if (xmlLoad) {
                XmlNodeReader reader = new XmlNodeReader(xmlDoc);

                ds.ReadXml(reader);
                reader.Close();
            }
            return ds;
        }

        private void SiteInformation() {
            try {
                lblAppName.Text = "Framework";
                lblAppInfo.Text = string.Format("<br /><span class=\"ms-vb\">Version: {0}</span>", Framework.RequiredVersion);
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }

        #endregion _Methods_

    }
}
