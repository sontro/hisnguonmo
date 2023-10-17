using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.InfantInformation.Validate
{
    class CMTvalidation : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtTextEdit;
        public CMTvalidation()
        {
        }
        public override bool Validate(Control control, object value)
        {
            bool valid = true;
            try
            {
                if (String.IsNullOrEmpty(txtTextEdit.Text))
                {
                    ErrorText = "Trường dữ liệu bắt buộc";
                    valid = false;

                }
                else if (Inventec.Common.String.CountVi.Count(txtTextEdit.Text.Trim()) != 9 && Inventec.Common.String.CountVi.Count(txtTextEdit.Text.Trim()) != 12)
                {
                    ErrorText = "Dữ liệu không đúng định dạng, CMND/CCCD phải 9 hoặc 12 ký tự.";
                    valid = false;
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }
    }
}

