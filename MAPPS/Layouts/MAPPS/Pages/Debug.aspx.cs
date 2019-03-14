using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections.Generic;
using System.Web;
using Microsoft.Office.Server.UserProfiles;
using System.Net;

namespace MAPPS.Pages {
    public partial class Debug : PageBase {
        #region _Constants_

        protected const string PAGE_TITLE = "Debug";
        protected const string PAGE_DESCRIPTION = "Application Debugging Information";
        public const string PAGE_URL = BASE_PAGES_URL + "Debug.aspx";

        #endregion _Constants_

        #region _Global_

        protected string rbnNewItemVisible = "false";
        protected string rbnSaveItemVisible = "false";
        protected string rbnCancelVisible = "false";
        protected string rbnEditItemVisible = "false";
        protected string rbnDeleteItemVisible = "false";
        protected string rbnAlertMeVisible = "false";
        protected List<string> valuesToNote;

        #endregion _Global_

        #region _Private Variables_



        #endregion _Private Variables_

        protected override void Page_Load(object sender, EventArgs e) {

            Fill();
            BindDebugInfoList();
            if (!IsPostBack) {
                this.Session["CaptchaImageText"] = MAPPS.Common.GenerateRandomText(6, true);
            }
        }

        /// <summary>
        /// Builds and adds values we need
        /// </summary>
        protected override void Fill() {
            valuesToNote = new List<string>();

            valuesToNote.Add("DateTime.Now = " + DateTime.Now);
            valuesToNote.Add("DateTime.UtcNow = " + DateTime.UtcNow);
            valuesToNote.Add("SPContext.Current.Web.Url = " + SPContext.Current.Web.Url);
            valuesToNote.Add("SPContext.Current.Site.Url = " + SPContext.Current.Site.Url);
            valuesToNote.Add("ServerName = " + System.Environment.MachineName);

            HttpContext context = HttpContext.Current;
            valuesToNote.Add("HttpContext.Current.Request.Url.Scheme = " + context.Request.Url.Scheme);
            valuesToNote.Add("HttpContext.Current.Request.Url.Host = " + context.Request.Url.Host);
            valuesToNote.Add("HttpContext.Current.Request.ApplicationPath = " + context.Request.ApplicationPath);
            valuesToNote.Add("HttpContext.Current.Request.Browser.Version = " + context.Request.Browser.Version);
            valuesToNote.Add("HttpContext.Current.Request.Browser.Browser = " + context.Request.Browser.Browser);
            valuesToNote.Add("HttpContext.Current.Request.RawUrl = " + context.Request.RawUrl);
            valuesToNote.Add("HttpContext.Current.Request.AbsoluteUri = " + context.Request.Url.AbsoluteUri);
            valuesToNote.Add("HttpContext.Current.Request.AbsolutePath = " + context.Request.Url.AbsolutePath);
            valuesToNote.Add("HttpContext.Current.User.Identity.Name = " + HttpContext.Current.User.Identity.Name);

            string CleanIdentity = HttpContext.Current.User.Identity.Name.Replace("i:0#.w|", "").Replace("0#.w|", "");
            valuesToNote.Add("Clean Identity Name from HttpContext.Current.User.Identity.Name = " + CleanIdentity);

            valuesToNote.Add("SPContext.Current.Web.CurrentUser.LoginName = " + SPContext.Current.Web.CurrentUser.LoginName);
            valuesToNote.Add("SPContext.Current.Web.CurrentUser.Email = " + SPContext.Current.Web.CurrentUser.Email);
            valuesToNote.Add("SPContext.Current.Web.CurrentUser.ID = " + SPContext.Current.Web.CurrentUser.ID);
            valuesToNote.Add("SPContext.Current.Web.CurrentUser.Name = " + SPContext.Current.Web.CurrentUser.Name);
            valuesToNote.Add("SPContext.Current.Web.CurrentUser.Sid = " + SPContext.Current.Web.CurrentUser.Sid);

            try {
                valuesToNote.Add("--- MAPPS User Object ---");
                MAPPS.User currentUser = new MAPPS.User(Context.User.Identity.Name);
                valuesToNote.Add("User.ID = " + currentUser.ID.ToString());
                valuesToNote.Add("User.ADUserID = " + currentUser.ADObjectGuid);
                valuesToNote.Add("User.UserName = " + currentUser.UserName);
                valuesToNote.Add("User.Roles = " + currentUser.Roles);
            }
            catch (Exception ex) {
                valuesToNote.Add("Error Occcured: " + ex.Message);
            }

            //Exception error = new Exception();
            //MAPPS.Error.WriteError("Debug Class", "Debug Method", error);
            //valuesToNote.Add("debug error = " + error.Message);

            //Person Picker results will be at the bottom
            if (spePickUser.ResolvedEntities.Count > 0) {
                foreach (PickerEntity entity in spePickUser.ResolvedEntities) {
                    valuesToNote.Add("entity.DisplayText = " + entity.DisplayText);
                    valuesToNote.Add("entity.Description = " + entity.Description);
                    valuesToNote.Add("entity.EntityType = " + entity.EntityType);
                    valuesToNote.Add("entity.IsResolved.ToString() = " + entity.IsResolved.ToString());
                    valuesToNote.Add("entity.Key = " + entity.Key);
                    valuesToNote.Add("entity.ProviderName = " + entity.ProviderName);
                    valuesToNote.Add("entity.Claim.Value = " + entity.Claim.Value);
                    valuesToNote.Add("entity.Claim.Value.Substring(0, entity.Claim.Value.LastIndexOf(@\"\\\")) = " + entity.Claim.Value.Substring(0, entity.Claim.Value.LastIndexOf(@"\")));
                    valuesToNote.Add("entity.Claim.Value.Substring((entity.Claim.Value.LastIndexOf(@\"\\\") + 1))) = " + entity.Claim.Value.Substring((entity.Claim.Value.LastIndexOf(@"\") + 1)));

                    try {
                        UserProfileManager userProfileManager = new UserProfileManager(SPServiceContext.GetContext(SPContext.Current.Site));
                        valuesToNote.Add("----");
                        valuesToNote.Add("userProfileManager.GlobalPersonalSitesList = " + userProfileManager.GlobalPersonalSitesList);
                        valuesToNote.Add("userProfileManager.IsClaimProvider = " + userProfileManager.IsClaimProvider.ToString());
                        try { valuesToNote.Add("userProfileManager.Count = " + userProfileManager.Count.ToString()); }
                        catch (Exception ex) { valuesToNote.Add("Error Occcured: " + ex.Message); }
                        try { valuesToNote.Add("userProfileManager.GlobalPersonalSitesList.Items.Count = " + userProfileManager.GlobalPersonalSitesList.Items.Count); }
                        catch (Exception ex) { valuesToNote.Add("Error Occcured: " + ex.Message); }
                        try { valuesToNote.Add("userProfileManager.UserExists(entity.Claim.Value) = " + userProfileManager.UserExists(entity.Claim.Value).ToString()); }
                        catch (Exception ex) { valuesToNote.Add("Error Occcured: " + ex.Message); }
                        try {
                            Microsoft.Office.Server.UserProfiles.UserProfile userProfile = userProfileManager.GetUserProfile(entity.Claim.Value);
                            valuesToNote.Add("------");
                            valuesToNote.Add("userProfile.DisplayName = " + userProfile.DisplayName);
                            valuesToNote.Add("userProfile.ID = " + userProfile.ID);
                            valuesToNote.Add("userProfile.ProfileType = " + userProfile.ProfileType);
                            valuesToNote.Add("userProfile.Properties.CountProperties = " + userProfile.Properties.CountProperties);
                            try {
                                valuesToNote.Add("userProfile[\"ADGuid\"].Value.GetType().ToString()" + userProfile["ADGuid"].Value.GetType().ToString());
                                valuesToNote.Add("userProfile[\"ADGuid\"].Value.ToString()" + userProfile["ADGuid"].Value.ToString());
                                if (userProfile["ADGuid"].Value.GetType().ToString() == "System.Byte[]") {
                                    valuesToNote.Add("(new Guid((byte[])userProfile[\"ADGuid\"].Value)).ToString() = " + (new Guid((byte[])userProfile["ADGuid"].Value)).ToString());
                                }
                            }
                            catch (Exception ex) {
                                valuesToNote.Add("Error Occcured: " + ex.Message);
                            }

                            try {
                                valuesToNote.Add("userProfile[\"WorkPhone\"].Value.ToString() = " + userProfile["WorkPhone"].Value.ToString());
                                valuesToNote.Add("userProfile[\"FirstName\"].Value.ToString() = " + userProfile["FirstName"].Value.ToString());
                                valuesToNote.Add("userProfile[\"LastName\"].Value.ToString() = " + userProfile["LastName"].Value.ToString());
                                valuesToNote.Add("userProfile[\"Office\"].Value.ToString() = " + userProfile["Office"].Value.ToString());
                                valuesToNote.Add("userProfile[\"WorkEmail\"].Value.ToString() = " + userProfile["WorkEmail"].Value.ToString());
                            }
                            catch (Exception ex) {
                                valuesToNote.Add("Error Occcured: " + ex.Message);
                            }
                        }
                        catch (Exception ex) {
                            valuesToNote.Add("Error Occcured: " + ex.Message);
                        }
                    }
                    catch (Exception ex) {
                        valuesToNote.Add("Error Occcured: " + ex.Message);
                    }//End of user profile manager checks

                }
            }
        }


        private void BindDebugInfoList() {
            bltlDebugData.DataSource = valuesToNote;
            bltlDebugData.DataBind();
        }
    }
}
