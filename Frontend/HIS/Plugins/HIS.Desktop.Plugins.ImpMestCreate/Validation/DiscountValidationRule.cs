using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestCreate.Validation
{
    class DiscountValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spinDiscountRatio;
        internal DevExpress.XtraEditors.SpinEdit spinDiscountPrice;
        internal DevExpress.XtraEditors.LookUpEdit cboImpMestType;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (spinDiscountPrice == null || spinDiscountRatio == null || cboImpMestType == null) return valid;
                if (cboImpMestType.EditValue != null && Convert.ToInt64(cboImpMestType.EditValue) == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__NCC)
                {
                    if (spinDiscountPrice.Value < 0 || spinDiscountRatio.Value < 0)
                    {
                        ErrorText = Base.ResourceMessageManager.TruongDuLieuBatBuoc;
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
