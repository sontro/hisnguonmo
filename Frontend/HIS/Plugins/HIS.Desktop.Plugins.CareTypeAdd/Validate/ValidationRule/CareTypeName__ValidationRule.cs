using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.CareTypeAdd.Validate.ValidationRule
{
    class CareTypeName__ValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtCareTypeName;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtCareTypeName == null || String.IsNullOrEmpty(txtCareTypeName.Text.Trim())) return valid;

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
