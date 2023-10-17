using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.TreatmentFinish.Validation
{
    class TransferMediOrgValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtMediOrgCode;
        internal DevExpress.XtraEditors.GridLookUpEdit cboMediOrgName;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtMediOrgCode == null || cboMediOrgName == null) return valid;
                if (String.IsNullOrEmpty(txtMediOrgCode.Text) || cboMediOrgName.EditValue == null)
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
