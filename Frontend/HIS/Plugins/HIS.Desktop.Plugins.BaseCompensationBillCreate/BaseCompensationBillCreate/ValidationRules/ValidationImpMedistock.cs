using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BaseCompensationBillCreate.ValidationRules
{
    class ValidationImpMedistock : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtMediStockImportCode;
        internal DevExpress.XtraEditors.LookUpEdit cboMediStockImport;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtMediStockImportCode == null || cboMediStockImport == null) return valid;
                if (string.IsNullOrEmpty(txtMediStockImportCode.Text) || string.IsNullOrEmpty(cboMediStockImport.Text)) return valid;
                valid = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return valid;
        }
    }
}
