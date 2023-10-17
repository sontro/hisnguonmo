using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ExamServiceReqExecute.Validate.ValidateRule
{
    class NextTreatmentInstValidationRuleControl : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtNextTreatmentInstructionCode;
        internal DevExpress.XtraEditors.TextEdit txtMainText;
        internal DevExpress.XtraEditors.GridLookUpEdit btnBenhChinh;
        internal DevExpress.XtraEditors.CheckEdit chkCheck;
        internal int? maxLengthCode;
        internal int? maxLengthText;
        internal bool IsObligatoryTranferMediOrg;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtNextTreatmentInstructionCode == null
                    || btnBenhChinh == null
                    || txtMainText == null
                    || chkCheck == null)
                    return valid;

                if (maxLengthCode != null)
                {
                    if (Inventec.Common.String.CheckString.IsOverMaxLengthUTF8(txtNextTreatmentInstructionCode.Text.Trim(), maxLengthCode ?? 0))
                    {
                        this.ErrorText = "Mã bệnh chính vượt quá ký tự cho phép";
                        return valid;
                    }
                }

                if (!String.IsNullOrEmpty(txtNextTreatmentInstructionCode.ErrorText)
                    && txtNextTreatmentInstructionCode.ErrorText != HIS.Desktop.Plugins.ExamServiceReqExecute.Resources.ResourceMessage.TruongDuLieuBatBuoc)
                {
                    this.ErrorText = HIS.Desktop.Plugins.ExamServiceReqExecute.Resources.ResourceMessage.NextTreatmentInstructionKhongDung;
                    return valid;
                }


                if (chkCheck.Checked)
                {
                    if (maxLengthText != null)
                    {
                        if (Inventec.Common.String.CheckString.IsOverMaxLengthUTF8(txtMainText.Text.Trim(), maxLengthText ?? 0))
                        {
                            this.ErrorText = "Tên bệnh chính vượt quá ký tự cho phép";
                            return valid;
                        }
                    }
                    if (IsObligatoryTranferMediOrg)
                    {
                        if (string.IsNullOrEmpty(txtMainText.Text.Trim()))
                            return valid;
                    }
                    else if (string.IsNullOrEmpty(txtNextTreatmentInstructionCode.Text.Trim())
                        || string.IsNullOrEmpty(txtMainText.Text.Trim()))
                        return valid;
                }
                else
                {
                    if (string.IsNullOrEmpty(txtNextTreatmentInstructionCode.Text.Trim())
                        || btnBenhChinh.EditValue == null)
                        return valid;
                }
                valid = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }
    }
}
