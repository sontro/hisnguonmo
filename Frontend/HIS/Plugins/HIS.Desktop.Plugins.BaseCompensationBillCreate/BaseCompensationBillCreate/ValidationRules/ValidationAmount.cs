using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BaseCompensationBillCreate.ValidationRules
{
    class ValidationAmount : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spinAmount;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = true;
            try
            {
                valid = valid && (spinAmount != null);
                if (valid)
                {
                    if (spinAmount.Enabled == false)
                        return true;
                }
                if (spinAmount.Value < 0)
                    return false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return valid;
        }
    }
}
