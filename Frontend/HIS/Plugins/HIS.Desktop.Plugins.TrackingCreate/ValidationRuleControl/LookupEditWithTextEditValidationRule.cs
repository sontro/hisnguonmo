using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.TrackingCreate
{
    class LookupEditWithTextEditValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtTextEdit;
        internal DevExpress.XtraEditors.LookUpEdit cbo;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtTextEdit == null || cbo == null) return valid;
                if (String.IsNullOrEmpty(txtTextEdit.Text) || cbo.EditValue == null)
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
