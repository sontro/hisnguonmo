using DevExpress.XtraEditors.DXErrorProvider;
using HIS.Desktop.LibraryMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceReqSampleInfo.Validation
{
    class SampleTimeValidationRule : ValidationRule
    {
        //internal long intructionTime;
        internal DevExpress.XtraEditors.DateEdit dtSampleTime;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (dtSampleTime == null) return valid;
                if (dtSampleTime.EditValue == null || dtSampleTime.DateTime == DateTime.MinValue)
                {
                    ErrorText = MessageUtil.GetMessage(Message.Enum.TruongDuLieuBatBuoc);
                    ErrorType = ErrorType.Warning;
                    return valid;
                }
                //long time = Convert.ToInt64(dtSampleTime.DateTime.ToString("yyyyMMddHHmmss"));
                //if (time < intructionTime)
                //{
                //    string s = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(intructionTime);
                //    ErrorText = String.Format("Thời gian duyệt mẫu không được nhỏ hơn thời gian y lệnh: {0}", s);
                //    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                //    return valid;
                //}
                valid = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }
    }
}
