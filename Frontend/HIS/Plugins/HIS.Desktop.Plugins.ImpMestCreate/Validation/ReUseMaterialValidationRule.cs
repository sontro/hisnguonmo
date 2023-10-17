using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestCreate.Validation
{
    class ReUseMaterialValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spinEdit;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;

                if (spinEdit == null) return valid;
                if (spinEdit.Enabled)
                {
                    if (spinEdit.EditValue == null)
                    {
                        this.ErrorText = Base.ResourceMessageManager.TruongDuLieuBatBuoc;
                        return valid;
                    }
                    if (spinEdit.Value <= 0)
                    {
                        this.ErrorText = "Số lần tái sử dụng phải lớn hơn 0";
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
