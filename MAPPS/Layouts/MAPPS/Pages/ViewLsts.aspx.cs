using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections.Generic;
using System.Web;
using Microsoft.Office.Server.UserProfiles;
using System.Data;
using System.Text;
using Microsoft.SharePoint.Utilities;
using System.Web.UI.WebControls;

namespace MAPPS.Pages {
    public partial class ViewLsts : LayoutsPageBase {

        protected const string PAGE_TITLE = "Site Contents";

        DataTable dtContent = new DataTable("SiteContent");
        DataTable dtSubsites = new DataTable("Subsites");

        DropDownList ddl = new DropDownList();

        protected void Page_Load(object sender, EventArgs e) {
            try {
                if (!Page.IsPostBack) {
                    FillContent();
                    FillSites();
                    SPWeb currentSite = SPControl.GetContextWeb(Context);
                    if (!currentSite.DoesUserHavePermissions(SPBasePermissions.ManageSubwebs)) {
                        newSubsite.Visible = false;
                    }
                    if (!currentSite.DoesUserHavePermissions(SPBasePermissions.ManageLists)) {
                        newapp.Visible = false;
                    }
                    lblMessage.Text += "<br>Manage Web: " + currentSite.DoesUserHavePermissions(SPBasePermissions.ManageWeb).ToString();
                    lblMessage.Text += "<br>Manage Lists: " + currentSite.DoesUserHavePermissions(SPBasePermissions.ManageLists).ToString();
                    lblMessage.Text += "<br>Manage Subwebs: " + currentSite.DoesUserHavePermissions(SPBasePermissions.ManageSubwebs).ToString();

                    lblRecycleBinCount.Text = string.Format("({0})", currentSite.RecycleBin.Count.ToString());
                }
            } catch (Exception ex) {
                lblMessage.Text += ex.ToString();
            }
        }

        private void FillContent() {

            DefineContentDataTable();
            foreach (SPList list in SPContext.Current.Web.Site.RootWeb.Lists) {
                if (!list.Hidden) {
                    DataRow dr = dtContent.NewRow();
                    dr["Title"] = list.Title;
                    dr["Description"] = list.Description;
                    dr["ItemCount"] = list.ItemCount;
                    dr["ImageURL"] = list.ImageUrl;
                    dr["DefaultViewURL"] = list.DefaultViewUrl;
                    dr["BaseType"] = list.BaseType.ToString();
                    dr["EntityTypeName"] = list.EntityTypeName.ToString();
                    dr["Hidden"] = list.Hidden;
                    dr["LastModified"] = list.LastItemModifiedDate;
                    dr["CreatedOn"] = list.Created;
                    dr["Temp"] = list.BaseTemplate.ToString();
                    dtContent.Rows.Add(dr);
                }
            }
            DataView dvContent = dtContent.DefaultView;
            dvContent.Sort = "Title";
            if (ddlBaseType.SelectedIndex > 0) {
                dvContent.RowFilter = string.Format("BaseType = '{0}'", ddlBaseType.SelectedValue);
            }
            gvData.DataSource = dvContent;
            gvData.DataBind();
        }
        private void FillSites() {
            DefineSubsitesTable();
            SPWeb currentSite = SPControl.GetContextWeb(Context);
            SPWebCollection subSites = currentSite.GetSubwebsForCurrentUser();
            foreach (SPWeb site in subSites) {
                DataRow dr = dtSubsites.NewRow();
                dr["Title"] = site.Title;
                dr["Description"] = site.Description;
                dr["URL"] = site.Url;
                dtSubsites.Rows.Add(dr);
            }
            gvSubsites.DataSource = dtSubsites;
            gvSubsites.DataBind();
        }
        private void DefineContentDataTable() {

            DataColumn colInt = new DataColumn();
            colInt.DataType = Type.GetType("System.Int32");
            DataColumn colString = new DataColumn();
            colString.DataType = Type.GetType("System.String");
            DataColumn colDateTime = new DataColumn();
            colDateTime.DataType = Type.GetType("System.DateTime");
            DataColumn colBool = new DataColumn();
            colBool.DataType = Type.GetType("System.Boolean");


            DataColumn colKey = new DataColumn("ID");
            colKey.AutoIncrement = true;
            colKey.AutoIncrementSeed = 1;
            colKey.AutoIncrementStep = 1;
            colKey.DataType = Type.GetType("System.Int32");
            dtContent.Columns.Add(colKey);

            colString = new DataColumn("Title");
            colString.AllowDBNull = true;
            dtContent.Columns.Add(colString);

            colString = new DataColumn("Description");
            colString.AllowDBNull = true;
            dtContent.Columns.Add(colString);

            colInt = new DataColumn("ItemCount");
            colInt.AllowDBNull = true;
            dtContent.Columns.Add(colInt);

            colString = new DataColumn("ImageURL");
            colString.AllowDBNull = true;
            dtContent.Columns.Add(colString);

            colString = new DataColumn("DefaultViewURL");
            colString.AllowDBNull = true;
            dtContent.Columns.Add(colString);

            colString = new DataColumn("BaseType");
            colString.AllowDBNull = true;
            dtContent.Columns.Add(colString);

            colString = new DataColumn("EntityTypeName");
            colString.AllowDBNull = true;
            dtContent.Columns.Add(colString);

            colString = new DataColumn("Temp");
            colString.AllowDBNull = true;
            dtContent.Columns.Add(colString);

            colBool = new DataColumn("Hidden");
            colBool.AllowDBNull = true;
            dtContent.Columns.Add(colBool);

            colDateTime = new DataColumn("CreatedOn");
            colDateTime.AllowDBNull = true;
            dtContent.Columns.Add(colDateTime);

            colDateTime = new DataColumn("LastModified");
            colDateTime.AllowDBNull = true;
            dtContent.Columns.Add(colDateTime);
        }

        private void DefineSubsitesTable() {

            DataColumn colInt = new DataColumn();
            colInt.DataType = Type.GetType("System.Int32");
            DataColumn colString = new DataColumn();
            colString.DataType = Type.GetType("System.String");
            DataColumn colDateTime = new DataColumn();
            colDateTime.DataType = Type.GetType("System.DateTime");
            DataColumn colBool = new DataColumn();
            colBool.DataType = Type.GetType("System.Boolean");

            DataColumn colKey = new DataColumn("ID");
            colKey.AutoIncrement = true;
            colKey.AutoIncrementSeed = 1;
            colKey.AutoIncrementStep = 1;
            colKey.DataType = Type.GetType("System.Int32");
            dtSubsites.Columns.Add(colKey);

            colString = new DataColumn("Title");
            colString.AllowDBNull = true;
            dtSubsites.Columns.Add(colString);

            colString = new DataColumn("Description");
            colString.AllowDBNull = true;
            dtSubsites.Columns.Add(colString);

            colString = new DataColumn("URL");
            colString.AllowDBNull = true;
            dtSubsites.Columns.Add(colString);
        }

        protected void gvData_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e) {
            try {
                switch (e.Row.RowType) {
                    case DataControlRowType.DataRow:

                        Label lblItemID = (Label)e.Row.FindControl("lblItemID");
                        int itemID = int.Parse(lblItemID.Text);

                        Label lblTitle = (Label)e.Row.FindControl("lblTitle");
                        Label lblNew = (Label)e.Row.FindControl("lblNew");
                        Label lblLastModified = (Label)e.Row.FindControl("lblLastModified");

                        DateTime dt = DateTime.Parse(lblNew.Text);
                        lblNew.Text = ((DateTime.Now - dt).TotalDays < 2) ? "<img src= \"/_layouts/15/images/jsosu/new.gif\" alt=\"New\" border=\"0\" />" : string.Empty;

                        lblLastModified.Text = MAPPS.Common.ConvertUTCToWebLocalTime(SPContext.Current.Web, DateTime.Parse(lblLastModified.Text)).ToString("dd-MMM-yyyy HHmm");

                        break;
                    case DataControlRowType.Header:
                        //SetupSort(gvData, e);
                        break;
                }
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
        }

        protected void gvData_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e) {

        }

        protected void gvData_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e) {
            if (e.CommandName == "Redirect") {
                Response.Redirect(String.Format("{0}/{1}", SPContext.Current.Web.Url, e.CommandArgument.ToString()));

            }
        }

        protected void gvSubsites_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e) {
            if (e.CommandName == "Redirect") {
                Response.Redirect(e.CommandArgument.ToString());

            }
        }

        protected void ddlBaseType_SelectedIndexChanged(object sender, EventArgs e) {
            FillContent();
        }
    }
}
