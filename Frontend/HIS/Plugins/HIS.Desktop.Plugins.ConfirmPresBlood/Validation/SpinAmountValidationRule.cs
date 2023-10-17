using HIS.Desktop.Plugins.ConfirmPresBlood.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ConfirmPresBlood.Validation
{
    class SpinAmountValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spinAmount;
        internal decimal? ycAmount;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (spinAmount == null) return valid;
                if (spinAmount.EditValue == null)
                {
                    ErrorText = ResourceMessage.TruongDuLieuBatBuoc;
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                    return valid;
                }
                if (spinAmount.Value <= 0)
                {
                    ErrorText = "Giá trị lớn hơn 0";
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                    return valid;
                }
                if (ycAmount < spinAmount.Value)
                {
                    ErrorText = "Số lượng duyệt thay thế lớn hơn số lượng yêu cầu - đã duyệt";
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
