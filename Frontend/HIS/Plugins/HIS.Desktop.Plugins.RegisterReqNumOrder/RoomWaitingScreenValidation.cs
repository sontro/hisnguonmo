using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.RegisterReqNumOrder
{
    class RoomWaitingScreenValidation : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spn;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (spn == null || spn.EditValue == null || Convert.ToDecimal(spn.EditValue) == 0)
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
