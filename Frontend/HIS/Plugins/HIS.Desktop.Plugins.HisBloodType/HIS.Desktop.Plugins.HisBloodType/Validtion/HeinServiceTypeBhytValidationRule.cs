using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisBloodType.Validtion
{
    class HeinServiceTypeBhytValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtHeinServiceTypeBhytCode;
        internal DevExpress.XtraEditors.TextEdit txtHeinServiceTypeBhytName;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtHeinServiceTypeBhytCode == null || txtHeinServiceTypeBhytName == null)
                    return valid;
                if ((String.IsNullOrEmpty(txtHeinServiceTypeBhytCode.Text) && String.IsNullOrEmpty(txtHeinServiceTypeBhytName.Text)) || (!String.IsNullOrEmpty(txtHeinServiceTypeBhytCode.Text) && !String.IsNullOrEmpty(txtHeinServiceTypeBhytName.Text)))
                {
                    valid = true;
                }
                else
                {
                    ErrorText = "Nếu nhập mã bhyt thì cần nhập tên bhyt";
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                    return valid;
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
