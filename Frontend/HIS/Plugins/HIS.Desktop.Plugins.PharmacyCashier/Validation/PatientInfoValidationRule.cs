using HIS.Desktop.LibraryMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PharmacyCashier.Validation
{
    class PatientInfoValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.CheckEdit checkIsVisitor;
        internal DevExpress.XtraEditors.TextEdit txtPatientName;
        internal DevExpress.XtraEditors.TextEdit txtPatientCode;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (checkIsVisitor == null || txtPatientCode == null || txtPatientName == null) return valid;
                if (!checkIsVisitor.Checked && (String.IsNullOrEmpty(txtPatientCode.Text) || String.IsNullOrEmpty(txtPatientName.Text)))
                {
                    ErrorText = MessageUtil.GetMessage(LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                    return valid;
                }
                else if (checkIsVisitor.Checked && String.IsNullOrEmpty(txtPatientName.Text))
                {
                    ErrorText = MessageUtil.GetMessage(LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
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
