using DevExpress.XtraEditors.DXErrorProvider;
using HIS.Desktop.LibraryMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.PackingMaterial.Validation
{
    class AmountValidationRule : ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spinAmount;

        public override bool Validate(Control control, object value)
        {
            bool valid = true;
            try
            {
                if (spinAmount == null)
                    return false;
                if (spinAmount.Value <= 0)
                {
                    ErrorText = MessageUtil.GetMessage(LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                    valid = false;
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
