using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestCreate.Validation
{
    class ImpVatRatioValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spinImpVatRatio;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;

                if (spinImpVatRatio == null) return valid;
                if (spinImpVatRatio.EditValue == null)
                {
                    this.ErrorText = Base.ResourceMessageManager.TruongDuLieuBatBuoc;
                    return valid;
                }
                if (spinImpVatRatio.Value < 0 || spinImpVatRatio.Value > 100)
                {
                    this.ErrorText = "Trường dữ liệu không hợp lệ";
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
