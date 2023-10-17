using HIS.Desktop.LibraryMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ReturnMicrobiologicalResults.Validation
{
    class ValidationNote : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.BaseEdit txtNote;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtNote == null)
                    return valid;
                if (!string.IsNullOrEmpty(txtNote.Text.Trim()) && Inventec.Common.String.CheckString.IsOverMaxLengthUTF8(txtNote.Text.Trim(), 500))
                {
                    ErrorText = "Chỉ được phép nhập tối đa 500 kí tự";
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
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
