using HIS.Desktop.Controls.Session;
using HIS.Desktop.Utility;
using System;

namespace HIS.Desktop.Plugins.ExamServiceReqExecute
{
    public partial class ExamServiceReqExecuteControl : UserControlBase
    {
        public void FinishShortCut()
        {
            try
            {
                btnFinish_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public void SaveFinishShortCut()
        {
            try
            {
                if (btnSaveFinish.Enabled == true)
                    btnSaveFinish_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public void FocusIcd()
        {
            try
            {
                if (txtIcdCode.Enabled)
				{
                    txtIcdCode.Focus();
                    txtIcdCode.SelectAll();
				}                    
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public void SaveShortCut()
        {
            try
            {
                btnSave_Click(null, null);
                if (!IsValidForSave)
                    return;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public void AssignService()
        {
            try
            {
                btnAssignService_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public void AssignPre()
        {
            try
            {
                btnAssignPre_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public void TreatmentFinish()
        {
            try
            {
                //btnTreatmentFinish_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public void CloseTab()
        {
            try
            {
                HIS.Desktop.ModuleExt.TabControlBaseProcess.CloseSelectedTabPage(SessionManager.GetTabControlMain());
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
			
        }
    }
}
