using HIS.Desktop.LibraryMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ImportBlood.Validation
{
    class CMNDNumberValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtCMND;
        internal bool isValid;
        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (isValid && String.IsNullOrEmpty(txtCMND.Text))
                {
                    this.ErrorText = MessageUtil.GetMessage(LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                    return valid;
                }
                if (!String.IsNullOrEmpty(txtCMND.Text) && ((txtCMND.Text.Trim().Length != 9 && txtCMND.Text.Trim().Length != 12)
                                                            || (txtCMND.Text.Trim().Length == 9 && !txtCMND.Text.Trim().All(char.IsLetterOrDigit))
                                                            || (txtCMND.Text.Trim().Length == 12 && !txtCMND.Text.Trim().All(char.IsDigit))))
                {
                    this.ErrorText = "Độ dài thông tin phải là 9(ký tự chữ, số) hoặc 12(ký tự số)";
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
