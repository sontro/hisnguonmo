using HIS.Desktop.LibraryMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisEmotionlessMethod
{
    class ValidateMaxLength : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txt;
        internal int maxLength;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txt == null) return valid;
                if (string.IsNullOrEmpty(txt.Text) )
                {
                    this.ErrorText = "Trường dữ liệu bắt buộc";
                    return valid;
                }
                else if (!string.IsNullOrEmpty(txt.Text) && Inventec.Common.String.CountVi.Count(txt.Text) > maxLength)
                {
                    this.ErrorText = string.Format("Chỉ được nhập tối đa {0} kí tự", maxLength);
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
