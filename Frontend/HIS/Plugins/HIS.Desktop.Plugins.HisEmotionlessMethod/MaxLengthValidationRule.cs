using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisEmotionlessMethod
{
    class MaxLengthValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtEdit;
        internal int maxlength;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtEdit == null) return valid;
                if (String.IsNullOrEmpty(txtEdit.Text))
                {
                    return valid;
                }
                if (Inventec.Common.String.CheckString.IsOverMaxLengthUTF8(txtEdit.Text, maxlength))
                {
                    this.ErrorText = "Trường dữ liệu vượt quá maxlength( " + maxlength + " kí tự)";
                    return valid;
                }
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
