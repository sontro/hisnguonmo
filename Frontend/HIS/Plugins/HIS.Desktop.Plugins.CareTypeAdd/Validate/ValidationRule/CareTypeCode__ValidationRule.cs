using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.CareTypeAdd.Validate.ValidationRule
{
    class CareTypeCode__ValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtCareTypeCode;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtCareTypeCode == null || String.IsNullOrEmpty(txtCareTypeCode.Text.Trim())) return valid;

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
