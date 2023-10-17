using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BaseCompensationBillCreate.ValidationRules
{
    class ValidationExpVat : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spinExportMestVat;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = true;
            try
            {
                valid = valid && (spinExportMestVat != null);
                if (valid)
                {
                    if (spinExportMestVat.Enabled == false)
                        return true;
                }
                if (spinExportMestVat.Value < 0 || spinExportMestVat.Value > 100)
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
