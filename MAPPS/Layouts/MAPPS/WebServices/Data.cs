using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace MAPPS.WebServices {
    [WebService(Namespace = "http://mapps.mepcom.army.mil/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class Data : PageBase {
        [WebMethod]
        public string UserTest() {
            CurrentUser = new MAPPS.User(Context.User.Identity.Name);
            return "The Web Service Works for: " + CurrentUser.UserName;
        }
        [WebMethod]
        public List<TreeViewNode> Nodes(string root)
        {
            CurrentUser = new MAPPS.User(Context.User.Identity.Name);
            bool IsAdmin = CurrentUser.InRole(Role.RoleType.Administrator.ToString());

            // on first load, root will be 0. on subsequent requests, root will be the id of the selected tree node
            int nodeId = (root == "source") ? 0 : Int32.Parse(root.ToString());
            int menuId = int.Parse(HttpContext.Current.Request.QueryString["menuid"].ToString());

            // everything
            string URL = HttpContext.Current.Request.Url.ToString();
            string RawURL = HttpContext.Current.Request.RawUrl;
            RawURL = RawURL.Replace("/~", "");
            URL = URL.Replace(RawURL, "");

            DataSet dsItems = new DataSet();
            MenuNode node = null;
            // on first load, get root (parent) nodes. On subsequet requests, get children nodes
            dsItems = nodeId == 0 ? MenuNode.Items(menuId, nodeId, true) : MenuNode.Items(menuId, nodeId, true);

            List<TreeViewNode> nodes = new List<TreeViewNode>();
            foreach (DataRow child in dsItems.Tables[0].Rows)
            {
                node = new MenuNode(int.Parse(child["ID"].ToString()));
                bool leaf = !node.HasChildren;
                string editLink = string.Format("{0}/_layouts/mapps/pages/menunodeitem.aspx?View=Edit&ID={1}&MenuID={2}&ParentID={3}", URL, node.ID.ToString(), menuId.ToString(), node.ParentID.ToString());
                string treeText = string.Format("<a href=\"{0}\" class=\"navmenulink\" title=\"{1}\" onmouseover=\"mopen('n{2}')\" onmouseout=\"mclosetime()\">{3}</a>&nbsp;&nbsp;<a id=\"n{4}\" title='Click to report as a bad link' onmouseover=\"mcancelclosetime()\" onmouseout=\"mclosetime()\" class=\"navmenunodeedit\" href=\"javascript:ModalOpen('{5}', 'Report Node', true);\">[report]</a>", node.URL, node.Description, node.ID.ToString(), node.Name, node.ID.ToString(), editLink);
                if (IsAdmin)
                    treeText = string.Format("<a href=\"{0}\" class=\"navmenulink\" title=\"{1}\" onmouseover=\"mopen('n{2}')\" onmouseout=\"mclosetime()\">{3}</a>&nbsp;&nbsp;<a id=\"n{4}\" title='Edit item' onmouseover=\"mcancelclosetime()\" onmouseout=\"mclosetime()\" class=\"navmenunodeedit\" href=\"javascript:ModalOpen('{5}', 'Edit Node', true);\">[edit]</a>", node.URL, node.Description, node.ID.ToString(), node.Name, node.ID.ToString(), editLink);
                nodes.Add(new TreeViewNode()
                {
                    id = node.ID.ToString(),
                    url = node.URL,
                    text = treeText,
                    hasChildren = node.HasChildren,
                    target = node.Target,
                    classes = leaf ? "file" : "folder",
                });
            }
            return nodes;
        }
        [WebMethod]
        public bool DeleteMenuAdmin(string ID)
        {
            bool success = false;
            bool IsAnonymous = false;
            if (Context.User.Identity.Name != null)
            {
                CurrentUser = new MAPPS.User(Context.User.Identity.Name);
                CurrentUserWithDomain = CurrentUser.UserName;
            }
            else
            {
                IsAnonymous = true;
            }
            if (!IsAnonymous)
            {
                MenuAdmin item = new MenuAdmin(Int32.Parse(ID));
                if (item.ID != 0)
                {
                    if (item.Delete())
                        success = true;
                }
                else
                {
                    if (item.Delete())
                        success = true;
                }
            }
            return success;
        }
        protected override void Fill() { }
    }
    public class TreeViewNode
    {

        #region Properties

        public string id { get; set; }
        public string text { get; set; }
        public bool expanded { get; set; }
        public bool hasChildren { get; set; }
        public string classes { get; set; }
        public string url { get; set; }
        public string target { get; set; }
        public string name { get; set; }
        public TreeViewNode[] children { get; set; }

        #endregion

    }
}
