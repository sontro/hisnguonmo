
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.DispenseMedicine.Validate
{
    class ExpDateValidationRule :
DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.DateEdit dtDate;

        public override bool Validate(Control control, object value)
        {
            bool valid = true;
            try
            {
                if (dtDate.DateTime < DateTime.Now.Date)
                {
                    this.ErrorText = "Hạn sử dụng phải lớn hơn ngày hiện tại";
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
