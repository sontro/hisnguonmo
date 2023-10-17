using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.DXErrorProvider;
using HIS.Desktop.Plugins.TrackingCreate.Resources;
using HIS.Desktop.LibraryMessage;

namespace HIS.Desktop.Plugins.TrackingCreate
{
    class MemoEditValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.MemoEdit txtTextEdit;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtTextEdit == null)
                {
                    return valid;
                }
                if (String.IsNullOrWhiteSpace(txtTextEdit.Text))
                {
                    this.ErrorText = MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                    this.ErrorType = ErrorType.Warning;
                    return valid;
                }

                if (!String.IsNullOrEmpty(txtTextEdit.Text) && Encoding.UTF8.GetByteCount(txtTextEdit.Text) > 4000)
                {
                    this.ErrorText = string.Format(ResourceMessage.NhapQuaMaxlength, 4000);
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
