using Inventec.Common.String;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicineTypeCreate.Validtion
{
    class MedicineNationalCodeValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtMedicineNationalCode;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtMedicineNationalCode == null || txtMedicineNationalCode == null)
                    return valid;
                if (!String.IsNullOrWhiteSpace(txtMedicineNationalCode.Text))
                {
                    if (CheckString.IsOverMaxLengthUTF8(txtMedicineNationalCode.Text, 30))
                    {
                        ErrorText = "Độ dài không được vượt quá 30!";
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
