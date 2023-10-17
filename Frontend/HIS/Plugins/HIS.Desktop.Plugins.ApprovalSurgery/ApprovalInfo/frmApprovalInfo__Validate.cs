using ACS.EFMODEL.DataModels;
using DevExpress.XtraEditors.DXErrorProvider;
using HIS.Desktop.Plugins.ApprovalSurgery.ApprovalInfo.ValidationRule;
using Inventec.Common.Controls.EditorLoader;
using Inventec.Desktop.Common.Controls.ValidationRule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ApprovalSurgery.ApprovalInfo
{
    
    public partial class frmApprovalInfo : Form
    {
        private void ValidateControl()
        {
            try
            {
                ValidationLoginname();
                ValidateTimeApproval();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ValidateTimeApproval()
        {
            try
            {
                ApprovalTimeValidationRule ruleMain = new ApprovalTimeValidationRule();
                ruleMain.time = dtTime;
                ruleMain.action = action;
                ruleMain.ErrorType = ErrorType.Warning;
                this.dxValidationProvider1.SetValidationRule(dtTime, ruleMain);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
			
        }

        private void ValidationLoginname()
        {
            try
            {
                GridLookupEditWithTextEditValidationRule ruleMain = new GridLookupEditWithTextEditValidationRule();
                ruleMain.txtTextEdit = txtLoginname;
                ruleMain.cbo = cboLoginname;
                ruleMain.ErrorText = Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.HeThongTBTruongDuLieuBatBuocPhaiNhap);
                ruleMain.ErrorType = ErrorType.Warning;
                this.dxValidationProvider1.SetValidationRule(txtLoginname, ruleMain);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
