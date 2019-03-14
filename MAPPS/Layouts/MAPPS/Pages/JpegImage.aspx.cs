using System.Drawing.Imaging;

namespace MAPPS.Pages {
    public partial class JpegImage : System.Web.UI.Page {
        protected void Page_Load(object sender, System.EventArgs e) {
            // Create a CAPTCHA image using the text stored in the Session object.
            CaptchaImage ci = new CaptchaImage(this.Session["CaptchaImageText"].ToString(), 300, 75, "Century Schoolbook");

            // Change the response headers to output a JPEG image.
            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";

            // Write the image to the response stream in JPEG format.
            ci.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);

            // Dispose of the CAPTCHA image object.
            ci.Dispose();
        }
    }
}
