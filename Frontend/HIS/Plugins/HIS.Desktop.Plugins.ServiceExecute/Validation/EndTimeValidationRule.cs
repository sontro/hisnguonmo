using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceExecute.Validation
{
    class EndTimeValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.DateEdit dtEndTime;
        internal long IntructionTime;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool vaild = false;
            try
            {
                if (dtEndTime == null) return vaild;

                if (dtEndTime.EditValue != null)
                    if (Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(dtEndTime.DateTime) < IntructionTime)
                    {
                        this.ErrorText = "Thời gian kết thúc không được nhỏ hơn thời gian y lệnh";
                        return vaild;
                    }

                vaild = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return vaild;
        }
    }
}
