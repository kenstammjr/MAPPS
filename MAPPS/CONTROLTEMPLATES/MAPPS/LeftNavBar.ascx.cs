using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web;
using System.Xml;

namespace MAPPS {
    public partial class LeftNavBar : UserControlBase {
        protected override void Page_Load(object sender, EventArgs e) {
            base.Page_Load(sender, e);
            DataSet ds = FromXML("LeftNavBar.xml");
            DataView dvNav = new DataView(ds.Tables[0]);
            dvNav.Sort = "ID ASC";
            DataView dvUserNav = new DataView(ds.Tables[1]);
            dvUserNav.Sort = "ID ASC";

            DataColumn colStr = new DataColumn();
            colStr.DataType = System.Type.GetType("System.String");

            DataTable dtNav = new DataTable();
            DataTable dtUserNav = new DataTable();

            colStr = new DataColumn("Name");
            colStr.AllowDBNull = true;
            dtNav.Columns.Add(colStr);
            colStr = new DataColumn("Name");
            colStr.AllowDBNull = true;
            dtUserNav.Columns.Add(colStr);

            colStr = new DataColumn("Tab");
            colStr.AllowDBNull = true;
            dtNav.Columns.Add(colStr);
            colStr = new DataColumn("Tab");
            colStr.AllowDBNull = true;
            dtUserNav.Columns.Add(colStr);

            colStr = new DataColumn("URL");
            colStr.AllowDBNull = true;
            dtNav.Columns.Add(colStr);
            colStr = new DataColumn("URL");
            colStr.AllowDBNull = true;
            dtUserNav.Columns.Add(colStr);

            colStr = new DataColumn("Image");
            colStr.AllowDBNull = true;
            dtNav.Columns.Add(colStr);
            colStr = new DataColumn("Image");
            colStr.AllowDBNull = true;
            dtUserNav.Columns.Add(colStr);

            colStr = new DataColumn("CssClass");
            colStr.AllowDBNull = true;
            dtNav.Columns.Add(colStr);
            colStr = new DataColumn("CssClass");
            colStr.AllowDBNull = true;
            dtUserNav.Columns.Add(colStr);


            // add data to table 1

            for (int i = 0; i < dvNav.Table.Rows.Count; i++) {
                DataRow dr = dtNav.NewRow();
                string name = dvNav.Table.Rows[i]["Name"].ToString();
                string url = dvNav.Table.Rows[i]["URL"].ToString();
                dr["Name"] = name;
                //url = (name == "About") ? string.Format("javascript:openModalDialog('About Meeting Scheduler','{0}');", AboutURL) : url;
                dr["URL"] = url;
                dr["Image"] = dvNav.Table.Rows[i]["Image"].ToString();
                dr["CssClass"] = ((Tabs)Enum.Parse(typeof(Tabs), dvNav.Table.Rows[i]["Tab"].ToString()) == Tab) ? "active" : string.Empty;
                dtNav.Rows.Add(dr);
            }

            lvNav.DataSource = dtNav;
            lvNav.DataBind();



            // add data to table 2

            if (IsAnonymous) {
                pnlNavUser.Visible = false;
            } else {
                for (int i = 0; i < dvUserNav.Table.Rows.Count; i++) {
                    string name = dvUserNav.Table.Rows[i]["Name"].ToString();
                    if (name != "Administration" || IsManager) {
                        DataRow dr = dtUserNav.NewRow();
                        dr["Name"] = name;
                        dr["URL"] = (name == "Administration") ? string.Format("{0}/{1}", SPContext.Current.Web.Url, Pages.Administration.PAGE_URL) : dvUserNav.Table.Rows[i]["URL"].ToString();
                        dr["Image"] = dvUserNav.Table.Rows[i]["Image"].ToString();
                        dr["CssClass"] = ((Tabs)Enum.Parse(typeof(Tabs), dvUserNav.Table.Rows[i]["Tab"].ToString()) == Tab) ? "active" : string.Empty;
                        dtUserNav.Rows.Add(dr);
                    }
                }

                lvNavUser.DataSource = dtUserNav;
                lvNavUser.DataBind();
            }
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
        protected override void Fill() { }
    }
}
