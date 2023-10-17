using HIS.Desktop.Utility;
using System;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute
{
    public partial class SurgServiceReqExecuteControl : UserControlBase
    {
        public void SaveShortCut()
        {
            try
            {
                if (btnSave.Enabled == true)
                    btnSave_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public void FinishShortCut()
        {
            try
            {
                if (btnFinish.Enabled == true)
                    btnFinish_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
