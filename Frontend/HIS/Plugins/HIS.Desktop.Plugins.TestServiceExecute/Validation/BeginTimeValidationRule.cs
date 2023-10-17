using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TestServiceExecute.Validation
{
    class BeginTimeValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.DateEdit dtBeginTime;
        internal DevExpress.XtraEditors.DateEdit dtEndTime;
        internal long IntructionTime;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool vaild = false;
            try
            {
                if (dtBeginTime == null) return vaild;

                if (dtBeginTime.EditValue != null)
                {
                    if (Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(dtBeginTime.DateTime) < IntructionTime)
                    {
                        this.ErrorText = "Thời gian bắt đầu không được nhỏ hơn thời gian y lệnh";
                        return vaild;
                    }

                    if (dtEndTime != null && dtEndTime.EditValue != null && Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(dtBeginTime.DateTime) > Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(dtEndTime.DateTime))
                    {
                        this.ErrorText = "Thời gian bắt đầu không được lớn hơn thời gian kết thúc";
                        return vaild;
                    }
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
