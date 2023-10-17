using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.CareCreate.Validate.ValidationRule
{
    class CareExecuteTime__ValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.DateEdit dtExecuteTime;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (dtExecuteTime == null || dtExecuteTime.EditValue == null || dtExecuteTime.DateTime == DateTime.MinValue) return valid;
                if (String.IsNullOrEmpty(dtExecuteTime.Text))
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
