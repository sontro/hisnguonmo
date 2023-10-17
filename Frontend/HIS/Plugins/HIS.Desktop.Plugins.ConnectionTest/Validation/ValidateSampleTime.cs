using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ConnectionTest.Validation
{
    class ValidateSampleTime : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.DateEdit dtTime;
        internal long intructionTime;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (dtTime == null && dtTime == null) return valid;
                if (dtTime.EditValue != null && dtTime.DateTime != DateTime.MinValue && Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(dtTime.DateTime) < intructionTime)
                {
                    ErrorText = string.Format("Thời gian duyệt mẫu không được nhỏ hơn thời gian y lệnh: {0}.", Inventec.Common.DateTime.Convert.TimeNumberToTimeString(intructionTime));
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
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
