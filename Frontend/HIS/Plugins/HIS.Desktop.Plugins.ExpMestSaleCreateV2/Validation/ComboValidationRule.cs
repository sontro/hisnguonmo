using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors.DXErrorProvider;

namespace HIS.Desktop.Plugins.ExpMestSaleCreateV2.Validation
{
    public class ComboValidationRule : ValidationRule
    {
        internal DevExpress.XtraEditors.GridLookUpEdit cbo;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (cbo == null) return valid;
                if (cbo.EditValue == null)
                {
                    this.ErrorText = Base.ResourceMessageLang.TruongDuLieuBatBuoc;
                    this.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
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
