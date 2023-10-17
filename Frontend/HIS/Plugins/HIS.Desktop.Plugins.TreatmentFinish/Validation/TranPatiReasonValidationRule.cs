using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.TreatmentFinish.Validation
{
    class TranPatiReasonValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtTranPatiReason;
        internal DevExpress.XtraEditors.GridLookUpEdit cboTranPatiReason;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtTranPatiReason == null || cboTranPatiReason == null) return valid;
                if (String.IsNullOrEmpty(txtTranPatiReason.Text) || cboTranPatiReason.EditValue == null)
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
