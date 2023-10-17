using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using HIS.Desktop.LibraryMessage;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute.Validate.ValidationRule
{
    public class InstructionDateCalendarValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.Controls.CalendarControl calendarControl;
        internal DevExpress.XtraLayout.LayoutControlItem lci;
        internal long? inTime;
        internal long? outTime;
        internal TimeSpanEdit timeSpan;
        internal bool isRequired = false;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (lci == null || lci.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                {
                    if (calendarControl == null) return valid;

                    if (isRequired && (calendarControl.EditValue == null))
                    {
                        this.ErrorText = MessageUtil.GetMessage(LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                        this.ErrorType = ErrorType.Warning;
                        return valid;
                    }
                     if(inTime.HasValue && Int64.Parse(calendarControl.DateTime.ToString("yyyyMMdd")) < Int64.Parse(inTime.ToString().Substring(0,8)))
                    {
                        this.ErrorText = "Ngày từ nhỏ hơn ngày vào viện";
                        this.ErrorType = ErrorType.Warning;
                        return valid;
                    }

                    if (outTime.HasValue && Int64.Parse(calendarControl.DateTime.ToString("yyyyMMdd") ) > Int64.Parse(outTime.ToString().Substring(0, 8)))
                    {
                        this.ErrorText = "Ngày đến lớn hơn ngày ra viện";
                        this.ErrorType = ErrorType.Warning;
                        return valid;
                    }
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
