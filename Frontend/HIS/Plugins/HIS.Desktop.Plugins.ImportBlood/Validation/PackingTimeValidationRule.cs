using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImportBlood.Validation
{
    class PackingTimeValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.ButtonEdit txtPackingTime;
        internal DevExpress.XtraEditors.DateEdit dtPackingTime;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (dtPackingTime == null || txtPackingTime == null) return valid;
                if (dtPackingTime.EditValue != null)
                {
                    if (dtPackingTime.DateTime > DateTime.Now)
                    {
                        ErrorText = Base.ResourceMessageLang.NgayPhaiNhoHonNgayHienTai;
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
