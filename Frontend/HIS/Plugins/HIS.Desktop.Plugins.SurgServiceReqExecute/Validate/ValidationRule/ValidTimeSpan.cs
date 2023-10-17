using HIS.Desktop.Plugins.SurgServiceReqExecute.Resources;
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
    class ValidTimeSpan :
DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.DateEdit dateFromEdit;
        internal DevExpress.XtraEditors.DateEdit dateToEdit;
        internal DevExpress.XtraLayout.LayoutControlItem lciDate;
        internal DevExpress.XtraEditors.Controls.CalendarControl calendarControl;
        internal DevExpress.XtraLayout.LayoutControlItem lciCa;
        internal DevExpress.XtraEditors.TimeSpanEdit timeSpanEdit;
        internal long? inTime;
        internal long? outTime;
        public override bool Validate(Control control, object value)
        {
            bool valid = true;
            try
            {
                if (timeSpanEdit.EditValue == null)
                {
                    this.ErrorText = ResourceMessage.TruongDuLieuBatBuoc;
                    return false;
                }
                if (lciDate.Visible)
                {
                    string dteFrom = dateFromEdit.DateTime.ToString("yyyyMMdd");
                    string dteTo = dateToEdit.DateTime.ToString("yyyyMMdd");
                    if (inTime.HasValue && Int64.Parse(dteFrom + (DateTime.Today.Date + timeSpanEdit.TimeSpan).ToString("HHmm")) < Int64.Parse(inTime.ToString().Substring(0, 12)))
                    {
                        this.ErrorText = "Thời gian nhỏ hơn thời gian vào viện";
                        this.ErrorType = ErrorType.Warning;
                        return false;
                    }

                    if (outTime.HasValue && Int64.Parse(dteTo + (DateTime.Today.Date + timeSpanEdit.TimeSpan).ToString("HHmm")) > Int64.Parse(outTime.ToString().Substring(0, 12)))
                    {
                        this.ErrorText = "Thời gian lớn hơn thời gian ra viện";
                        this.ErrorType = ErrorType.Warning;
                        return false;
                    }
                }

                if(lciCa.Visible)
                {
                    if (inTime.HasValue && Int64.Parse(calendarControl.DateTime.ToString("yyyyMMdd") + (DateTime.Today.Date + timeSpanEdit.TimeSpan).ToString("HHmm")) < Int64.Parse(inTime.ToString().Substring(0, 12)))
                    {
                        this.ErrorText = "Thời gian nhỏ hơn thời gian vào viện";
                        this.ErrorType = ErrorType.Warning;
                        return false;
                    }

                    if (outTime.HasValue && Int64.Parse(calendarControl.DateTime.ToString("yyyyMMdd") + (DateTime.Today.Date + timeSpanEdit.TimeSpan).ToString("HHmm")) > Int64.Parse(outTime.ToString().Substring(0, 12)))
                    {
                        this.ErrorText = "Thời gian lớn hơn thời gian ra viện";
                        this.ErrorType = ErrorType.Warning;
                        return false;
                    }
                }    
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }
    }
}
