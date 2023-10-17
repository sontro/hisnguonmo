using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestSaleCreate.Validation
{
    class PatientValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.CheckEdit checkIsVisitor;
        internal DevExpress.XtraEditors.TextEdit txtVirPatientName;
        internal DevExpress.XtraEditors.TextEdit txtPatientCode;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (checkIsVisitor == null || txtPatientCode == null || txtVirPatientName == null) return valid;
                if (!checkIsVisitor.Checked && (String.IsNullOrEmpty(txtPatientCode.Text) || String.IsNullOrEmpty(txtVirPatientName.Text)))
                {
                    ErrorText = Base.ResourceMessageLang.TruongDuLieuBatBuoc;
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                    return valid;
                }
                else if (checkIsVisitor.Checked && String.IsNullOrEmpty(txtVirPatientName.Text))
                {
                    ErrorText = Base.ResourceMessageLang.TruongDuLieuBatBuoc;
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
