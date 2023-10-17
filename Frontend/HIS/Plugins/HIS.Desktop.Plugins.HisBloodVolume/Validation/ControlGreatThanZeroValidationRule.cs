using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using HIS.Desktop.LibraryMessage;
using Inventec.Common.Logging;
using Inventec.Desktop.Common.Controls.ValidationRule;
using System;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.HisBloodVolume
{
    public class ControlGreatThanZeroOrLessThanThoundValidationRule : ValidationRule
    {

        internal DevExpress.XtraEditors.SpinEdit spin;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (spin.Value <= 0 && spin.EditValue != null)
                {
                    this.ErrorText = MessageUtil.GetMessage(LibraryMessage.Message.Enum.TruongDuLieuKhongNhanGiaTriAm);
                    return valid;
                }
                if (spin.EditValue != null && spin.Value>999)
                {
                    this.ErrorText = "Trường dữ liệu chỉ được phép bé hơn 1000";
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