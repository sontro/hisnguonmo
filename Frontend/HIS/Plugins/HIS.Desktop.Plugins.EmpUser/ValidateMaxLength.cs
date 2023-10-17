using DevExpress.XtraEditors;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.EmpUser
{
    public class ValidateMaxLength : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal object txt;
        internal int count = 0;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txt is TextEdit)
                {
                    var text = txt as TextEdit;
                    if (!string.IsNullOrEmpty(text.Text.Trim()) && Inventec.Common.String.CountVi.Count(text.Text.Trim()) > count)
                    {
                        this.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                        this.ErrorText = "Không được nhập quá " + count + " kí tự";
                        return valid;
                    }
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
