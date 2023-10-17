using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceExecute
{
    class WordProcess
    {
        internal static void zoomFactor(DevExpress.XtraRichEdit.RichEditControl txtDescription)
        {
            try
            {
                if (txtDescription != null)
                {
                    float zoom = 0;
                    if (txtDescription.Document.Sections[0].Page.Landscape)
                        //zoom = (float)(txtDescription.Width - 50) / (txtDescription.Document.Sections[0].Page.Height / 3);
                        zoom = (float)(txtDescription.Width) / (txtDescription.Document.Sections[0].Page.Height / 3);
                    else
                        //zoom = (float)(txtDescription.Width - 50) / (txtDescription.Document.Sections[0].Page.Width / 3);
                        zoom = (float)(txtDescription.Width) / (txtDescription.Document.Sections[0].Page.Width / 3);
                    txtDescription.ActiveView.ZoomFactor = zoom;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
