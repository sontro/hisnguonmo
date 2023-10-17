using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentFinish.Validation
{
    class FinishTypeValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtTinhTrangRaVien;
        internal DevExpress.XtraEditors.GridLookUpEdit cboTinhTrangRaVien;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtTinhTrangRaVien == null || cboTinhTrangRaVien == null) return valid;
                if (String.IsNullOrEmpty(txtTinhTrangRaVien.Text) || cboTinhTrangRaVien.EditValue == null)
                    return valid;
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
