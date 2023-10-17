using HIS.Desktop.LibraryMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisFileType.Validation
{
    class FileTypeValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtControl;
        internal long maxLength;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            byte[] bytes = Encoding.UTF8.GetBytes(txtControl.Text.Trim());
            try
            {
                if (txtControl == null) return valid;
                if (String.IsNullOrEmpty(txtControl.Text))
                {
                    ErrorText = MessageUtil.GetMessage(Message.Enum.TruongDuLieuBatBuoc);
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                    return valid;
                }
                if (bytes.Length > maxLength)
                {
                    ErrorText = "Vượt quá độ dài cho phép " + maxLength;
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
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
