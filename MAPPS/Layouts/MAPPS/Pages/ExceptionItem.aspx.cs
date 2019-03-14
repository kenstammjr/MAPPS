using System;
using System.Data;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using System.Collections.Generic;

namespace MAPPS.Pages {
    public partial class ExceptionItem : PageBase {
        protected const string PAGE_TITLE = "Exception";
        protected const string PAGE_DESCRIPTION = "View the details of an application exception";
        public const string PAGE_URL = BASE_PAGES_URL + "ExceptionItem.aspx";

        protected string jsActionCancel;

        private MAPPS.Error item;

        #region _Methods_

        protected override void Page_Load(object sender, EventArgs e) {
            try {
                base.Page_Load(sender, e);

                SetupContribute();
                jsActionCancel = string.Format("rbnCancelClick('{0}/{1}'); return false;", SPContext.Current.Web.Url, Pages.Exceptions.PAGE_URL);
                btnCancel.OnClientClick = jsActionCancel;

                if (!IsPostBack) {
                    Fill();
                } else {
                    HandlePostBack();
                }
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }
        public virtual string GetItemsURL(bool AsLink) {
            string s = string.Empty;
            try {
                string url = string.Format("{0}/{1}", SPContext.Current.Web.Url, Pages.Exceptions.PAGE_URL);
                s = (AsLink) ? string.Format("<a href=\"{0}\" class=\"mapps-breadcrumb\">Exceptions</a>", url) : url;
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
            }
            return s;
        }

        protected override void AddRibbonTab() {
            try {
                switch (IView) {
                    case ItemView.View:
                        RibbonItem("Ribbon.MAPPS.Commit.Cancel.Click", jsActionCancel, true);
                        break;
                }
                base.AddRibbonTab();
                Ribbon.Minimized = true;
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }

        protected override void AddTabEvents() {
            try {
                switch (IView) {
                    case ItemView.View:
                        RibbonItem("Ribbon.MAPPS.Commit.Cancel.Click", jsActionCancel, true);
                        break;
                  }
                base.AddTabEvents();
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }

        private void SetupContribute() {

            if (IsManager) {
                item = (ItemID == 0) ? new MAPPS.Error() : new MAPPS.Error(ItemID);
            } else {
                Response.Redirect(string.Format("{0}/{1}?code={2}", SPContext.Current.Web.Url, Message.URL_USERMESSAGE, Message.Code.MngrAccessReq), false);

            }
        }

        private void HandlePostBack() {
            try {
                Page.Validate();
                if (Page.IsValid) {
                    switch (this.Page.Request["__EVENTTARGET"]) {
                        case RIBBON_POSTBACK_EDIT_EVENT:
                            Response.Redirect(QueryStringParameter(CurrentURL, Request.QueryString, "View", "Edit"), false);
                            break;
                        case RIBBON_POSTBACK_VIEW_EVENT:
                            Response.Redirect(QueryStringParameter(CurrentURL, Request.QueryString, "View", "View"), false);
                            break;
                    }
                } else {
                    Script("resizeModalDialog('True');");
                }
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }


        protected override void Fill() {
            try {
                bool isView = (IView == ItemView.View);
                bool isNew = (IView == ItemView.New);
                lblReqMsg.Visible = !isView;

                this.lblObjectClassNameView.Text = item.Class;
                this.lblClassMethodView.Text = item.Method;
                this.lblRecordIDView.Text = item.RecordID;
                this.lblUserNameView.Text = item.UserName;
                this.lblServerNameView.Text = item.ServerName;
                this.lblExceptionMessageView.Text = item.ExceptionMessage;
                this.lblExceptionTypeView.Text = item.ExceptionType;
                this.lblExceptionSourceView.Text = item.ExceptionSource;
                this.lblExceptionStackTraceView.Text = item.ExceptionStackTrace;
                this.lblCommentView.Text = item.Comment;
                this.lblDateOccurredView.Text = item.DateOccurredLocal.ToString();
                btnCancel.Text = isView ? "Close" : "Cancel";
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }

        #endregion _Methods_

        protected void btnCancel_Click(object sender, EventArgs e) {
            Response.Redirect(string.Format("{0}/{1}", SPContext.Current.Web.Url, Pages.Exceptions.PAGE_URL), false);

        }
    }
}
