using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.PackingMaterial.Validation
{
    class ExpDateValidationRule : ValidationRule
    {
        internal DevExpress.XtraEditors.DateEdit dtExpDate;

        public override bool Validate(Control control, object value)
        {
            bool valid = true;
            try
            {
                if (dtExpDate == null)
                    return false;
                if (dtExpDate.EditValue != null && dtExpDate.DateTime != DateTime.MinValue && dtExpDate.DateTime < DateTime.Now.Date)
                {
                    ErrorText = "Hạn sử dụng không được nhỏ hơn ngày hiện tại";
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
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
