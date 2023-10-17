using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BaseCompensationBillCreate.ValidationRules
{
    class ValidationExpMedistock : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.LookUpEdit cboMediStockExport;
        internal DevExpress.XtraEditors.TextEdit txtMediStockExportCode;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtMediStockExportCode == null || cboMediStockExport == null) return valid;
                if (string.IsNullOrEmpty(txtMediStockExportCode.Text) || string.IsNullOrEmpty(cboMediStockExport.Text)) return valid;
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
