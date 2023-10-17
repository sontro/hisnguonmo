using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ConnectionTest.Validation
{
    class ValidateTime : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.DateEdit DateEdit;
        public bool check;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (DateEdit == null) return valid;
                if (check)
                    ErrorText = "Thời gian trả kết quả lớn hơn thời gian y lệnh";
                ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                return valid;
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
