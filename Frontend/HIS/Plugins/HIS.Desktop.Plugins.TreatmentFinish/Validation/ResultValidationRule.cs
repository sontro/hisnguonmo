using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentFinish.Validation
{
    class ResultValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtKetQuaDieuTri;
        internal DevExpress.XtraEditors.GridLookUpEdit cboKetQuaDieuTri;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtKetQuaDieuTri == null || cboKetQuaDieuTri == null) return valid;
                if (String.IsNullOrEmpty(txtKetQuaDieuTri.Text) || cboKetQuaDieuTri.EditValue == null)
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
