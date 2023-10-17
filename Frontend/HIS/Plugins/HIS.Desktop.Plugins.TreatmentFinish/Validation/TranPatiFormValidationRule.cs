using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.TreatmentFinish.Validation
{
    class TranPatiFormValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtTranPatiForm;
        internal DevExpress.XtraEditors.GridLookUpEdit cboTranPatiForm;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtTranPatiForm == null || cboTranPatiForm == null) return valid;
                if (String.IsNullOrEmpty(txtTranPatiForm.Text) || cboTranPatiForm.EditValue == null)
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
