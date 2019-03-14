using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MAPPS.ControlTemplates {
    public partial class Tabs : System.Web.UI.UserControl {
        protected void Page_Load(object sender, EventArgs e) {
            try {
                MAPPS.User user = new MAPPS.User(Context.User.Identity.Name);
                bool IsAdmin = user.InRole(RoleType.Administrator.ToString());
                DataSet ds = new DataSet();
                if (IsAdmin)
                    ds = Tab.ActiveItems(true);
                else
                    ds = Tab.ActiveItems(false);

                DataView dv = new DataView();
                dv.Table = ds.Tables[0];
                dv.Sort = "DisplayIndex";
                dv.RowFilter = "ParentID = 0";
                foreach (DataRowView drv in dv) {
                    MenuItem menuItem = new MenuItem();
                    menuItem.Text = drv["Name"].ToString();
                    menuItem.Value = drv["ID"].ToString();
                    menuItem.NavigateUrl = drv["URL"].ToString();
                    this.TopNavigationMenu.Items.Add(menuItem);
                    this.TopNavigationMenu.Attributes.Add("padding-right", "15px");
                    AddChildItems(ds.Tables[0], menuItem);
                }
                this.TopNavigationMenu.Visible = true;
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
        }
        private void AddChildItems(DataTable menuData, MenuItem parentMenuItem) {
            DataView view = new DataView(menuData);
            view.RowFilter = "ParentID = " + parentMenuItem.Value;

            foreach (DataRowView rowView in view) {
                MenuItem childItem = new MenuItem();
                childItem.Text = rowView["Name"].ToString();
                childItem.Value = rowView["ID"].ToString();
                childItem.NavigateUrl = rowView["URL"].ToString();
                parentMenuItem.ChildItems.Add(childItem);
                AddChildItems(menuData, childItem);
            }
        }
        protected void TopNavigationMenu_MenuItemDataBound(object sender, MenuEventArgs e) {

        }
    }
}