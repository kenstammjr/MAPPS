using Microsoft.Office.Server.UserProfiles;
using Microsoft.SharePoint;

namespace MAPPS {
    public class SPCommon {

        public static string GetUserProfilePictureURL(string UserName) {
            string url = string.Empty;
            SPServiceContext serviceContext = SPServiceContext.GetContext(SPContext.Current.Site);
            UserProfileManager upm = new UserProfileManager(serviceContext);
            if (upm.UserExists(UserName)) {
                Microsoft.Office.Server.UserProfiles.UserProfile profile = upm.GetUserProfile(UserName);
                if (profile != null) {
                    url = profile["PictureURL"].Value.ToString().Replace("MThumb", "LThumb");
                    //Action.Write(profile.ID.ToString(), UserName);
                    //Action.Write(profile.PersonalUrl.ToString().ToString(), UserName);
                    //Action.Write(profile.PersonalUrl.ToString().ToString(), UserName);
                    //Action.Write(profile.PublicUrl.ToString().ToString(), UserName);
                    //Action.Write(profile.RecordId.ToString().ToString(), UserName);
                    //Action.Write(profile.DisplayName.ToString().ToString(), UserName);
                } else {
                    //Action.Write("profile is null", UserName);
                }
            }
            // change to default if user does not have an image specified
            if (string.IsNullOrEmpty(url))
                url = "/_layouts/15/images/mapps/PHOTO-NOT-AVAILABLE.png";
            return url;

        }
    }
}
