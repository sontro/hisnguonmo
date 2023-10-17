using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors.ViewInfo;
using HIS.Desktop.LibraryMessage;
using HIS.Desktop.Plugins.CareCreate.Resources;
using HIS.Desktop.Plugins.CareCreate.Validate.ValidationRule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.CareCreate
{
    public partial class CareCreate : HIS.Desktop.Utility.FormBase
    {
        private void ValidControl()
        {
            try
            {
                ValidExecuteTime();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ValidExecuteTime()
        {
            CareExecuteTime__ValidationRule oDobDateRule = new CareExecuteTime__ValidationRule();
            oDobDateRule.dtExecuteTime = dtExcuteTime;
            oDobDateRule.ErrorText = MessageUtil.GetMessage(LibraryMessage.Message.Enum.NguoiDungNhapNgayKhongHopLe);
            oDobDateRule.ErrorType = ErrorType.Warning;
            this.dxValidationProvider.SetValidationRule(dtExcuteTime, oDobDateRule);
        }

        private void ValidSpinPulse(SpinEdit spinEdit)
        {
            SpinEditValidationRule spin = new SpinEditValidationRule();
            spin.spinEdit = spinEdit;
            spin.ErrorText = ResourceMessage.SpinEdit__Dhst__KhongDuocNhapSoAm;
            spin.ErrorType = ErrorType.Warning;
            this.dxValidationProvider.SetValidationRule(spinEdit, spin);
        }
    }
}
