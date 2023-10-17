using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TYT.Desktop.Plugins.FetusAbortionList
{
    public partial class UCTYTFetusAbortionList : HIS.Desktop.Utility.UserControlBase
    {
        public void FindShortcut()
        {
            try
            {
                if (btnFind.Enabled)
                {
                    btnFind_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
			
        }

        public void RefeshShortcut()
        {
            try
            {
                if (btnRefesh.Enabled)
                {
                    btnRefesh_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

        }
    }
}
