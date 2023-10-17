using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BaseCompensationBillCreate.ValidationRules
{
    class ValidationExpMestType : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtExportMestTypeCode;
        internal DevExpress.XtraEditors.LookUpEdit cboExportMestType;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtExportMestTypeCode == null || cboExportMestType == null) return valid;
                if (string.IsNullOrEmpty(txtExportMestTypeCode.Text) || string.IsNullOrEmpty(cboExportMestType.Text)) return valid;
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
