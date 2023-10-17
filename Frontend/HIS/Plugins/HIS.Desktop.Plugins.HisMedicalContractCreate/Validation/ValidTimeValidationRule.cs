using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisMedicalContractCreate.Validation
{
    class ValidTimeValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.DateEdit dtValidToDate;
        internal DevExpress.XtraEditors.DateEdit dtValidFromDate;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (dtValidToDate == null) return valid;
                if (dtValidFromDate == null) return valid;
                if (dtValidToDate.DateTime != DateTime.MinValue && dtValidFromDate.DateTime != DateTime.MinValue && dtValidFromDate.DateTime.Date > dtValidToDate.DateTime.Date)
                {
                    return valid;
                }

                valid = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return valid;
        }
    }
}
