using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.DXErrorProvider;

namespace HIS.Desktop.Plugins.RegisterGate
{
    internal class ValidateCheckZero : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        public DevExpress.XtraEditors.BaseEdit textEdit;
        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                string check = textEdit.Text;
                int a = 0;
                if (!string.IsNullOrEmpty(check))
                {
                    if (!check.Contains(a.ToString()))
                    {
                        this.ErrorText = "Chỉ cho phép nhập số 0";
                        this.ErrorType = ErrorType.Warning;
                        return valid;
                    }

                    valid = true;

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
