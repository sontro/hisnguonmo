using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestCreate.Validation
{
    class ImpPriceValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spinImpPrice;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;

                if (spinImpPrice == null) return valid;
                if (spinImpPrice.EditValue == null)
                {
                    this.ErrorText = Base.ResourceMessageManager.TruongDuLieuBatBuoc;
                    return valid;
                }
                if (spinImpPrice.Value < 0)
                {
                    this.ErrorText = "Trường dữ liệu không được âm";
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
