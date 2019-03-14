using System;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Web.Services;
using System.Xml;

namespace SNAPPS.WebServices {
    /// <summary>
    /// Returns current time for ZoneIDs
    /// </summary>
    [WebService(Namespace = "http://sof.socnorth.socom.mil/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    public class Time : WebService {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public XmlDocument GetTime(List<string> ZoneIDs) {
            XmlDocument result = new XmlDocument();
            XmlElement root = result.CreateElement("Data");
            result.AppendChild(root);

            foreach (string zone in ZoneIDs) {
                XmlElement element = result.CreateElement("Time");
                element.SetAttribute("ZoneID", zone);
                element.SetAttribute("Value", TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, zone).ToString("HH:mm"));
                root.AppendChild(element);
            }

            return result;
        }
    }
}