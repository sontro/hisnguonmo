using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.HisCarerCard
{
    class ValidationRequireAndMaxLength : ValidationRule
    {
        internal DevExpress.XtraEditors.BaseControl textEdit;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (textEdit == null || string.IsNullOrEmpty(textEdit.Text))
                {
                    this.ErrorText = "Trường dữ liệu bắt buộc";
                    this.ErrorType = ErrorType.Warning;
                    return valid;
                }
                if (!String.IsNullOrEmpty(textEdit.Text) && Inventec.Common.String.CountVi.Count(textEdit.Text) > 20)
                {
                    this.ErrorText = "Số lượng kí tự địa chỉ phải nhỏ hơn 20 kí tự";
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
