using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ServiceReqList
{
    class ValidateBiggerThan0 : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spinEdit;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (Inventec.Common.TypeConvert.Parse.ToInt64(spinEdit.EditValue.ToString()) < 1)
                {
                    this.ErrorText = "Số ngày nhập phải lớn hơn 0";
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
