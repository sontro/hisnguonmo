using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.TrackingCreate.ValidationRuleControl
{
    class SheetOrderValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtSheetOrder;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (!string.IsNullOrEmpty(txtSheetOrder.Text) && long.Parse(txtSheetOrder.Text) <= 0)
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
