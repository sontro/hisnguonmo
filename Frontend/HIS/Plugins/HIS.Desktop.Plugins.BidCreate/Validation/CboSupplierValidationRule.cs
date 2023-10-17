using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BidCreate.Validation
{
    class CboSupplierValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtSupplierCode;
        internal DevExpress.XtraEditors.GridLookUpEdit cboSupplier;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtSupplierCode == null || cboSupplier == null) return valid;
                if (string.IsNullOrEmpty(txtSupplierCode.Text) || string.IsNullOrEmpty(cboSupplier.Text)) return valid;
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
