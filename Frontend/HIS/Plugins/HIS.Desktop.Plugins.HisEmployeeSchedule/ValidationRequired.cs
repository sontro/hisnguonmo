using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.HisEmployeeSchedule
{
    class ValidationRequired : ValidationRule
    {
        internal DevExpress.XtraEditors.BaseControl control;
        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (control == null) return valid;
                if (String.IsNullOrEmpty(control.Text.Trim()))
                {
                    this.ErrorText = Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                    this.ErrorType = ErrorType.Warning;
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
