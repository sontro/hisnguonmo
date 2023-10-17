using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImportBlood.Validation
{
    class DocumentDateValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.ButtonEdit txtDocumentDate;
        internal DevExpress.XtraEditors.DateEdit dtDocumentDate;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (dtDocumentDate == null || txtDocumentDate == null) return valid;
                if (dtDocumentDate.EditValue != null)
                {
                    if (dtDocumentDate.DateTime == DateTime.MinValue)
                    {
                        ErrorText = Base.ResourceMessageLang.NgayKhongHopLe;
                        ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                        return valid;
                    }
                    if (dtDocumentDate.DateTime > DateTime.Now)
                    {
                        ErrorText = Base.ResourceMessageLang.NgayPhaiLonHonNgayHienTai;
                        ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                        return valid;
                    }
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
