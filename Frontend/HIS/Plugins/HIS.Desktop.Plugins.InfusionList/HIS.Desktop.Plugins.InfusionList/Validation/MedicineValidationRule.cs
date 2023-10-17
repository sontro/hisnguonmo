using HIS.Desktop.LibraryMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HIS.Desktop.Plugins.InfusionCreate.Validation
{
     class MedicineValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {

					internal DevExpress.XtraEditors.GridLookUpEdit cboMedicineType;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
             if (cboMedicineType == null) return valid;
             if (cboMedicineType.EditValue == null)
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
