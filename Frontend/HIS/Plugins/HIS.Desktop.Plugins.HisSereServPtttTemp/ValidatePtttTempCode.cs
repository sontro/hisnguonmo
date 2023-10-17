using HIS.Desktop.LibraryMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.HisSereServPtttTemp
{
    public class ValidatePtttTempCode : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txt;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (string.IsNullOrEmpty(control.Text.Trim()))
                {
                    this.ErrorText = MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                    return valid;
                }
                else if (control.Text.Contains(" "))
                {
                    this.ErrorText = "Trường dữ liệu không được chứa khoảng trắng";
                    return valid;
                }
                else if (Inventec.Common.String.CountVi.Count(control.Text.Trim()) > 50)
                {
                    this.ErrorText = "Trường dữ liệu phải nhỏ hơn 50 ký tự";
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
