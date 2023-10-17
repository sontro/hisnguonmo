using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.Register.ValidationRule
{
    class TextEditMaxLengthValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtEdit;
        internal int maxlength;
        internal bool isVali;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool vaild = false;
            try
            {
                if (isVali && (txtEdit == null || string.IsNullOrEmpty(txtEdit.Text)))
                {
                    this.ErrorText = Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                    return vaild;
                }
                if (txtEdit != null && !string.IsNullOrEmpty(txtEdit.Text) && Inventec.Common.String.CheckString.IsOverMaxLengthUTF8(txtEdit.Text, maxlength))
                {
                    this.ErrorText = "Trường dữ liệu vượt quá maxlength( " + maxlength + " kí tự)";
                    return vaild;
                }
                vaild = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return vaild;
        }
    }
}
