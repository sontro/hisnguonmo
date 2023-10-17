using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.DXErrorProvider;

namespace HIS.Desktop.Utility.ValidateRule
{
    public class CommonNumberEditValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        public DevExpress.XtraEditors.BaseEdit numberEdit;
        public bool isValidNull = true;
        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (numberEdit == null) return valid;
                if (isValidNull && (numberEdit == null || string.IsNullOrEmpty(numberEdit.Text)))
                {
                    this.ErrorText = Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                    return valid;
                }

                if ((!string.IsNullOrEmpty(numberEdit.Text)) && (numberEdit.Text.Length < 9 || numberEdit.Text.Length > 12))
                {
                    this.ErrorText = "Số điện thoại không hợp lệ";
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
