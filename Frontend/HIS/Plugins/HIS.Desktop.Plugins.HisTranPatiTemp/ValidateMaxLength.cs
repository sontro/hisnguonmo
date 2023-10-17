using Inventec.Desktop.Common.LibraryMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisTranPatiTemp
{
    class ValidateMaxLength : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtEdit;
        internal int maxlength;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtEdit.Enabled && (String.IsNullOrEmpty(txtEdit.Text.Trim())))
                {
                    this.ErrorText ="Trường dữ liệu bắt buộc";
                    return valid;
                }

                if (txtEdit.Enabled && txtEdit != null && !string.IsNullOrEmpty(txtEdit.Text) && Inventec.Common.String.CheckString.IsOverMaxLengthUTF8(txtEdit.Text, maxlength))
                {
                    this.ErrorText = "Trường dữ liệu vượt quá ký tự cho phép( " + maxlength + " kí tự)";
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
