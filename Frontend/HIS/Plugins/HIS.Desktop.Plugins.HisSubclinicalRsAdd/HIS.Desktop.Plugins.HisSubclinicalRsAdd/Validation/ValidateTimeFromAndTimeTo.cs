using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.DXErrorProvider;

namespace HIS.Desktop.Plugins.HisSubclinicalRsAdd.Validtion
{
    public class ValidateTimeFromAndTimeTo : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TimeSpanEdit spanFrom;
        internal DevExpress.XtraEditors.TimeSpanEdit spanTo;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (spanFrom == null || spanFrom == null) return valid;

                spanFrom.DeselectAll();
                spanTo.DeselectAll();

                if (spanFrom.EditValue == null || spanTo.EditValue == null)
                {
                    valid = true;
                    return valid;
                }

                if (spanFrom.EditValue != null && spanTo.EditValue != null)
                {
                    if (spanFrom.TimeSpan.Hours > spanTo.TimeSpan.Hours)
                    {
                        this.ErrorText = "Giờ chỉ định từ bé hơn hoặc bằng giờ chỉ định đến";
                        this.ErrorType = ErrorType.Warning;
                        return valid;
                    }
                    else if (spanFrom.TimeSpan.Hours == spanTo.TimeSpan.Hours && spanFrom.TimeSpan.Minutes > spanTo.TimeSpan.Minutes)
                    {
                        this.ErrorText = "Giờ chỉ định từ bé hơn hoặc bằng giờ chỉ định đến";
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
