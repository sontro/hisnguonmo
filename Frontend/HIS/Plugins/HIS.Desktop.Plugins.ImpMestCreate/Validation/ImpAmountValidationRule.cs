using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestCreate.Validation
{
    class ImpAmountValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spinImpAmount;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;

                if (spinImpAmount == null) return valid;
                if (spinImpAmount.EditValue == null)
                {
                    this.ErrorText = Base.ResourceMessageManager.TruongDuLieuBatBuoc;
                    return valid;
                }
                if (spinImpAmount.Value <= 0)
                {
                    this.ErrorText = "Số lượng phải lớn hơn 0";
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
