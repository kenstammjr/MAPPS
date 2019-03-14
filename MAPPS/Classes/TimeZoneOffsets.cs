using Microsoft.SharePoint.Utilities;
using System.Web;
using System.Xml.Linq;
using System.Xml.XPath;

namespace MAPPS {
    public class TimeZoneOffset {

        #region _Private Variables_

        private int _ID;
        private string _Offset;
        private string _Letter;
        private string _ErrorMessage;

        #endregion

        #region _Public Properties_

        public int ID {
            get {
                return _ID;
            }
        }
        public string Offset {
            get {
                return _Offset;
            }
            set {
                _Offset = value;
            }
        }
        public string Letter {
            get {
                return _Letter;
            }
            set {
                _Letter = value;
            }
        }
        public string ErrorMessage {
            get {
                return _ErrorMessage;
            }
        }

        #endregion

        #region _Constructors_

        public TimeZoneOffset() {
            _ID = 0;
            _Offset = string.Empty;
            _Letter = string.Empty;
            _ErrorMessage = string.Empty;
        }
        public TimeZoneOffset(string Offset)
            : this() {
            string xPath = string.Format("//TimeZone[@Offset='{0}']", Offset);
            XDocument xP = TimeZoneOffets();
            XElement xe = xP.XPathSelectElement(xPath);
            if (xe != null) {
                _ID = int.Parse(xe.Attribute("ID").Value);
                _Offset = xe.Attribute("Offset").Value;
                _Letter = xe.Attribute("Letter").Value;
                _ErrorMessage = string.Empty;
            }
        }

        #endregion

        #region _Private Methods_

        private static XDocument TimeZoneOffets() {
            string path = Common.GetResource("TimeZoneOffsets.xml");
            XDocument xDoc = XDocument.Load(path);
            return xDoc;
        }

        #endregion

    }
}