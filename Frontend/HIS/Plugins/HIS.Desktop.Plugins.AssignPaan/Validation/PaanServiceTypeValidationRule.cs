using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignPaan.Validation
{
    class PaanServiceTypeValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtPaanServiceTypeCode;
        internal DevExpress.XtraEditors.GridLookUpEdit cboPaanServiceType;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtPaanServiceTypeCode == null || cboPaanServiceType == null)
                    return valid;
                if (String.IsNullOrEmpty(txtPaanServiceTypeCode.Text) || cboPaanServiceType.EditValue == null)
                {
                    ErrorText = Resources.ResourceMessageLang.TruongDuLieuBatBuoc;
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
