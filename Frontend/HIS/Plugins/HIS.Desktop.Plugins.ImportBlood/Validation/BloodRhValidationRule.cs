using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ImportBlood.Validation
{
    class BloodRhValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule        
    {
        internal DevExpress.XtraEditors.LookUpEdit cboBloodRh;
        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (cboBloodRh == null) return valid;
                if (String.IsNullOrEmpty(cboBloodRh.Text) || cboBloodRh.EditValue == null)
                {
                    ErrorText = Base.ResourceMessageLang.TruongDuLieuBatBuoc;
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
//COMMIT