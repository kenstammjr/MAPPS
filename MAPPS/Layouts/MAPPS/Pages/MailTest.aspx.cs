using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Net.Mail;
using System.Data;
using System.Web;
using System.Xml;
using Microsoft.SharePoint.Administration;

namespace MAPPS.Pages {
    public partial class MailTest : PageBase {

        protected const string PAGE_TITLE = "Mail Test";
        protected const string PAGE_DESCRIPTION = "Opening this page automatically sends a test email message";
        public const string PAGE_URL = BASE_PAGES_URL + "MailTest.aspx";

        protected override void Page_Load(object sender, EventArgs e) {
            try {
                base.Page_Load(sender, e);
                Fill();
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
                SPSecurity.RunWithElevatedPrivileges(delegate () {
                    string msg = string.Empty;
                    msg = string.Format(@"You have received this message because an email test was initiated from the MAPPS SIS application and you are configured to receive admin alerts. <br/>");
                    msg += string.Format(@"<br/>If you no longer want to receive admin alerts you need to have your email address removed from the application setting [AdminAlerts].<br/>");

                    string[] emailAcct = Setting.KeyValue("AdminAlerts").Split(new char[] { ';' });

                    SmtpClient client = null;
                    string smtpHost = string.Empty;
                    smtpHost = SPAdministrationWebApplication.Local.OutboundMailServiceInstance.Server.Name;
                    MailMessage email = new MailMessage();
                    email.From = new MailAddress("mapps.noreply@mail.mil");
                    email.Subject = "MAPPS SIS Email Functionality Test";
                    email.IsBodyHtml = true;
                    email.Body = msg;
                    client = new SmtpClient(smtpHost);
                    for (int i = 0; i < emailAcct.Length; i++) {
                        email.To.Add(emailAcct[i].Trim());
                    }
                    client.Send(email);
                    lblMessage.Text = "Test Email Message <font color='green'>SUCCESSFULLY</font> sent to all addresses identified in the application setting of AdminAlerts";
                });
            } catch (Exception ex) {
                MAPPS.Error.WriteError(ex);
                lblMessage.Text = "Test Email Message <font color='red'>FAILED</font> to send. Check Error Log! Verify the app setting \"AdminAlerts have valid email addresses and are delimited with semi colons!\" ";
            }
        }
    }
}
