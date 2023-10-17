using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BaseCompensationBillCreate.ValidationRules
{
    class ValidationExpPrice : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spinExportMestPrice;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = true;
            try
            {
                valid = valid && (spinExportMestPrice != null);
                if (valid)
                {
                    if (spinExportMestPrice.Enabled == false)
                        return true;
                }
                if (spinExportMestPrice.Value < 0)
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
