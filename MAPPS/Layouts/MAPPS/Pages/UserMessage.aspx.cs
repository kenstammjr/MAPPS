using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Administration;

namespace MAPPS.Pages {
    public partial class UserMessage : UnsecuredLayoutsPageBase {

        protected override bool AllowAnonymousAccess {
            get {
                return true;
            }
        }

        #region _Constants_
        protected const string PAGE_TITLE = "User Message";
        protected const string PAGE_DESCRIPTION = "User information board";
        public const string PAGE_URL = "_layouts/MAPPS/Pages/usermessage.aspx";
        protected const string DEFAULT_MESSAGE = "<H3>Application Error.</H3><li>MAPPS application encountered an error.</li><li>Please notify the System Administrator if this error persists.</li><br><br><li><i>Application Error 202</i></li>";
        #endregion

        protected void Page_Load(object sender, EventArgs e) {
            try {
                if (!IsPostBack && !new Framework().IsDatabaseCurrent) {
                    Response.Redirect(this.Web.Url + "/_layouts/mapps/pages/dbutility.aspx", false);
                    return;
                }

                this.Title = PAGE_TITLE;
                int code = (int)Message.Code.Error; // code for default error message
                if (!IsPostBack) {
                    //lblPageTitle.Text = PAGE_TITLE;
                    lblMessage.Text = DEFAULT_MESSAGE;

                    // if application message code is passed to web form
                    if (Request["code"] != null) {
                        //See if the enum was passed
                        if (Enum.IsDefined(typeof(Message.Code), Request["code"])) {
                            code = (int)((Message.Code)Enum.Parse(typeof(Message.Code), Request["code"].ToString(), true));
                        } else { //See if the int code value was passed in
                            int.TryParse(Request["code"], out code);
                        }
                    }

                    // if session message title and if session message is passed to web form
                    if (Session["MsgHeader"] != null && Session["MsgText"] != null) {
                        lblHeader.Text = Session["MsgHeader"].ToString();
                        lblMessage.Text = Session["MsgText"].ToString();
                        Session.Remove("MsgHeader");
                        Session.Remove("MsgText");
                    } else {
                        string message = Message.GetAppMessage(code.ToString());
                        if (!string.IsNullOrEmpty(message))
                            lblMessage.Text = message;

                        if (Session["MsgText"] != null) {
                            lblMessage.Text += "<br><br><li><i>" + Session["MsgText"].ToString() + "</i></li>";
                            Session.Remove("MsgText");
                        }
                    }
                }

            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (SPFarm.Local.CurrentUserIsAdministrator(true))
                    lblErrorMessage.Visible = true;
                lblErrorMessage.Text = ex.ToString();
            }
        }

    }
}
