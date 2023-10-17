using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.TreatmentFinish.Validation
{
    class DeathCauseValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
       
        internal DevExpress.XtraEditors.GridLookUpEdit cboDeathCause;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if ( cboDeathCause == null) return valid;
                if ( cboDeathCause.EditValue == null)
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
