using System;

namespace MAPPS.CONTROLTEMPLATES
{
    public partial class NavigationTree : UserControlBase
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Fill();
                }
            }
            catch (Exception ex)
            {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }
        protected override void Fill()
        {
            try
            {

            }
            catch (Exception ex)
            {
                MAPPS.Error.WriteError(ex);
                if (ShowDebug)
                    lblErrorMessage.Text = ex.ToString();
            }
        }

    }
}
