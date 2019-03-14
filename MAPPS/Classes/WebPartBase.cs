using System.Web.UI;
using System.Web.UI.WebControls.WebParts;

namespace MAPPS.WebParts {
    public class WebPartBase : WebPart {

        #region Properties

        private bool _ShowFolding = false;
        private bool _InitCollapsed = false;

        public bool ShowFolding {
            get {
                return _ShowFolding;
            }
            set {
                _ShowFolding = value;
            }
        }
        public bool InitCollapsed {
            get {
                return _InitCollapsed;
            }
            set {
                _InitCollapsed = value;
            }
        }
        protected bool ShowDebug {
            get {
                if (ViewState["ShowDebug"] == null) {
                    ViewState["ShowDebug"] = MAPPS.Setting.KeyValue("ShowDebug");
                    ViewState["ShowDebug"] = ((string)ViewState["ShowDebug"] == string.Empty) ? false : ViewState["ShowDebug"];
                }
                return bool.Parse(ViewState["ShowDebug"].ToString());
            }
        }

        protected override void CreateChildControls() {
            string s = string.Empty;
            s += string.Format("<link rel=\"stylesheet\" type=\"text/css\" href=\"/_layouts/15/mapps/styles/jsou.css?v={0}\" />\r\n", Framework.RequiredVersion);
            s += string.Format("<script type=\"text/javascript\" src=\"/_layouts/15/mapps/scripts/mapps.js?v={0}\"></script>\r\n", Framework.RequiredVersion);
            this.Controls.Add(new LiteralControl(s));

        }

        #endregion
    }

}
