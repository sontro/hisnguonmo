using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicineTypeCreate.Validtion
{
    class ValidMaxlengthTxtMedicineTypeCodeName : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtMedicineTypeCode;
        internal DevExpress.XtraEditors.TextEdit txtMedicineTypeName;
        internal bool isValidCode = true;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if ((isValidCode && string.IsNullOrEmpty(txtMedicineTypeCode.Text)) || string.IsNullOrEmpty(txtMedicineTypeName.Text))
                {
                    this.ErrorText = HIS.Desktop.LibraryMessage.MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                    return valid;
                }
                else
                {
                    if (Inventec.Common.String.CountVi.Count(txtMedicineTypeCode.Text) > 25 && Inventec.Common.String.CountVi.Count(txtMedicineTypeName.Text) > 500)
                    {
                        this.ErrorText = "Độ dài mã vượt quá " + 25 + "||" + "Độ dài tên vượt quá " + 500;
                        return valid;
                    }
                    else
                    {
                        if (Inventec.Common.String.CountVi.Count(txtMedicineTypeCode.Text) > 25)
                        {
                            this.ErrorText = "Độ dài mã vượt quá " + 25;
                            return valid;
                        }
                        else if (Inventec.Common.String.CountVi.Count(txtMedicineTypeName.Text) > 500)
                        {
                            this.ErrorText = "Độ dài tên vượt quá " + 500;
                            return valid;
                        }
                        else
                            valid = true;
                    }


                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }
    }
}
