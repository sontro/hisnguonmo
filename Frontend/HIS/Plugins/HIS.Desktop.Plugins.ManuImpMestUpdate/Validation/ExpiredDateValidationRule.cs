using HIS.Desktop.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ManuImpMestUpdate.Validation
{
    class ExpiredDateValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.ButtonEdit txtExpiredDate;
        internal DevExpress.XtraEditors.DateEdit dtExpiredDate;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtExpiredDate == null || dtExpiredDate == null) return valid;
                if (!String.IsNullOrEmpty(txtExpiredDate.Text))
                {
                    var dt = DateTimeHelper.ConvertDateStringToSystemDate(txtExpiredDate.Text);
                    if (dt == null || dt.Value == DateTime.MinValue)
                    {
                        ErrorText = Resources.ResourceMessage.NguoiDungNhapNgayKhongHopLe;
                        ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                        return valid;
                    }
                    dtExpiredDate.EditValue = dt;
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
