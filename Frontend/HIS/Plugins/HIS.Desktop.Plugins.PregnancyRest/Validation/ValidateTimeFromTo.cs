using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PregnancyRest.Validation
{
    class ValidateTimeFromAndTo : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.DateEdit dtTimeFrom;
        internal DevExpress.XtraEditors.DateEdit dtTimeTo;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool success = false;
            try
            {
                if (dtTimeFrom == null || dtTimeTo == null) return success;

                if (dtTimeFrom.EditValue == null || dtTimeFrom.DateTime == DateTime.MinValue)
                    return success;

                if (dtTimeTo.EditValue != null && dtTimeTo.DateTime != DateTime.MinValue && dtTimeTo.DateTime < dtTimeFrom.DateTime)
                {
                    this.ErrorText = "Thời gian từ không được lớn hơn thời gian đến";
                    return success;
                }
                success = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return success;
        }
    }
}
