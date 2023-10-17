using HIS.Desktop.LibraryMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HIS.Desktop.Plugins.InfusionCreate.Validation
{
    class ServiceUnitValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
     internal DevExpress.XtraEditors.GridLookUpEdit cboServiceUnitName;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
             if (cboServiceUnitName == null) return valid;
             if (cboServiceUnitName.EditValue==null)
                {
                    ErrorText = MessageUtil.GetMessage(LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
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
